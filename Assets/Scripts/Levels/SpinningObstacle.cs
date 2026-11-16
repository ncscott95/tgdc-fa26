using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}
