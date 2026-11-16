using UnityEngine;

public class CrusherObstacle : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] private float _idleX;
    [SerializeField] private float _closedX;
   
    [Header("Timing")]
    [SerializeField] private float _idleDuration;
    [SerializeField] private float _closingDuration;
    [SerializeField] private float _closedDuration;
    [SerializeField] private float _openingDuration;

    [Header("Objects")]
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;

    private float _timer = 0f;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        float xPos = _idleX;
        if (_timer < _idleDuration)
        {
            xPos = _idleX;
        }
        else if (_timer < _idleDuration + _closingDuration)
        {
            float t = (_timer - _idleDuration) / _closingDuration;
            xPos = Mathf.SmoothStep(_idleX, _closedX, t);
        }
        else if (_timer < _idleDuration + _closingDuration + _closedDuration)
        {
            xPos = _closedX;
        }
        else if (_timer < _idleDuration + _closingDuration + _closedDuration + _openingDuration)
        {
            float t = (_timer - _idleDuration - _closingDuration - _closedDuration) / _openingDuration;
            xPos = Mathf.SmoothStep(_closedX, _idleX, t);
        }
        else
        {
            _timer = 0f;
        }

        _left.localPosition = new Vector2(-xPos, 0f);
        _right.localPosition = new Vector2(xPos, 0f);
    }
}
