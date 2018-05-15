using UnityEngine;

namespace Game.Scripts.Triggers.ZoneTrigger
{
    public class ZoneTrigger : MonoBehaviour
    {
        public delegate void ZoneTriggerDelegate(Collider2D _collider, ZoneTrigger _trigger);

        [SerializeField] private EZoneTriggerType type;
        private ZoneTriggerDelegate onStayZoneCallback;

        public EZoneTriggerType Type { get { return type; } }

        public void SubscribeToonStayZoneCallback(ZoneTriggerDelegate _function_pointer)
        {
            onStayZoneCallback += _function_pointer;
        }

        public void UnsubscribeFromonStayZoneCallback(ZoneTriggerDelegate _function_pointer)
        {
            onStayZoneCallback -= _function_pointer;
        }

        private void OnTriggerStay2D(Collider2D _other)
        {
            if (onStayZoneCallback != null)
                onStayZoneCallback(_other, this);
        }
    }
}
