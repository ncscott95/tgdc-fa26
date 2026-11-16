using UnityEngine;

public class BeltWheel : MonoBehaviour
{
    private const float ROTATE_SPEED = 50f;

    void Start()
    {
        transform.Rotate(Vector3.forward, Random.Range(0f, 360f));
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, -ROTATE_SPEED * Time.deltaTime);
    }
}
