using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class LevelTrigger : MonoBehaviour
    {
        private Collider2D triggerCollider;
        private ITriggerAction[] actions;

        [SerializeField]
        private GameObject player;

        private void Start()
        {
            actions = GetComponents<ITriggerAction>();

            if (actions.Length == 0)
            {
                Debug.LogWarning(gameObject.name + " : Any associated actions to trigger !");
                Destroy(this);
            }

            triggerCollider = GetComponent<PolygonCollider2D>();

            if(!triggerCollider)
            {
                Debug.LogWarning(gameObject.name + " : Any collider on LevelTrigger !");
                Destroy(this);
            }
        }

        private void Update()
        {
            if (triggerCollider.OverlapPoint(player.transform.position))
            {
                foreach (ITriggerAction action in actions)
                {
                    action.Trigger();
                }
                Destroy(this);
            }
        }
    }
}
