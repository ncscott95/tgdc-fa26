using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeatGenerator : MonoBehaviour
{
    private const float BaseNodeColliderRadius = 0.2f;
    private const float BaseTangentScale = 0.15f;
    private const float SpringDampingRatio = 0.0f;
    private const float SpringFrequency = 8f;
    private const float EyeSpringFrequency = 3f;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _basePrefab;
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private GameObject _eyePrefab;

    private SteakData _testData;

    void Start()
    {
        _testData = new SteakData();
    }

    public Steak InstantiateSteak(SteakData data)
    {
        int numNodes = data.ExteriorNodePositions.Count;
        List<Transform> nodes = new List<Transform>();
        List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();
        List<CircleCollider2D> colliders = new List<CircleCollider2D>();

        Transform newMeat = Instantiate(_basePrefab).transform;
        newMeat.position = _spawnPoint.position;

        for (int i = 0; i < numNodes; i++)
        {
            Transform newNode = Instantiate(_nodePrefab, newMeat).transform;
            newNode.localPosition = data.ExteriorNodePositions[i];

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
        List<SpriteRenderer> eyeRenderers = new List<SpriteRenderer>();
        Transform eyeContainer = newMeat.Find("EyeContainer");
        for (int i = 0; i < data.EyeTransforms.Count; i++)
        {
            Transform eye = Instantiate(_eyePrefab, eyeContainer).transform;

            eye.localPosition = data.EyeTransforms[i].Position;
            eye.Rotate(Vector3.forward, data.EyeTransforms[i].RotationAngle);
            eye.localScale = Vector3.one * data.EyeTransforms[i].Scale;

            foreach (Rigidbody2D node in rigidbodies)
            {
                SpringJoint2D spring = eye.AddComponent<SpringJoint2D>();
                spring.dampingRatio = SpringDampingRatio;
                spring.frequency = EyeSpringFrequency;
                spring.connectedBody = node;
            }

            eyeRenderers.Add(eye.GetComponent<SpriteRenderer>());
        }

        // initialize all fields
        SoftbodySpriteShape softSprite = newMeat.GetComponent<SoftbodySpriteShape>();
        float tangentScale = BaseTangentScale / (numNodes / 6f); // arbitrary amount to shrink with more nodes
        softSprite.Initialize(nodes, eyeRenderers, tangentScale, data.TextureUVOffset);

        Steak steak = newMeat.GetComponent<Steak>();
        return steak;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // InstantiateSteak(NewSteakData());
            InstantiateSteak(_testData);
        }
    }
}
