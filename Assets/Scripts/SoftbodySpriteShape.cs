using UnityEngine;
using UnityEngine.U2D;

public class SoftbodySpriteShape : MonoBehaviour
{
    private const float splineOffset = 0.5f;
    private const float splineHeight = 0.4f;
    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _center;

    private Material _material;

    void Awake()
    {
        InitializeVertices();
        UpdateVertices();
    }

    void Update()
    {
        UpdateVertices();
    }

    private void InitializeVertices()
    {
        _spriteShapeController.spline.Clear();
        float angle = 2f * Mathf.PI / _points.Length;
        for (int i = 0; i < _points.Length; i++)
        {
            Vector2 position = new Vector2(2f * Mathf.Cos(i * angle), 2f * Mathf.Sin(i * angle));
            _spriteShapeController.spline.InsertPointAt(i, position);
            _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);

            Vector2 rightTangent = Vector2.Perpendicular(-position.normalized) / 2f;
            _spriteShapeController.spline.SetRightTangent(i, rightTangent);
            _spriteShapeController.spline.SetLeftTangent(i, -rightTangent);
            _spriteShapeController.spline.SetHeight(i, splineHeight);
        }

        _material = GetComponent<Renderer>().material;
        Vector2 randomUVOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        _material.SetVector("_Offset", randomUVOffset);
        Debug.Log($"Setting UV offset to {randomUVOffset}");
    }

    private void UpdateVertices()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            Vector2 vertex = _points[i].localPosition;
            Vector2 towardsCenter = -vertex.normalized;
            float colliderRadius = _points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try
            {
                _spriteShapeController.spline.SetPosition(i, vertex - towardsCenter * colliderRadius);
            }
            catch
            {
                Debug.Log("Spline points are too close together, recalculate");
                _spriteShapeController.spline.SetPosition(i, vertex - towardsCenter * (colliderRadius + splineOffset));
            }

            Vector2 leftTangent = _spriteShapeController.spline.GetLeftTangent(i);
            Vector2 newRightTangent = Vector2.Perpendicular(towardsCenter) * leftTangent.magnitude;
            Vector2 newLeftTangent = -newRightTangent;

            _spriteShapeController.spline.SetRightTangent(i, newRightTangent);
            _spriteShapeController.spline.SetLeftTangent(i, newLeftTangent);
        }
    }
}
