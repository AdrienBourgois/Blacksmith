using Game.Scripts.AI;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class EnemyEntity : BaseEntity
    {
        [SerializeField] private EnemyBehavior behavior;

        #region Unity Methods

        protected override void Start()
        {
            base.Start();

            behavior = new EnemyBehavior(this);
        }

        protected override void Update()
        {
            base.Update();

            behavior.Update();
        }

        #endregion
    }
}