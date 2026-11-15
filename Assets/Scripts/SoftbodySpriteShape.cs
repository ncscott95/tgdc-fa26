using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class SoftbodySpriteShape : MonoBehaviour, 
    IPointerClickHandler, 
    IPointerEnterHandler,
    IPointerExitHandler,
    IDragHandler,
    IEndDragHandler
{
    private const float SplineOffset = 0.5f;
    private const float SplineHeight = 0.1f;
    private const int MaxPoints = 32; // Must match HLSL array size

    [SerializeField] private List<Transform> _nodes;
    [SerializeField] private Steak _steak;
    [SerializeField] private PolygonCollider2D _polygonCollider;
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _center;

    private float _tangentScale = 0.15f;
    private SpriteShapeController _controller;
    private SpriteShapeRenderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private Vector4[] _restPositions = new Vector4[MaxPoints];
    private Vector4[] _currentPositions = new Vector4[MaxPoints];
    private CircleCollider2D[] _colliders;
    private Vector2 _randomUVOffset;
    private bool _isInitialized = false;

    void Awake()
    {
        _controller = GetComponentInChildren<SpriteShapeController>();
        _renderer = GetComponentInChildren<SpriteShapeRenderer>();
        _propBlock = new MaterialPropertyBlock();
        _colliders = new CircleCollider2D[_nodes.Count];

        _renderer.enabled = false;

        // for (int i = 0; i < _nodes.Count; i++)
        // {
        //     _colliders[i] = _nodes[i].GetComponent<CircleCollider2D>();
        // }

        // InitializeVertices();
        // UpdateVertices();
    }

    void Update()
    {
        if (!_isInitialized) return;
        UpdateVertices();
    }

    public void SetPoints(List<Transform> reversePoints)
    {
        _nodes = reversePoints;
        _nodes.Reverse();
        _colliders = new CircleCollider2D[_nodes.Count];

        for (int i = 0; i < _nodes.Count; i++)
        {
            _colliders[i] = _nodes[i].GetComponent<CircleCollider2D>();
        }
    }

    public void SetTangentScale(float scale)
    {
        _tangentScale = scale;
    }

    public void InitializeVertices()
    {
        _controller.spline.Clear();
        float angle = 2f * Mathf.PI / _nodes.Count;
        for (int i = 0; i < _nodes.Count; i++)
        {
            Transform node = _nodes[i];
            Vector2 position = node.localPosition;
            _controller.spline.InsertPointAt(i, position);
            _controller.spline.SetTangentMode(i, ShapeTangentMode.Continuous);

            Vector2 rightTangent = Vector2.Perpendicular(-position.normalized) * _tangentScale;
            _controller.spline.SetRightTangent(i, rightTangent);
            _controller.spline.SetLeftTangent(i, -rightTangent);
            _controller.spline.SetHeight(i, SplineHeight);

            // store initial rest position
            Vector2 towardsCenter = -position.normalized;
            Vector2 restPos = position - towardsCenter * _colliders[i].radius;
            _restPositions[i] = new Vector4(restPos.x, restPos.y, 0, 0);
        }

        _randomUVOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        _renderer.enabled = true;
        _isInitialized = true;
    }

    private void UpdateVertices()
    {
        int count = Mathf.Min(_nodes.Count, MaxPoints);

        _polygonCollider.SetPath(0, _points
            .Select(t => new Vector2(t.localPosition.x, t.localPosition.y))
            .ToArray()
        );

        for (int i = 0; i < count; i++)
        {
            Vector2 vertex = _nodes[i].localPosition;
            Vector2 towardsCenter = -vertex.normalized;
            float colliderRadius = _colliders[i].radius;
            Vector2 finalSplinePos;

            try
            {
                finalSplinePos = vertex - towardsCenter * colliderRadius;
                _controller.spline.SetPosition(i, finalSplinePos);
            }
            catch
            {
                finalSplinePos = vertex - towardsCenter * (colliderRadius + SplineOffset);
                _controller.spline.SetPosition(i, finalSplinePos);
            }

            _currentPositions[i] = new Vector4(finalSplinePos.x, finalSplinePos.y, 0, 0);

            Vector2 leftTangent = _controller.spline.GetLeftTangent(i);
            Vector2 newRightTangent = Vector2.Perpendicular(towardsCenter) * leftTangent.magnitude;

            _controller.spline.SetRightTangent(i, newRightTangent);
            _controller.spline.SetLeftTangent(i, -newRightTangent);
        }

        _renderer.GetPropertyBlock(_propBlock, 0);
        _propBlock.SetVector("_Offset", _randomUVOffset);
        _propBlock.SetInt("_PointCount", count);
        _propBlock.SetVectorArray("_RestPoints", _restPositions);
        _propBlock.SetVectorArray("_CurrentPoints", _currentPositions);
        _renderer.SetPropertyBlock(_propBlock, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InteractionManager.Instance.Click(_steak);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InteractionManager.Instance.Hover(_steak);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InteractionManager.Instance.Unhover(_steak);
    }

    public void OnDrag(PointerEventData eventData)
    {
        InteractionManager.Instance.Drag(_steak, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InteractionManager.Instance.EndDrag(_steak);
    }
}
