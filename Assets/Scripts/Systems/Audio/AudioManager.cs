using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : Singleton<AudioManager>
{
    public void Play(EventReference eventReference)
    {
        RuntimeManager.PlayOneShot(eventReference);
    }

    public void Play(EventReference eventReference, float volume, Vector3 pos)
    {
        EventInstance instance = RuntimeManager.CreateInstance(eventReference);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(pos));
        instance.setVolume(volume);
        instance.start();
        instance.release();
    }

    public void PlaySquelch()
    {
        
    }
}
