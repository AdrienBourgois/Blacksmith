using UnityEngine;

namespace Game.Scripts.Sounds
{
    public class MusicManager : MonoBehaviour
    {
        [FMODUnity.EventRef]
        [SerializeField]
        private string musicEvent;

        private FMOD.Studio.EventInstance musicInstance;

        private void Start()
        {
            musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
            musicInstance.start();
        }

        private void OnDestroy()
        {
            musicInstance.release();
        }
    }
}
