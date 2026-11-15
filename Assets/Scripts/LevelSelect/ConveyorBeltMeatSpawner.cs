using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltMeatSpawner : MonoBehaviour
{
    [SerializeField] private MeatGenerator _generator;

    private const float SPAWN_INTERVAL = 3f;
    private const int MAX_MEATS = 10;
    private float _startTime;
    private Queue<Steak> _meats;

    void Start()
    {
        _meats = new Queue<Steak>();
        AddMeat();
    }

    void Update()
    {
        if (Time.time >= _startTime + SPAWN_INTERVAL)
        {
            AddMeat();
        }
    }

    private void AddMeat()
    {
        Steak meat = _generator.GenerateMeat();
        meat.transform.SetParent(transform, true);
        _meats.Enqueue(meat);
        if (_meats.Count > MAX_MEATS)
        {
            Destroy(_meats.Dequeue().gameObject);
        }
        _startTime = Time.time;
    }
}
