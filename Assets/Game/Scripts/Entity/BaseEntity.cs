using System.Collections;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class BaseEntity : SceneObjects.MovablePhysicSceneObject, IDamagable
    {
        [SerializeField] protected float recoveryTime;
        
        [SerializeField] protected float maxHealth;

        [SerializeField] private Color recoveryColor;

        private Color sColor;

        protected float health;

        protected bool inRecovery;

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

            if (Input.GetKeyDown(KeyCode.Space))
                ReceiveDamages(3);
        }

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