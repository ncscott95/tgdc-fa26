using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SteakRepopulator : MonoBehaviour
{
    private const float RepopulationDelay = 0.1f;

    [SerializeField] private SteakGenerator _meatGenerator;
    [SerializeField] private List<Transform> _spawnPoints;

    private List<SteakData> _steaksToRepopulate = new List<SteakData>();

    void Start()
    {
        foreach (SteakData data in PersistentData.Instance.Steaks)
        {
            _steaksToRepopulate.Add(data);
        }

        StartCoroutine(RepopulateSteakCoroutine());
    }

    private IEnumerator RepopulateSteakCoroutine()
    {
        int spawnPointIndex = 0;
        while (_steaksToRepopulate.Count > 0)
        {
            SteakData data = _steaksToRepopulate[0];
            _steaksToRepopulate.RemoveAt(0);

            spawnPointIndex = (spawnPointIndex + 1) % _spawnPoints.Count;
            Transform spawnPoint = _spawnPoints[spawnPointIndex];
            _meatGenerator.GenerateMeat(data, false, spawnPoint);

            yield return new WaitForSeconds(RepopulationDelay);
        }
    }
}
