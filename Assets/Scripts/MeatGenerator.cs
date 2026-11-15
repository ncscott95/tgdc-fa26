using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeatGenerator : MonoBehaviour
{
    private const float BaseNodeDistance = 0.3f;
    private const float NoiseScale = 0.3f;
    private const float BaseNodeColliderRadius = 0.2f;
    private const float BaseTangentScale = 0.15f;
    private const int MinNodes = 6;
    private const int MaxNodes = 12; // Inclusive
    private const float SpringDampingRatio = 0.0f;
    private const float SpringFrequency = 8f;
    private const float EyePositionRandomRadius = 0.1f;
    private const float EyeSpringFrequency = 3f;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _basePrefab;
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private GameObject _eyePrefab;

    public void GenerateMeat()
    {
        // random offsets to sample into the perlin noise
        float noiseOffsetX = Random.Range(0f, 1000f);
        float noiseOffsetY = Random.Range(0f, 1000f);

        int numNodes = Random.Range(MinNodes, MaxNodes + 1);
        List<Transform> nodes = new List<Transform>();
        List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();
        List<CircleCollider2D> colliders = new List<CircleCollider2D>();

        Transform newMeat = Instantiate(_basePrefab).transform;
        newMeat.position = _spawnPoint.position;

        // apply random offsets to perimeter nodes
        for (int i = 0; i < numNodes; i++)
        {
            Transform newNode = Instantiate(_nodePrefab, newMeat).transform;
            float angle = 2 * Mathf.PI / numNodes;
            Vector2 coords = new Vector2(Mathf.Cos(angle * i), Mathf.Sin(angle * i));
            float noise = (Mathf.PerlinNoise(coords.x + noiseOffsetX, coords.y + noiseOffsetY) - 0.5f) * NoiseScale;
            newNode.localPosition = new Vector2((BaseNodeDistance + noise) * coords.x, (BaseNodeDistance + noise) * coords.y);

            CircleCollider2D newCollider = newNode.GetComponent<CircleCollider2D>();
            newCollider.radius = BaseNodeColliderRadius / (numNodes / 4f); // arbitrary amount to shrink with more nodes

            nodes.Add(newNode);
            rigidbodies.Add(newNode.GetComponent<Rigidbody2D>());
            colliders.Add(newCollider);
        }

        Rigidbody2D centerRb = newMeat.GetComponent<Rigidbody2D>();

        // connect all perimeter nodes to every other node with springs
        for (int i = 0; i < numNodes; i++)
        {
            Transform node = nodes[i];

            for (int j = 0; j < numNodes; j++)
            {
                SpringJoint2D spring = node.AddComponent<SpringJoint2D>();

                spring.dampingRatio = SpringDampingRatio;
                spring.frequency = SpringFrequency;
                if (j == 0)
                {
                    spring.connectedBody = centerRb;
                }
                else
                {
                    spring.connectedBody = rigidbodies[(i + j) % numNodes];
                }
            }

            colliders[i].enabled = true;
        }

        // place eyes randomly within a circle, anchor with springs
        Transform eyeContainer = newMeat.Find("EyeContainer");
        Transform eye = Instantiate(_eyePrefab, eyeContainer).transform;

        float eyePosRadius = EyePositionRandomRadius * Mathf.Sqrt(Random.value);
        float eyePosTheta = Random.value * 2 * Mathf.PI;
        eyeContainer.localPosition = new Vector2(eyePosRadius * Mathf.Cos(eyePosTheta), eyePosRadius * Mathf.Sin(eyePosTheta));
        eyeContainer.Rotate(Vector3.forward, Random.Range(0f, 360f));

        List<SpriteRenderer> eyeRenderers = new List<SpriteRenderer>();

        foreach (Rigidbody2D node in rigidbodies)
        {
            SpringJoint2D spring = eye.AddComponent<SpringJoint2D>();
            // spring.dampingRatio = SpringDampingRatio;
            spring.dampingRatio = 0f;
            spring.frequency = EyeSpringFrequency;
            spring.connectedBody = node;
        }

        eyeRenderers.Add(eye.GetComponent<SpriteRenderer>());

        // initialize all fields
        SoftbodySpriteShape softSprite = newMeat.GetComponent<SoftbodySpriteShape>();
        softSprite.SetTangentScale(BaseTangentScale / (numNodes / 6f)); // arbitrary amount to shrink with more nodes
        softSprite.SetPoints(nodes);
        softSprite.SetEyes(eyeRenderers);
        softSprite.Initialize();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GenerateMeat();
        }
    }
}
