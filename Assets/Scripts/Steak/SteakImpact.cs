using UnityEngine;

public class SteakImpact : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference squelchRef;
    [SerializeField] private GameObject particles;
    private Transform particleContainer;
    private const float THRESHOLD = 20000f;

    public void SetParticleContainer(Transform container)
    {
        particleContainer = container;
    }

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

            Vector2 normal = collision.GetContact(0).normal;
            float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;
            GameObject newParticle = Instantiate(particles, particleContainer);
            newParticle.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 0f, angle - 90f));
            newParticle.SetActive(true);
        }
    }
}
