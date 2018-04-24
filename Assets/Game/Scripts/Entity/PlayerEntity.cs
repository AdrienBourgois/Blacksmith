using System.Collections;
using System.Collections.Generic;
using Game.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Entity
{
    public class PlayerEntity : AttackEntity
    {
        enum EPlayerState
        {
            NORMAL,
            KNOCKED_OUT
        }

        [SerializeField] private float reviveTimeTeammate;

        [SerializeField] private float reviveTime;

        private delegate void PlayerState();

        private event PlayerState KnockedOut;
        private event PlayerState Revived;

        private Slider healthSlider;

        private EPlayerState currentState = EPlayerState.NORMAL;

        private Coroutine inReviveCoroutine;

        #region Unity Methods

        protected override void Start()
        {
            base.Start();

            InputManager input_manager = FindObjectOfType<InputManager>();
            switch (soAttack.GetAttackType())
            {
                case SO_BaseAttack.EAttackType.DISTANCE:
                {
                    input_manager.SubscribeToHorizontalP1Event(ListenXAxis);
                    input_manager.SubscribeToVerticalP1Event(ListenZAxis);
                    input_manager.SubscribeToJumpReviveP1Event(Jump);
                    input_manager.SubscribeToWeakAttackP1Event(soAttack.LightGroundedAttack);
                    input_manager.SubscribeToStrongAttackP1Event(soAttack.HeavyGroundedAttack);

                        healthSlider = GameObject.FindGameObjectWithTag("P1_healthSlider").GetComponent<Slider>();
                    break;
                }
                case SO_BaseAttack.EAttackType.CAC:
                {
                    input_manager.SubscribeToHorizontalP2Event(ListenXAxis);
                    input_manager.SubscribeToVerticalP2Event(ListenZAxis);
                    input_manager.SubscribeToJumpReviveP2Event(Jump);
                    input_manager.SubscribeToWeakAttackP2Event(soAttack.LightGroundedAttack);
                    input_manager.SubscribeToStrongAttackP2Event(soAttack.HeavyGroundedAttack);

                        healthSlider = GameObject.FindGameObjectWithTag("P2_healthSlider").GetComponent<Slider>();
                    break;
                }
            }

            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;

            KnockedOut += () => { inReviveCoroutine = StartCoroutine(ToReviveState()); };
            KnockedOut += () => { ++FindObjectOfType<EntityManager>().PlayerKncokedDown; };
        }

        protected override void Update()
        {
            base.Update();
        }

        #endregion

        #region IDamagable

        public override void ReceiveDamages(int _damages)
        {
            base.ReceiveDamages(_damages);

            healthSlider.value = health;
        }

        public override void Die()
        {
            base.Die();

            switch (currentState)
            {
                case EPlayerState.NORMAL:
                {
                    if (KnockedOut != null)
                        KnockedOut();
                    break;
                }
                case EPlayerState.KNOCKED_OUT:
                {
                    Destroy(gameObject);
                    break;
                }
            }
        }

        #endregion

        #region Revive

        private IEnumerator ToReviveState()
        {
            currentState = EPlayerState.KNOCKED_OUT;

            float time = reviveTime;
            while (time >= 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            Die();
        }

        private void RevivePlayer()
        {

        }

        #endregion

    }
}