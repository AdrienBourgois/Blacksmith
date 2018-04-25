﻿using Game.Scripts.Interfaces;
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
        public virtual void ReceiveDamages(float _damages)
        {
            health -= _damages;
            if (health <= 0)
                Die();
        }

        public virtual void Die()
        {

        }

        /*private void OnCollisionEnter2D(Collision2D other)
        {
            print("Detected");
        }*/

        #endregion

    }
}