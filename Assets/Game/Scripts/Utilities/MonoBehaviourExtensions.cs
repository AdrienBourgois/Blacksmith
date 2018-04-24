using UnityEngine;

namespace Game.Scripts.Utilities
{
    public static class MonoBehaviourExtensions
    {
        public static void VerifyComponent<T>(this MonoBehaviour _mono_behaviour, T _component_reference) where T : Component
        {
            _component_reference = _mono_behaviour.GetComponent<T>();
            if (!_component_reference)
                _component_reference = _mono_behaviour.gameObject.AddComponent<T>();
        }
    }

}
