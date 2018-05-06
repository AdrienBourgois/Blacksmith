using System;
using System.Collections;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Timer;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Entity
{
    public class PlayerEntity : BaseEntity
    {
        private enum EPlayerType
        {
            MELEE,
            RANGE
        }

        private enum EPlayerState
        {
            NORMAL,
            KNOCKED_OUT
        }

        [SerializeField] private float reviveTimeTeammate;
        [SerializeField] private float reviveTime;
        [SerializeField] private float maxFury;

        private delegate void PlayerStateHanlder();

        private event PlayerStateHanlder KnockedOutEvent;
        private event PlayerStateHanlder RevivedEvent;

        private Slider healthSlider;

        private EPlayerState currentState = EPlayerState.NORMAL;
        [SerializeField] private EPlayerType playerType;

        private int reviveTimerId;

        #region Unity Methods

        protected override void Start()
        {
            base.Start();

            InputManager.InputManager input_manager = FindObjectOfType<InputManager.InputManager>();
            switch (playerType)
            {
                case EPlayerType.MELEE:
                {
                    input_manager.SubscribeToHorizontalP1Event(ListenXAxis);
                    input_manager.SubscribeToVerticalP1Event(ListenZAxis);
                    input_manager.SubscribeToWeakAttackP1Event(LightGroundedAttack);
                    input_manager.SubscribeToStrongAttackP1Event(HeavyGroundedAttack);
                    input_manager.SubscribeToJumpP1Event(Jump);
                    input_manager.SubscribeToFusionP1Event(Fusion);

                    healthSlider = GameObject.FindGameObjectWithTag("HealthUI").transform.GetChild(0).GetComponent<Slider>();

                    break;
                }
                case EPlayerType.RANGE:
                {
                    input_manager.SubscribeToHorizontalP2Event(ListenXAxis);
                    input_manager.SubscribeToVerticalP2Event(ListenZAxis);
                    input_manager.SubscribeToWeakAttackP2Event(LightGroundedAttack);
                    input_manager.SubscribeToStrongAttackP2Event(HeavyGroundedAttack);
                    input_manager.SubscribeToJumpP2Event(Jump);
                    input_manager.SubscribeToFusionP2Event(Fusion);

                    healthSlider = GameObject.FindGameObjectWithTag("HealthUI").transform.GetChild(1).GetComponent<Slider>();

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;

            KnockedOutEvent += KnockedOut;
            KnockedOutEvent += () => { ++FindObjectOfType<EntityManager>().PlayerKncokedDown; };
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
                    if (KnockedOutEvent != null)
                        KnockedOutEvent();
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

        private void KnockedOut()
        {
            currentState = EPlayerState.KNOCKED_OUT;

            reviveTimerId = TimerManager.Instance.AddTimer("Revive Timer", reviveTime, true, false, Die);
        }

        private void RevivePlayer()
        {

        }

        #endregion

        public override void DamageEntity(BaseEntity _entity, float _damages)
        {
            base.DamageEntity(_entity, _damages);

            OnEntityHit(_entity, _damages);
        }

        public void OnEntityHit(BaseEntity _entity, float _useless)
        {
            // if _entity is Enemy
            UiManager.Instance.IncreaseFury(1);
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