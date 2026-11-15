using UnityEngine;

public class Steak : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private const float DRAG_SPEED = 20f;
    private const float DRAG_RADIUS = 5f;

    public void Click()
    {
        
    }

    public void Drag(Vector2 targetPos)
    {
        Vector2 distance = targetPos - new Vector2(transform.position.x, transform.position.y);
        if (distance == Vector2.zero) return;
        Vector2 direction = distance.normalized;
        float t = Mathf.Clamp01(distance.magnitude / DRAG_RADIUS);
        _rb.linearVelocity = direction * Mathf.Lerp(3f, DRAG_SPEED, t);
    }
}
