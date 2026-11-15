using System.Collections.Generic;
using UnityEngine;

public class SteakData
{
    // Gameplay data
    public float Speed { get; private set; }
    public float Stickiness { get; private set; }
    public float Weight { get; private set; }
    public float Tenderness { get; private set; }
    public List<Trait> Traits { get; private set; } = new List<Trait>();

    // Trait parameters
    private const int MaxTraits = 3;
    private const float TraitChance = 0.25f;
    private static readonly KeyValuePair<TraitID, float>[] TraitWeights =
    {
        new(TraitID.Sticky, 1f),
        new(TraitID.Slippery, 1f),
        new(TraitID.Bouncy, 1f),
        new(TraitID.Wriggly, 1f),
        new(TraitID.Unstable, 0f), // unstable trait disabled, not implemented
        new(TraitID.Jumpy, 1f),
        new(TraitID.Tiny, 1f),
        new(TraitID.Giant, 1f),
        new(TraitID.Heavy, 1f),
        new(TraitID.Light, 1f),
    };

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
        // Gameplay data

        // randomly select trait
        // assumes base-level steaks can only have 0 or 1 trait
        bool hasTrait = Random.value <= TraitChance;
        if (hasTrait)
        {
            Traits.Add(CreateTrait(SelectRandomTrait()));
        }

        // Visual data

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

        foreach (Trait trait in Traits)
        {
            if (trait is ISizeBehavior sizeTrait)
            {
                sizeTrait.Invoke(this);
            }
        }

        TextureUVOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    private static TraitID SelectRandomTrait()
    {
        float totalWeight = 0f;
        foreach (KeyValuePair<TraitID, float> weightPair in TraitWeights)
        {
            if (weightPair.Value <= 0f) continue;
            totalWeight += weightPair.Value;
        }

        float random = Random.Range(0f, totalWeight);

        foreach (KeyValuePair<TraitID, float> weightPair in TraitWeights)
        {
            if (weightPair.Value <= 0f) continue;

            random -= weightPair.Value;
            if (random < 0f)
            {
                Debug.Log($"Selected trait: {weightPair.Key}");
                return weightPair.Key;
            }
        }

        // failsafe return last id in list
        return TraitWeights[^1].Key;
    }

    private Trait CreateTrait(TraitID id)
    {
        return id switch
        {
            TraitID.Sticky => new StickyTrait(),
            TraitID.Slippery => new SlipperyTrait(),
            TraitID.Bouncy => new BouncyTrait(),
            TraitID.Wriggly => new WrigglyTrait(),
            TraitID.Unstable => new UnstableTrait(),
            TraitID.Jumpy => new JumpyTrait(),
            TraitID.Tiny => new TinyTrait(),
            TraitID.Giant => new GiantTrait(),
            TraitID.Heavy => new HeavyTrait(),
            TraitID.Light => new LightTrait(),
            _ => throw new System.ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }
}

public struct PosRotScale
{
    public Vector2 Position;
    public float RotationAngle;
    public float Scale;
}
