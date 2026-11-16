using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStartManager : MonoBehaviour
{
    private const float SpawnDelay = 0.1f;
    [SerializeField] private MeatGenerator _meatGenerator;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _startPlatform;
    [SerializeField] private Sprite _platformUpSprite;
    [SerializeField] private Sprite _platformDownSprite;

    private Collider2D _platformCollider;
    private SpriteRenderer _platformRenderer;
    private List<Vector2> _spawnPositions = new List<Vector2>();

    void Start()
    {
        _platformCollider = _startPlatform.GetComponent<Collider2D>();
        _platformRenderer = _startPlatform.GetComponent<SpriteRenderer>();

        _platformCollider.enabled = true;
        _platformRenderer.sprite = _platformUpSprite;

        StartCoroutine(RaceStartSequence());
    }

    private IEnumerator RaceStartSequence()
    {
        yield return SpawnRacers();

        yield return new WaitForSeconds(2);

        Debug.Log("3...");
        yield return new WaitForSeconds(1);
        Debug.Log("2...");
        yield return new WaitForSeconds(1);
        Debug.Log("1...");
        yield return new WaitForSeconds(1);
        Debug.Log("GO!!!");

        _platformRenderer.sprite = _platformDownSprite;
        _platformCollider.enabled = false;
    }

    private IEnumerator SpawnRacers()
    {
        int playerRacers = PersistentData.Instance.SelectedSteaks.Count;
        int totalRacers = PersistentData.Instance.GetSelectedRace().NumOpponents + 1;
        List<SteakSpawnGreyedPair> racersToSpawn = new List<SteakSpawnGreyedPair>();

        // increase spacing with fewer racers
        // 4 racers: 2 space, 8 racers: 1 space
        float spacingScale = -totalRacers / 4f + 3f;

        for (int i = 0; i < totalRacers; i++)
        {
            _spawnPositions.Add(new Vector2((-totalRacers / 2f + 0.5f + i) * spacingScale, 0f));
            Debug.Log($"{_spawnPositions[i]}");
        }

        for (int i = 0; i < playerRacers; i++)
        {
            racersToSpawn.Add(new SteakSpawnGreyedPair(PersistentData.Instance.SelectedSteaks[i], false));
        }

        for (int i = playerRacers; i < totalRacers; i++)
        {
            racersToSpawn.Add(new SteakSpawnGreyedPair(new SteakData(), true));
        }

        while (racersToSpawn.Count > 0)
        {
            yield return new WaitForSeconds(SpawnDelay);

            int racer = Random.Range(0, racersToSpawn.Count);
            SteakData data = racersToSpawn[racer].Data;

            int position = Random.Range(0, _spawnPositions.Count);
            _spawnPoint.localPosition = _spawnPositions[position];
            _spawnPositions.RemoveAt(position);

            _meatGenerator.GenerateMeat(data, false, _spawnPoint, racersToSpawn[racer].SpawnGreyedOut);
            racersToSpawn.RemoveAt(racer);
        }
    }

    [System.Serializable]
    private struct SteakSpawnGreyedPair
    {
        public SteakData Data;
        public bool SpawnGreyedOut;

        public SteakSpawnGreyedPair(SteakData data, bool spawnGreyedOut)
        {
            Data = data;
            SpawnGreyedOut = spawnGreyedOut;
        }
    }
}
