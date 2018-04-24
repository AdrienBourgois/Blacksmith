using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class BaseEntity : SceneObjects.MovablePhysicSceneObject, IDamagable
    {
        [SerializeField] protected float maxHealth;

        protected float health;

        // Use this for initialization
        protected override void Start()
        {
            health = maxHealth;
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ReceiveDamages(3);
        }

        #region IDamagable
        public virtual void ReceiveDamages(int _damages)
        {
            health -= _damages;
            if (health <= 0)
                Die();
        }

        public virtual void Die()
        {

        }

        private void OnTriggerEnter(Collider _other)
        {
            IAttack attack = _other.GetComponent<IAttack>();
            if (attack != null)
            {
                print("AIIIIE");
            }
        }

        #endregion

    }
}