using System.Collections.Generic;
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

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _basePrefab;
    [SerializeField] private GameObject _nodePrefab;

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

        for (int i = 0; i < numNodes; i++)
        {
            Transform node = nodes[i];
            SpringJoint2D[] springs = node.GetComponents<SpringJoint2D>();
            springs[0].connectedBody = centerRb;
            springs[1].connectedBody = rigidbodies[(i + 1) % numNodes];

            springs[0].enabled = true;
            springs[1].enabled = true;

            colliders[i].enabled = true;
        }

        SoftbodySpriteShape softSprite = newMeat.GetComponent<SoftbodySpriteShape>();
        softSprite.SetTangentScale(BaseTangentScale / (numNodes / 6f)); // arbitrary amount to shrink with more nodes
        softSprite.SetPoints(nodes);
        softSprite.InitializeVertices();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GenerateMeat();
        }
    }
}
