using System;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Game.Scripts.Sounds
{
    public class SoundOneShot : MonoBehaviour
    {
        private EVENT_CALLBACK endCallback;
        private EventInstance soundInstance;

        public static void PlayOneShot(string _event, float _volume, Vector2 _position)
        {
            GameObject go = new GameObject("Sound " + _event);
            SoundOneShot sound = go.AddComponent<SoundOneShot>();
            sound.SetParameters(_event, _volume, _position);
        }

        private void SetParameters(string _event, float _volume, Vector2 _position)
        {
            endCallback = OnStopped;

            soundInstance = RuntimeManager.CreateInstance(_event);
            soundInstance.start();

            Vector3 position = _position;
            position.z = UnityEngine.Camera.main.transform.position.z;
            transform.position = position;

            soundInstance.set3DAttributes(transform.To3DAttributes());
            soundInstance.setVolume(_volume);
            soundInstance.setCallback(endCallback, EVENT_CALLBACK_TYPE.STOPPED);

        }

        [AOT.MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
        private RESULT OnStopped(EVENT_CALLBACK_TYPE _type, EventInstance _eventinstance, IntPtr _parameters)
        {
            soundInstance.stop(STOP_MODE.IMMEDIATE);
            soundInstance.release();
            Destroy(gameObject);
            return RESULT.OK;
        }
    }
}
