using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.AI
{
    public class EnemyBehavior
    {
        #region States
        [HideInInspector] public IdleState idleState;
        [HideInInspector] public SelectTargetState selectTargetState;
        [HideInInspector] public ChaseState chaseState;
        [HideInInspector] public AttackState attackState;

        [HideInInspector] public IEnemyState currentState;
        #endregion

        [HideInInspector] public EnemyEntity myEntity;

        public EnemyBehavior(EnemyEntity _entity)
        {
            myEntity = _entity;

            idleState = new IdleState(this);
            selectTargetState = new SelectTargetState(this);
            attackState = new AttackState(this);

            currentState = idleState;
        }

        public void Update()
        {
            currentState.ToIdleState();
            currentState.ToSelectTargetState();
            currentState.ToChaseState();
            currentState.ToAttackState();

            currentState.Update();
        }
    }
}