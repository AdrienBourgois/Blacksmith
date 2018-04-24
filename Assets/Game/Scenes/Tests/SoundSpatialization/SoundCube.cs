using FMOD.Studio;
using UnityEngine;

namespace Game.Scenes.Tests.SoundSpatialization
{
    public class SoundCube : MonoBehaviour
    {
        [FMODUnity.EventRef]
        [SerializeField]
        private string soundEvent;

        private EventInstance soundInstance;

        [SerializeField]
        private float speed = 3f;

        [SerializeField]
        private float maxX = 3f;

        private void Start()
        {
            soundInstance = FMODUnity.RuntimeManager.CreateInstance(soundEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundInstance, transform, (Rigidbody) null);
        }

        private void Update()
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            if (transform.position.x >= maxX)
                transform.position = Vector3.left * maxX;

            if(Input.GetKey(KeyCode.P))
                soundInstance.start();
            if (Input.GetKey(KeyCode.S))
                soundInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }

        private void OnDestroy()
        {
            soundInstance.release();
        }
    }
}
