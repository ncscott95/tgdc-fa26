using UnityEngine;

public class SteakGrinder : MonoBehaviour
{
    [SerializeField] private SteakContainerTrigger _container;

    public void OnPullLever()
    {
        int steakCount = _container.ContainedSteaks.Count;

        for (int i = 0; i < steakCount; i++)
        {
            Steak steak = _container.ContainedSteaks[0];
            PersistentData.Instance.RemoveSteak(_container.ContainedSteaks[0].Data);
            Destroy(steak.gameObject);
        }

        // TODO: play particles, apply force to nearby steaks
    }
}
