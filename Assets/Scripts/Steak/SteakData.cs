using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SteakData
{
    // Gameplay data
    public List<Trait> Traits { get; private set; } = new List<Trait>();

    // Trait parameters
    public const int MaxTraits = 3;
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
    public List<PosRotScale> EyeTransforms = new List<PosRotScale>();
    public Vector2 TextureUVOffset { get; private set; }

    // Random generation parameters
    private const int MinNodes = 6;
    private const int MaxNodes = 12; // Inclusive
    private const float NoiseWeight = 0.3f;
    private const float BaseNodeDistance = 0.3f;
    public const float EyePositionRandomRadius = 0.1f;
    private const float MinEyeScale = 0.5f;
    private const float MaxEyeScale = 0.7f;

    // Flavor data
    public string Name = "";
    public List<string> Descriptors = new List<string>();

    // Flavor parameters
    // assumes steaks only have one descriptor from each pool
    private static readonly string[] DescriptorPoolA =
    {
        "Agile",
        "Angular",
        "Ashen",
        "Asymmetrical",
        "Athletic",
        "Bald",
        "Bearded",
        "Blotchy",
        "Bony",
        "Brittle",
        "Broad",
        "Bulky",
        "Burly",
        "Bushy",
        "Chubby",
        "Chunky",
        "Coarse",
        "Curvy",
        "Dainty",
        "Dark",
        "Delicate",
        "Dry",
        "Dull",
        "Flabby",
        "Flat",
        "Flawless",
        "Frail",
        "Freckled",
        "Frizzy",
        "Furry",
        "Gaunt",
        "Glossy",
        "Graceful",
        "Greasy",
        "Hairy",
        "Hollow",
        "Lanky",
        "Lean",
        "Lithe",
        "Lush",
        "Messy",
        "Muscular",
        "Oily",
        "Pale",
        "Petite",
        "Plump",
        "Porcelain",
        "Portly",
        "Prominent",
        "Puffy",
        "Radiant",
        "Robust",
        "Rosy",
        "Rugged",
        "Sallow",
        "Scarred",
        "Scrawny",
        "Scruffy",
        "Shaggy",
        "Shapely",
        "Shiny",
        "Short",
        "Silky",
        "Sinewy",
        "Sleek",
        "Slender",
        "Slim",
        "Smooth",
        "Spiky",
        "Spotty",
        "Stocky",
        "Stout",
        "Straight",
        "Gay",
        "Bisexual",
        "Strapping",
        "Sturdy",
        "Symmetrical",
        "Stale",
        "Musty",
        "Dusty",
        "Tall",
        "About to expire",
        "Thick",
        "Thin",
        "Toned",
        "Trim",
        "Voluminous",
        "Voluptuous",
        "Weak",
        "Weathered",
        "Wiry",
        "Wrinkly",
        "Firm",
        "Itchy",
        "Wobbly",
        "Gelatinous",
        "Putrid",
        "Rotten", 
        "Sour",
        "Rancid",
        "Stringy",
        "Palid", 
        "Wet", 
        "Goopy", 
        "Dry",
        "Glooby", 
        "Slimy", 
        "Stinky", 
        "Smelly", 
        "Maggot-filled",
        "Fragile", 
        "Sturdy", 
        "Leathery", 
        "Chewy", 
        "Juicy", 
        "Squishy", 
        "Slippery", 
        "Gooey"
    };
    private static readonly string[] DescriptorPoolB =
    {
        "Funny",
        "Kind", 
        "Stupid",
        "Brilliant",
        "Thoughtful",
        "Enthusiastic",
        "Bubbly",
        "Wise",
        "Diligent",
        "Curious",
        "Open-minded",
        "Witty",
        "Elegant",
        "Eccentric",
        "Artistic",
        "Sincere",
        "Trustworthy",
        "Friendly",
        "Loving",
        "Joyful",
        "Gentle",
        "Energetic",
        "Humble",
        "Tenacious",
        "Resilient",
        "Courageous",
        "Whimsical",
        "Understanding",
        "Sensitive",
        "Inspiring",
        "Depressed",
        "Self-centered",
        "Bossy",
        "Vain",
        "Selfish",
        "Brainless",
        "Moody",
        "Petulant",
        "Thoughtless",
        "Dense",
        "Awkward",
        "Cold",
        "Volatile",
        "Scatterbrained",
        "Obnoxious",
        "Fickle",
        "Conceited",
        "Stubborn",
        "Gullible",
        "Cowardly",
        "Petty",
        "Lazy",
        "Careless",
        "Unsophisticated",
        "Combative",
        "Insincere",
        "Crude",
        "Foolish",
        "Manipulative",
        "Immature",
        "Grouchy",
        "Bizarre",
        "Short-tempered",
        "Melodramatic",
        "Impolite",
        "Mean",
        "Arrogant",
        "Cruel",
        "Flaky",
        "Rude",
        "Hostile",
        "Bitter",
        "Incompetent",
        "Gloomy",
        "Reserved",
        "Nonchalant",
        "Discerning",
        "Rational",
        "Serious",
        "Collected",
        "Demure",
        "Realistic",
        "Composed",
        "Sensible",
        "Aloof",
        "Stoic",
        "Impartial",
        "Shy",
        "Pragmatic",
        "Mellow",
        "Tolerant",
        "Detached",
        "Skeptical",
        "Unassuming",
        "Firm",
        "Compliant",
        "Observant",
        "Mercurial",
        "Orderly",
        "Deliberate",
        "Cautious",
        "Efficient",
        "Discreet",
        "Inquisitive",
        "Quirky",
        "Extroverted",
        "Mysterious",
        "Civil",
        "Outspoken",
        "Evil",
        "Sad",
        "Grieving",
        "Hungry",
        "Starving",
        "Ravenous",
        "Peckish",
        "In love", 
        "Broken-hearted",
        "Groovy"
    };

    private static readonly System.Random rng = new System.Random();

    public SteakData(List<Trait> traits, List<Vector2> exteriorNodePositions, List<PosRotScale> eyeTransforms, Vector2 textureUVOffset, string name, List<string> descriptors)
    {
        Traits = traits;
        ExteriorNodePositions = exteriorNodePositions;
        EyeTransforms = eyeTransforms;
        TextureUVOffset = textureUVOffset;
        Name = name;
        Descriptors = descriptors;
    }

    public SteakData()
    {
        // Gameplay data

        // randomly select trait
        // assumes base-level steaks can only have 0 or 1 trait
        bool hasTrait = rng.NextDouble() <= TraitChance;
        if (hasTrait)
        {
            Traits.Add(CreateTrait(SelectRandomTrait()));
        }

        // Visual data

        // random offsets to sample into the perlin noise
        float noiseOffsetX = (float)rng.NextDouble() * 1000f;
        float noiseOffsetY = (float)rng.NextDouble() * 1000f;

        int numNodes = rng.Next(MinNodes, MaxNodes + 1);

        // apply random offsets to perimeter nodes
        for (int i = 0; i < numNodes; i++)
        {
            float angle = 2 * Mathf.PI / numNodes;
            Vector2 coords = new Vector2(Mathf.Cos(angle * i), Mathf.Sin(angle * i));
            float noise = (Mathf.PerlinNoise(coords.x + noiseOffsetX, coords.y + noiseOffsetY) - 0.5f) * NoiseWeight;
            ExteriorNodePositions.Add(new Vector2((BaseNodeDistance + noise) * coords.x, (BaseNodeDistance + noise) * coords.y));
        }


        // place eye at uniformly random point in circle
        float eyePosRadius = EyePositionRandomRadius * Mathf.Sqrt((float)rng.NextDouble());
        float eyePosTheta = (float)rng.NextDouble() * 2 * Mathf.PI;
        PosRotScale eyeTransform = new PosRotScale
        {
            Position = new Vector2(eyePosRadius * Mathf.Cos(eyePosTheta), eyePosRadius * Mathf.Sin(eyePosTheta)),
            RotationAngle = (float)rng.NextDouble() * 360f,
            Scale = (float)rng.NextDouble() * (MaxEyeScale - MinEyeScale) + MinEyeScale
        };
        EyeTransforms.Add(eyeTransform);

        foreach (Trait trait in Traits)
        {
            if (trait is ISizeBehavior sizeTrait)
            {
                sizeTrait.Invoke(this);
            }
        }

        TextureUVOffset = new Vector2((float)rng.NextDouble(), (float)rng.NextDouble());

        // Flavor data

        Descriptors.Add(DescriptorPoolA[rng.Next(0, DescriptorPoolA.Length)]);
        Descriptors.Add(DescriptorPoolB[rng.Next(0, DescriptorPoolB.Length)]);
    }

    private static TraitID SelectRandomTrait()
    {
        float totalWeight = 0f;
        foreach (KeyValuePair<TraitID, float> weightPair in TraitWeights)
        {
            if (weightPair.Value <= 0f) continue;
            totalWeight += weightPair.Value;
        }

        float random = (float)rng.NextDouble() * totalWeight;

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

[System.Serializable]
public struct PosRotScale
{
    public Vector2 Position;
    public float RotationAngle;
    public float Scale;
}
