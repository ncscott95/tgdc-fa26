using UnityEngine;
using UnityEngine.U2D;

public class SoftbodySpriteShape : MonoBehaviour
{
    private const float SplineOffset = 0.5f;
    private const float SplineHeight = 0.4f;
    private const int MaxPoints = 32; // Must match HLSL array size

    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _center;

    private Material _material;

    private SpriteShapeRenderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private Vector4[] _restPositions = new Vector4[MaxPoints];
    private Vector4[] _currentPositions = new Vector4[MaxPoints];
    private CircleCollider2D[] _colliders;
    private Vector2 _randomUVOffset;

    void Awake()
    {
        _renderer = GetComponentInChildren<SpriteShapeRenderer>();
        _propBlock = new MaterialPropertyBlock();
        _colliders = new CircleCollider2D[_points.Length];

        for (int i = 0; i < _points.Length; i++)
        {
            _colliders[i] = _points[i].GetComponent<CircleCollider2D>();
        }

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
            _spriteShapeController.spline.SetHeight(i, SplineHeight);

            // store initial rest position
            Vector2 towardsCenter = -position.normalized;
            Vector2 restPos = (Vector2)_points[i].localPosition - towardsCenter * _colliders[i].radius;
            _restPositions[i] = new Vector4(restPos.x, restPos.y, 0, 0);
        }

        _randomUVOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    private void UpdateVertices()
    {
        int count = Mathf.Min(_points.Length, MaxPoints);

        for (int i = 0; i < count; i++)
        {
            Vector2 vertex = _points[i].localPosition;
            Vector2 towardsCenter = -vertex.normalized;
            float colliderRadius = _colliders[i].radius;
            Vector2 finalSplinePos;

            try
            {
                finalSplinePos = vertex - towardsCenter * colliderRadius;
                _spriteShapeController.spline.SetPosition(i, finalSplinePos);
            }
            catch
            {
                finalSplinePos = vertex - towardsCenter * (colliderRadius + SplineOffset);
                _spriteShapeController.spline.SetPosition(i, finalSplinePos);
            }

            _currentPositions[i] = new Vector4(finalSplinePos.x, finalSplinePos.y, 0, 0);

            Vector2 leftTangent = _spriteShapeController.spline.GetLeftTangent(i);
            Vector2 newRightTangent = Vector2.Perpendicular(towardsCenter) * leftTangent.magnitude;

            _spriteShapeController.spline.SetRightTangent(i, newRightTangent);
            _spriteShapeController.spline.SetLeftTangent(i, -newRightTangent);
        }

        // Push deformation data to the Fill Material (index 0)
        _renderer.GetPropertyBlock(_propBlock, 0);
        _propBlock.SetVector("_Offset", _randomUVOffset);
        _propBlock.SetInt("_PointCount", count);
        _propBlock.SetVectorArray("_RestPoints", _restPositions);
        _propBlock.SetVectorArray("_CurrentPoints", _currentPositions);
        _renderer.SetPropertyBlock(_propBlock, 0);
    }
}
