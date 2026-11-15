using System.Collections.Generic;
using UnityEngine;

public class SteakData
{
    public float Speed { get; private set; }
    public float Stickiness { get; private set; }
    public float Weight { get; private set; }
    public float Tenderness { get; private set; }

    // Visual data
    public List<Vector2> ExteriorNodePositions { get; private set; } = new List<Vector2>();
    public List<PosRotScale> EyeTransforms { get; private set; } = new List<PosRotScale>();
    public Vector2 TextureUVOffset;

    // Random generation parameters
    private const int MinNodes = 6;
    private const int MaxNodes = 12; // Inclusive
    private const float NoiseWeight = 0.3f;
    private const float BaseNodeDistance = 0.3f;
    private const float EyePositionRandomRadius = 0.1f;
    private const float MinEyeScale = 0.5f;
    private const float MaxEyeScale = 0.7f;

    public SteakData()
    {
        // random offsets to sample into the perlin noise
        float noiseOffsetX = Random.Range(0f, 1000f);
        float noiseOffsetY = Random.Range(0f, 1000f);

        int numNodes = Random.Range(MinNodes, MaxNodes + 1);

        // apply random offsets to perimeter nodes
        for (int i = 0; i < numNodes; i++)
        {
            float angle = 2 * Mathf.PI / numNodes;
            Vector2 coords = new Vector2(Mathf.Cos(angle * i), Mathf.Sin(angle * i));
            float noise = (Mathf.PerlinNoise(coords.x + noiseOffsetX, coords.y + noiseOffsetY) - 0.5f) * NoiseWeight;
            ExteriorNodePositions.Add(new Vector2((BaseNodeDistance + noise) * coords.x, (BaseNodeDistance + noise) * coords.y));
        }

        PosRotScale eyeTransform = new PosRotScale();

        // place eye at uniformly random point in circle
        float eyePosRadius = EyePositionRandomRadius * Mathf.Sqrt(Random.value);
        float eyePosTheta = Random.value * 2 * Mathf.PI;
        eyeTransform.Position = new Vector2(eyePosRadius * Mathf.Cos(eyePosTheta), eyePosRadius * Mathf.Sin(eyePosTheta));
        eyeTransform.RotationAngle = Random.Range(0f, 360f);
        eyeTransform.Scale = Random.Range(MinEyeScale, MaxEyeScale);

        EyeTransforms.Add(eyeTransform);

        TextureUVOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}

public struct PosRotScale
{
    public Vector2 Position;
    public float RotationAngle;
    public float Scale;
}
