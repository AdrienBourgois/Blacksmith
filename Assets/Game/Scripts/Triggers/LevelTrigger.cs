using Game.Scripts.Entity;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class LevelTrigger : MonoBehaviour
    {
        private Collider2D triggerCollider;
        private ITriggerAction[] actions;

        private PlayerEntity rangePlayer;
        private PlayerEntity meleePlayer;

        private void Start()
        {
            meleePlayer = EntityManager.Instance.MeleePlayer;
            rangePlayer = EntityManager.instance.RangePlayer;

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
            if (triggerCollider.OverlapPoint(meleePlayer.transform.position) || triggerCollider.OverlapPoint(rangePlayer.transform.position))
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
