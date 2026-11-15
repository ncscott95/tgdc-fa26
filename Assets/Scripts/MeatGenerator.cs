using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeatGenerator : MonoBehaviour
{
    private const float BaseNodeDistance = 0.3f;
    private const float BaseNodeColliderRadius = 0.2f;
    private const int MinNodes = 6;
    private const int MaxNodes = 16; // Inclusive

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _basePrefab;
    [SerializeField] private GameObject _nodePrefab;

    public void GenerateMeat()
    {
        int numNodes = Random.Range(MinNodes, MaxNodes + 1);
        List<GameObject> nodeObjs = new List<GameObject>();

        Transform newMeat = Instantiate(_basePrefab).transform;

        for (int i = 0; i < numNodes; i++)
        {
            Transform newNode = Instantiate(_nodePrefab, newMeat).transform;
            float angle = 2 * Mathf.PI / numNodes;
            Vector2 basePos = new Vector2(BaseNodeDistance * Mathf.Cos(angle), BaseNodeDistance * Mathf.Sin(angle));
            newNode.localPosition = basePos;

            nodeObjs.Add(newNode.gameObject);
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GenerateMeat();
        }
    }
}
