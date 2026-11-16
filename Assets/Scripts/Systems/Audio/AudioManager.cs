using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : Singleton<AudioManager>
{
    private FMOD.GUID _currentMusicID;
    private EventInstance _musicInstance;

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

    public void PlayMusic(EventReference eventReference)
    {
        if (eventReference.Guid != _currentMusicID)
        {
            StopMusic();
            _currentMusicID = eventReference.Guid;
            _musicInstance = RuntimeManager.CreateInstance(eventReference);
            _musicInstance.start();
        }
    }

    public void StopMusic()
    {
        if (_musicInstance.isValid())
        {
            _musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _musicInstance.release();
        }
    }
}
