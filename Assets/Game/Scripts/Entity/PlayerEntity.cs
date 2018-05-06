using System;
using System.Collections;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Timer;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Entity
{
    public class PlayerEntity : AttackEntity
    {
        private enum EPlayerState
        {
            NORMAL,
            READY_TO_FUSION,
            ASK_TO_FUSION,
            FUSION,
            KNOCKED_OUT
        }

        [SerializeField] private float reviveTimeTeammate;
        [SerializeField] private float reviveTime;
        [SerializeField] private float maxFury;

        private float fury;

        public delegate void PlayerStateDelegate();

        private event PlayerStateDelegate askToFusion;
        private event PlayerStateDelegate KnockedOut;
        private event PlayerStateDelegate Revived;

        private Slider healthSlider;
        private Slider furySlider;

        private EPlayerState currentState = EPlayerState.NORMAL;

        private Coroutine inReviveCoroutine;

        public bool IsAskingFusion { get { return currentState == EPlayerState.ASK_TO_FUSION; } }

        #region CallBackSubscription
        public void SubscribeToAskToFusionCallback(PlayerStateDelegate _listener_function)
        {
            askToFusion += _listener_function;
        }
        #endregion

        #region CallBackUnsubscription
        public void UnsubscribeToAskToFusionCallback(PlayerStateDelegate _listener_function)
        {
            askToFusion -= _listener_function;
        }
        #endregion

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
                    input_manager.SubscribeToJumpP1Event(Jump);
                    input_manager.SubscribeToFusionP1Event(Fusion);
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
                    input_manager.SubscribeToJumpP2Event(Jump);
                    input_manager.SubscribeToFusionP2Event(Fusion);
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

            velocity.x = -0.1f;
            velocity.y = 0.1f;

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

        protected void Fusion(float axe_value)
        {
            print("Fusion = " + axe_value);
            if (currentState == EPlayerState.READY_TO_FUSION)
            {
                currentState = EPlayerState.ASK_TO_FUSION;
            }
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

        private void SwitchPlayerState(EPlayerState _new_state)
        {
            switch (_new_state)
            {
                case EPlayerState.NORMAL: 
                    break;
                case EPlayerState.READY_TO_FUSION:
                    break;
                case EPlayerState.ASK_TO_FUSION:
                {
                    if (askToFusion != null)
                        askToFusion();

                    break;
                }
                    
                case EPlayerState.FUSION:
                    break;
                case EPlayerState.KNOCKED_OUT:
                    break;
            }
        }

        public void FusionAskRefused()
        {
            if (IsAskingFusion)
                SwitchPlayerState(EPlayerState.READY_TO_FUSION);
        }

        public void FusionAskAccepted()
        {
            SwitchPlayerState(EPlayerState.FUSION);
        }
    }
}