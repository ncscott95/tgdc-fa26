using UnityEngine;

public class SteakGrinder : MonoBehaviour
{
    [SerializeField] private SteakContainerTrigger _container;

    public void OnPullLever()
    {
        Steak[] steaksToGrind = new Steak[_container.ContainedSteaks.Count];
        _container.ContainedSteaks.CopyTo(steaksToGrind);

        for (int i = 0; i < steaksToGrind.Length; i++)
        {
            Steak steak = steaksToGrind[i];
            // TODO: remove steak from player data
            Destroy(steak.gameObject);
        }

        // TODO: play particles, apply force to nearby steaks
    }
}
