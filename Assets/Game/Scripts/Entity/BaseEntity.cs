using System.Collections;
using Game.Scripts.Interfaces;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class BaseEntity : SceneObjects.MovablePhysicSceneObject, IDamagable, IAttack
    {
        [SerializeField] protected SoBaseAttack lightAttack;
        [SerializeField] protected SoBaseAttack heavyAttack;

        [SerializeField] protected float recoveryTime;
        [SerializeField] protected float maxHealth;

        [SerializeField] private Color recoveryColor;

        private bool isInCooldown;
        private bool isAttacking;

        protected bool inRecovery;

        private Color sColor;

        protected float health;

        private Coroutine recoveryCor;

        // Use this for initialization
        protected override void Start()
        {
            sColor = GetComponent<SpriteRenderer>().color;
            health = maxHealth;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected override void ListenXAxis(float _value)
        {
            if (!isAttacking)
                base.ListenXAxis(_value);

        }

        protected override void ListenZAxis(float _value)
        {
            if (!isAttacking)
                base.ListenZAxis(_value);
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
            TimerManager.Instance.AddTimer("Cooldown", _cooldown, true, false, () => isInCooldown = false);
        }

        public virtual void DamageEntity(BaseEntity _entity, float _damages)
        {
            _entity.ReceiveDamages(_damages);
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
                StartRecovery();
            }
        }

        public void StartRecovery()
        {
            recoveryCor = StartCoroutine(RecoveryCoroutine());
        }

        IEnumerator RecoveryCoroutine()
        {
            inRecovery = true;
            float time = recoveryTime;

            GetComponent<SpriteRenderer>().color = recoveryColor;

            while (time >= 0)
            {

                time -= Time.deltaTime;
                yield return null;
            }

            inRecovery = false;
            GetComponent<SpriteRenderer>().color = sColor;
        }

        public virtual void Die()
        {
            if (recoveryCor != null)
                StopCoroutine(recoveryCor);
        }

        /*private void OnCollisionEnter2D(Collision2D other)
        {
            print("Detected");
        }*/

        #endregion

    }
}