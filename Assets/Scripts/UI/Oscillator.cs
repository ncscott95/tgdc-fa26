using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private float _offset;
    private Vector3 _origin;

    void Start()
    {
        _origin = transform.position;
        _offset = Random.Range(0.0f, Mathf.PI);
    }

    void Update()
    {
        transform.position = new Vector3(
            _origin.x, 
            _origin.y + (Mathf.Sin((_speed * Time.time) + _offset) * _distance),
            _origin.z
        );
    }
}
