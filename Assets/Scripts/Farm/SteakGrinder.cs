using UnityEngine;

public class SteakGrinder : MonoBehaviour
{
    [SerializeField] private SteakContainerTrigger _container;
    [SerializeField] private FMODUnity.EventReference _blendRef;
    [SerializeField] private ParticleSystem _upParticles;
    [SerializeField] private ParticleSystem _rightParticles;

    public void OnPullLever()
    {
        int steakCount = _container.ContainedSteaks.Count;

        for (int i = 0; i < steakCount; i++)
        {
            Steak steak = _container.ContainedSteaks[0];
            PersistentData.Instance.RemoveSteak(_container.ContainedSteaks[0].Data);
            Destroy(steak.gameObject);
        }

        if (steakCount > 0)
        {
            _upParticles.Play();
            _rightParticles.Play();
        }

        AudioManager.Instance.Play(_blendRef, transform.position);
        // TODO: play particles, apply force to nearby steaks
    }
}
