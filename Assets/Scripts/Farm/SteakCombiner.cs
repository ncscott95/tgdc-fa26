using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakCombiner : MonoBehaviour
{
    [SerializeField] private SteakContainerTrigger _containerLeft;
    [SerializeField] private SteakContainerTrigger _containerRight;
    [SerializeField] private NewSteakButton _newSteakButton;
    [SerializeField] private SpriteRenderer _windowGlowEffect;
    [SerializeField] private FMODUnity.EventReference _mergeSfx;
    private const float GLOW_DURATION = 1f;
    private Color _glowColor = new Color(0.827f, 0.855f, 0.239f, 1.0f);
    private bool _canMerge = true;

    public void OnPushButton()
    {
        if (_canMerge && (_containerLeft.ContainedSteaks.Count != 1 || _containerRight.ContainedSteaks.Count != 1))
        {
            // TODO: play fail SFX?
            return;
        }

        Steak leftSteak = _containerLeft.ContainedSteaks[0];
        Steak rightSteak = _containerRight.ContainedSteaks[0];

        _canMerge = false;
        StartCoroutine(GlowRoutine(leftSteak, rightSteak));
    }

    private SteakData CombineSteakData(SteakData left, SteakData right)
    {
        List<Trait> combinedTraits = new List<Trait>();
        combinedTraits.AddRange(left.Traits);
        combinedTraits.AddRange(right.Traits);
        while (combinedTraits.Count > SteakData.MaxTraits)
        {
            combinedTraits.Remove(combinedTraits[Random.Range(0, combinedTraits.Count)]);
        }

        // TODO: generate fancy new exterior node positions
        List<Vector2> nodes = Random.value > 0.5f ? left.ExteriorNodePositions : right.ExteriorNodePositions;

        // TODO: add logic to prevent eyes from overlapping
        List<PosRotScale> eyes = new List<PosRotScale>();
        eyes.AddRange(left.EyeTransforms);
        eyes.AddRange(right.EyeTransforms);
        int totalEyes = eyes.Count;
        for (int i = 0; i < totalEyes; i++)
        {
            float eyePosRadius = 2 * SteakData.EyePositionRandomRadius * Mathf.Sqrt(Random.value);
            float eyePosTheta = 2 * i * Mathf.PI / totalEyes;
            PosRotScale newTransform = new PosRotScale
            {
                Position = new Vector2(eyePosRadius * Mathf.Cos(eyePosTheta), eyePosRadius * Mathf.Sin(eyePosTheta)),
                RotationAngle = eyes[i].RotationAngle,
                Scale = eyes[i].Scale
            };
            eyes[i] = newTransform;
        }

        Vector2 uvOffset = Random.value > 0.5f ? left.TextureUVOffset : right.TextureUVOffset;
        List<string> descriptors = new List<string>()
        {
            Random.value > 0.5f ? left.Descriptors[0] : right.Descriptors[0],
            Random.value > 0.5f ? left.Descriptors[1] : right.Descriptors[1]
        };

        SteakData combined = new SteakData(combinedTraits, nodes, eyes, uvOffset, "", descriptors);
        return combined;
    }

    private IEnumerator GlowRoutine(Steak leftSteak, Steak rightSteak)
    {
        AudioManager.Instance.Play(_mergeSfx);

        yield return BasicAnimations.Interpolate(
            () => _windowGlowEffect.gameObject.SetActive(true),
            t =>
            {
                float smooth = BasicAnimations.Smooth(t);
                _windowGlowEffect.color = new Color(_glowColor.r, _glowColor.g, _glowColor.b, smooth);
            },
            null,
            GLOW_DURATION / 2f
        );

        Destroy(leftSteak.gameObject);
        Destroy(rightSteak.gameObject);

        yield return BasicAnimations.Interpolate(
            null,
            t =>
            {
                float smooth = BasicAnimations.Smooth(t);
                _windowGlowEffect.color = new Color(_glowColor.r, _glowColor.g, _glowColor.b, 1.0f - smooth);
            },
            () => _windowGlowEffect.gameObject.SetActive(false),
            GLOW_DURATION / 2f
        );

        SteakData combinedData = CombineSteakData(leftSteak.Data, rightSteak.Data);

        // TODO: remove steaks from player data
        PersistentData.Instance.RemoveSteak(leftSteak.Data);
        PersistentData.Instance.RemoveSteak(rightSteak.Data);
        
        _newSteakButton.NewCombinedSteak(combinedData);

        _canMerge = true;
    }
}
