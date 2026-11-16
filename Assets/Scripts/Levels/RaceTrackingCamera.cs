using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class RaceTrackingCamera : MonoBehaviour
{
    private CinemachineCamera _camera;
    private List<Transform> _transforms = null;
    [SerializeField] private Transform _target;
    private float _lowestY = Mathf.Infinity;

    void Awake()
    {
        _camera = GetComponent<CinemachineCamera>();
        _camera.Follow = _target;
    }

    void Update()
    {
        if (_transforms == null) return;

        foreach (Transform t in _transforms)
        {
            if (t.position.y < _lowestY)
            {
                _lowestY = t.position.y;
            }
        }

        _target.position = new Vector2(0f, _lowestY);
    }

    public void Initialize(List<Transform> transforms)
    {
        _transforms = transforms;
    }
}
