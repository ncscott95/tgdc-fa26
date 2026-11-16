using UnityEngine;

public class SteakImpact : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference squelchRef;
    private const float THRESHOLD = 20000f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        float impulse = 0f;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            impulse += contact.normalImpulse;
        }

        float impactForce = impulse / Time.fixedDeltaTime;

        if (impactForce >= THRESHOLD)
        {
            float volume = Mathf.Clamp01(Mathf.InverseLerp(THRESHOLD, THRESHOLD * 5f, impactForce));
            AudioManager.Instance.Play(squelchRef, volume, collision.GetContact(0).point);
        }
    }
}
