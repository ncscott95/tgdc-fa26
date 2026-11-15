using UnityEngine;

public class WrigglyTrait : Trait, IUpdateBehavior
{
    private const float Frequency = 2f;
    private const float Amplitude = 10000f;
    private float _seedX = -1f;
    private float _seedY = -1f;

    public WrigglyTrait() : base(TraitID.Wriggly, "Wriggly") { }

    public void Invoke(Rigidbody2D rigidbody2D)
    {
        if (_seedX == -1f && _seedY == -1f)
        {
            _seedX = Random.Range(0f, 100f);
            _seedY = Random.Range(0f, 100f);
        }

        float time = Time.time * Frequency;

        float wriggleX = (Mathf.PerlinNoise(_seedX, time) - 0.5f) * 2f;
        float wriggleY = (Mathf.PerlinNoise(_seedY, time) - 0.5f) * 2f;

        Vector2 force = new Vector2(wriggleX, wriggleY) * Amplitude;
        rigidbody2D.AddForce(force, ForceMode2D.Force);
    }
}
