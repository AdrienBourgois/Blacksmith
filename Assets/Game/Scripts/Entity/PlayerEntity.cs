using System;
using System.Collections;
using Game.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Entity
{
    public class PlayerEntity : AttackEntity
    {
        private enum EPlayerState
        {
            NORMAL,
            KNOCKED_OUT
        }

        [SerializeField] private float reviveTimeTeammate;

        [SerializeField] private float reviveTime;

        [SerializeField] private float maxFury;

        private float fury;

        private delegate void PlayerState();

        private event PlayerState KnockedOut;
        private event PlayerState Revived;

        private Slider healthSlider;
        private Slider furySlider;

        private EPlayerState currentState = EPlayerState.NORMAL;

        private Coroutine inReviveCoroutine;

        #region Unity Methods

        protected override void Start()
        {
            base.Start();

            InputManager.InputManager input_manager = FindObjectOfType<InputManager.InputManager>();
            switch (soAttack.GetAttackType())
            {
                case SoBaseAttack.EAttackType.DISTANCE:
                {
                    input_manager.SubscribeToHorizontalP1Event(ListenXAxis);
                    input_manager.SubscribeToVerticalP1Event(ListenZAxis);
                    input_manager.SubscribeToJumpReviveP1Event(Jump);
                    input_manager.SubscribeToWeakAttackP1Event(soAttack.LightGroundedAttack);
                    input_manager.SubscribeToStrongAttackP1Event(soAttack.HeavyGroundedAttack);

                    healthSlider = GameObject.FindGameObjectWithTag("HealthUI").transform.GetChild(0).GetComponent<Slider>();
                    furySlider = GameObject.FindGameObjectWithTag("FuryUI").transform.GetChild(0).GetComponent<Slider>();

                    break;
                }
                case SoBaseAttack.EAttackType.CAC:
                {
                    input_manager.SubscribeToHorizontalP2Event(ListenXAxis);
                    input_manager.SubscribeToVerticalP2Event(ListenZAxis);
                    input_manager.SubscribeToJumpReviveP2Event(Jump);
                    input_manager.SubscribeToWeakAttackP2Event(soAttack.LightGroundedAttack);
                    input_manager.SubscribeToStrongAttackP2Event(soAttack.HeavyGroundedAttack);

                    healthSlider = GameObject.FindGameObjectWithTag("HealthUI").transform.GetChild(1).GetComponent<Slider>();
                    furySlider = GameObject.FindGameObjectWithTag("FuryUI").transform.GetChild(1).GetComponent<Slider>();

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
            furySlider.value = fury;

            KnockedOut += () => { inReviveCoroutine = StartCoroutine(ToReviveState()); };
            KnockedOut += () => { ++FindObjectOfType<EntityManager>().PlayerKncokedDown; };
        }

        #endregion

        #region IDamagable
        public override void ReceiveDamages(float _damages)
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
                    gameObject.SetActive(false);
                    //Destroy(gameObject);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
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

        public void OnEntityHit(BaseEntity _entity, float useless)
        {
            // if _entity is Enemy

            if (fury >= maxFury)
                return;

            ++fury;
            furySlider.value = fury;
        }


        protected override void ListenXAxis(float _value)
        {
            if (currentState != EPlayerState.KNOCKED_OUT)
                base.ListenXAxis(_value);

        }

        protected override void ListenZAxis(float _value)
        {
            if (currentState != EPlayerState.KNOCKED_OUT)
                base.ListenZAxis(_value);
        }

        protected override void Jump()
        {
            if (currentState != EPlayerState.KNOCKED_OUT)
                base.Jump();
        }

        public void Revive()
        {
            if (currentState == EPlayerState.KNOCKED_OUT)
            {
                gameObject.SetActive(true);
                currentState = EPlayerState.NORMAL;
                health = maxHealth;
                healthSlider.value = health;
            }
        }
    }
}