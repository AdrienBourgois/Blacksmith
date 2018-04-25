using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Game.Scripts.Sounds
{
    public class SceneSound3D : MonoBehaviour
    {
        [EventRef]
        [SerializeField]
        private string soundEvent;

        [SerializeField]
        [Range(0f, 1f)]
        private float volume = 0.5f;

        private EventInstance soundInstance;

        private void Start()
        {
            soundInstance = RuntimeManager.CreateInstance(soundEvent);
            soundInstance.start();

            Vector3 position = transform.position;
            position.z = UnityEngine.Camera.main.transform.position.z;
            transform.position = position;
        }

        private void Update()
        {
            soundInstance.set3DAttributes(transform.To3DAttributes());
            soundInstance.setVolume(volume);
        }

        private void OnDestroy()
        {
            soundInstance.stop(STOP_MODE.ALLOWFADEOUT);
            soundInstance.release();
        }
    }
}
