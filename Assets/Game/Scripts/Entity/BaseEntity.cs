using Game.Scripts.Interfaces;
using Game.Scripts.SceneObjects;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class BaseEntity : MovablePhysicSceneObject, IDamagable, IAttack
    {
        [SerializeField] protected SoBaseAttack lightAttack;
        [SerializeField] protected SoBaseAttack heavyAttack;

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

        private Color sColor;

        protected float health;

        private Coroutine recoveryCor;

        // Use this for initialization
        protected override void Start()
        {
            sColor = GetComponent<SpriteRenderer>().color;
            health = maxHealth;
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

        public virtual void DamageEntity(BaseEntity _entity, float _damages)
        {
            _entity.ReceiveDamages(_damages);
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

            health -= _damages;

            if (health <= 0)
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