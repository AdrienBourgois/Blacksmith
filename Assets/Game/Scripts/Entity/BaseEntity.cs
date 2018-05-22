using Game.Scripts.AttackBehavior;
using Game.Scripts.Interfaces;
using Game.Scripts.SceneObjects;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class BaseEntity : MovablePhysicSceneObject, IDamagable, IAttack
    {
        [Header("Attacks")]
        [SerializeField] protected SoBaseAttack lightAttack;
        [SerializeField] protected SoBaseAttack heavyAttack;

        [Header("Stats")]
        [SerializeField] protected float recoveryTime;
        [SerializeField] protected float maxHealth;

        [SerializeField] private Color recoveryColor;

        private bool isInCooldown;
        protected bool isAttacking;

        private int cooldownTimerId;
        private int recoveryTimerId;

        protected bool inRecovery;
        public bool IsInRecovery
        {
            get { return inRecovery; }
        }

        private float health;
        public float Health
        {
            get { return health; }
            set
            {
                if (value <= maxHealth)
                    health = value;
            }
        }

        private Color sColor;


        private Coroutine recoveryCor;

        // Use this for initialization
        protected override void Start()
        {
            sColor = GetComponent<SpriteRenderer>().color;
            Health = maxHealth;
        }

        protected override void Jump()
        {
            if (!isAttacking)
                base.Jump();
        }

        #region IAttack
        public void LightGroundedAttack()
        {
            if (CanAttack())
                lightAttack.Attack(this);
        }

        public void HeavyGroundedAttack()
        {
            if (CanAttack())
                heavyAttack.Attack(this);
        }

        public bool CanAttack()
        {
            return !isInCooldown && !isAttacking;
        }

        public void StartCooldown(float _cooldown)
        {
            isInCooldown = true;
            cooldownTimerId = TimerManager.Instance.AddTimer("Cooldown", _cooldown, true, false, () => isInCooldown = false);
        }

        public virtual void DamageEntity(BaseEntity _entity, BaseBehavior.HitData _data)
        {
            _entity.ReceiveDamages(_data.damages);
        }

        public virtual float GetXScale()
        {
            return transform.localScale.x;
        }
        #endregion

        #region IDamagable
        public virtual void ReceiveDamages(float _damages)
        {
            if (inRecovery)
                return;

            Health -= _damages;

            if (Health <= 0)
                Die();
            else
            {
                inRecovery = true;
                GetComponent<SpriteRenderer>().color = recoveryColor;

                recoveryTimerId = TimerManager.Instance.AddTimer("Recovery Timer", recoveryTime, true, false, () =>
                {
                    GetComponent<SpriteRenderer>().color = sColor;
                    inRecovery = false;
                });

                //StartRecovery();
            }
        }

        public virtual void Die()
        {
            if (recoveryCor != null)
                StopCoroutine(recoveryCor);
        }
        #endregion

    }
}