using UnityEngine;

public class JumpyTrait : Trait, IUpdateBehavior
{
    private const float JumpForce = 7500f;
    private const float MinJumpCooldown = 4f;
    private const float MaxJumpCooldown = 8f;
    private float _timer = 0f;
    private float _jumpCooldown = 0f;
    private bool _isInitialized = false;

    public JumpyTrait() : base(TraitID.Jumpy, "Jumpy") { }

    public void Invoke(Rigidbody2D rigidbody2D)
    {
        if (!_isInitialized)
        {
            _jumpCooldown = Random.Range(0, MinJumpCooldown);
            _isInitialized = true;
        }

        _timer += Time.deltaTime;

        if (_timer >= _jumpCooldown)
        {
            Vector2 jumpDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            rigidbody2D.AddForce(jumpDirection * JumpForce, ForceMode2D.Impulse);

            _jumpCooldown = Random.Range(MinJumpCooldown, MaxJumpCooldown);
            _timer = 0f;
        }
    }
}
