using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

namespace Game.Scripts.SceneObjects
{
    public class BaseEntity : Game.Scripts.SceneObjects.MovablePhysicSceneObject, IDamagable
    {
        [SerializeField] protected float maxHealth;

        protected float health;

        // Use this for initialization
        protected virtual void Start()
        {
            health = maxHealth;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Space))
                ReceiveDamages(3);
        }

        public virtual void ReceiveDamages(int _damages)
        {
            health -= _damages;
            if (health <= 0)
                Die();
        }

        public virtual void Die()
        {

        }
    }
}