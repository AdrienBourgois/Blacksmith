using FMOD.Studio;
using UnityEngine;

namespace Game.Scripts.Sounds
{
    public class MusicManager : MonoBehaviour
    {
        [FMODUnity.EventRef]
        [SerializeField]
        private string musicEvent;

        [SerializeField]
        [Range(0f, 1f)]
        private float volume = 0.5f;

        [SerializeField]
        [Range(0f, 3f)]
        private float parameter = 3f;

        private EventInstance musicInstance;

        private void Start()
        {
            musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
            musicInstance.start();

            musicInstance.setParameterValue("DynamicMusic", parameter);
        }

        private void Update()
        {
            musicInstance.setVolume(volume);
        }

        private void OnDestroy()
        {
            musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
        }
    }
}
