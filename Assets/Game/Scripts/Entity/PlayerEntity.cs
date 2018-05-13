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
        public enum EPlayerType
        {
            MELEE,
            RANGE
        }

        private enum EPlayerState
        {
            NORMAL,
            READY_TO_FUSION,
            ASK_TO_FUSION,
            FUSION,
            KNOCKED_OUT
        }

        [SerializeField] [Range(0, 90)] private float maxRotationAngle;
        [SerializeField] private float rotateSpeed;

        private Vector3 inputDir;

        [SerializeField] private SoBaseAttack fusionAttack;

        [SerializeField] private float reviveTimeTeammate;
        [SerializeField] private float reviveTime;
        [SerializeField] private float maxFury;

        public delegate void PlayerStateHanlder();

        private event PlayerStateHanlder AskToFusion;
        private event PlayerStateHanlder KnockedOutEvent;
        private event PlayerStateHanlder RevivedEvent;

        private Slider healthSlider;

        private EPlayerState currentState = EPlayerState.NORMAL;
        [SerializeField] private EPlayerType playerType;
        public EPlayerType PlayerType
        {
            get { return playerType; }
        }
        private int reviveTimerId;

        public bool IsAskingFusion { get { return currentState == EPlayerState.ASK_TO_FUSION; } }

        private InputManager.InputManager input_manager;

        #region CallBackSubscription
        public void SubscribeToAskToFusionCallback(PlayerStateHanlder _listener_function)
        {
            AskToFusion += _listener_function;
        }
        #endregion

        #region CallBackUnsubscription
        public void UnsubscribeToAskToFusionCallback(PlayerStateHanlder _listener_function)
        {
            AskToFusion -= _listener_function;
        }
        #endregion

        #region Unity Methods
        protected override void Start()
        {
            base.Start();

            input_manager = FindObjectOfType<InputManager.InputManager>();
            switch (playerType)
            {
                case EPlayerType.RANGE: // Player 1
                {
                    SubscribeToP1();
                    healthSlider = GameObject.FindGameObjectWithTag("HealthUI").transform.GetChild(0).GetComponent<Slider>();

                    break;
                }
                case EPlayerType.MELEE: // player 2
                {
                    if (GameState.Instance.IsTwoPlayer)
                    {
                        input_manager.SubscribeToHorizontalP2Event(ListenXAxis);
                        input_manager.SubscribeToVerticalP2Event(ListenZAxis);
                        input_manager.SubscribeToWeakAttackP2Event(LightGroundedAttack);
                        input_manager.SubscribeToStrongAttackP2Event(HeavyGroundedAttack);
                        input_manager.SubscribeToJumpP2Event(Jump);
                        input_manager.SubscribeToFusionP2Event(Fusion);
                    }

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

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.F))
                FusionAskAccepted();
        }
        #endregion

        #region Subscribe / Unsubscribe
        public void SubscribeToP1()
        {
            input_manager.SubscribeToHorizontalP1Event(ListenXAxis);
            input_manager.SubscribeToVerticalP1Event(ListenZAxis);
            input_manager.SubscribeToWeakAttackP1Event(LightGroundedAttack);
            input_manager.SubscribeToStrongAttackP1Event(HeavyGroundedAttack);
            input_manager.SubscribeToJumpP1Event(Jump);
            input_manager.SubscribeToFusionP1Event(Fusion);
        }

        public void UnsubscribeFromP1()
        {
            input_manager.UnsubscribeFromHorizontalP1Event(ListenXAxis);
            input_manager.UnsubscribeFromVerticalP1Event(ListenZAxis);
            input_manager.UnsubscribeFromWeakAttackP1Event(LightGroundedAttack);
            input_manager.UnsubscribeFromStrongAttackP1Event(HeavyGroundedAttack);
            input_manager.UnsubscribeFromJumpP1Event(Jump);
            input_manager.UnsubscribeFromFusionP1Event(Fusion);
        }
        #endregion

        #region Rotate
        public void AimV(float _value)
        {
            inputDir.y = _value;
            Rotate();
        }

        void Rotate()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, Mathf.Sign(inputDir.y) * maxRotationAngle), rotateSpeed * Time.deltaTime);
        }
        #endregion

        #region IDamagable
        public override void ReceiveDamages(float _damages)
        {
            if (currentState == EPlayerState.FUSION)
                return;

            base.ReceiveDamages(_damages);

            if (!inRecovery)
            {
                velocity.x = -0.1f;
                velocity.y = 0.1f;
                healthSlider.value = health;
            }
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

        #region Movement
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
        #endregion

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
                    if (AskToFusion != null)
                        AskToFusion();

                    break;
                }
                    
                case EPlayerState.FUSION:
                    break;
                case EPlayerState.KNOCKED_OUT:
                    break;
            }
        }

        #region Fusion
        public void FusionAttack()
        {
            if (CanAttack())
                fusionAttack.Attack(this);
        }

        public void FusionAskAccepted()
        {
            SwitchPlayerState(EPlayerState.FUSION);
            InputManager.InputManager input_manager = FindObjectOfType<InputManager.InputManager>();

            if (GameState.Instance.IsTwoPlayer)
            {
                switch (playerType)
                {
                    case EPlayerType.MELEE:
                    {
                        input_manager.UnsubscribeFromWeakAttackP1Event(LightGroundedAttack);
                        input_manager.UnsubscribeFromStrongAttackP1Event(HeavyGroundedAttack);

                        input_manager.SubscribeToWeakAttackP1Event(FusionAttack);
                        input_manager.SubscribeToStrongAttackP1Event(FusionAttack);
                        break;
                    }
                    case EPlayerType.RANGE:
                    {
                        PlayerEntity melee = EntityManager.Instance.GetMeleePlayer();
                        transform.parent = melee.transform;
                        location = Vector3.zero;

                        input_manager.SubscribeToVerticalP2Event(AimV);
                        input_manager.UnsubscribeFromHorizontalP2Event(ListenXAxis);
                        input_manager.UnsubscribeFromVerticalP2Event(ListenXAxis);
                        input_manager.UnsubscribeFromJumpP2Event(Jump);
                        input_manager.UnsubscribeFromWeakAttackP2Event(LightGroundedAttack);
                        input_manager.UnsubscribeFromStrongAttackP2Event(HeavyGroundedAttack);

                        input_manager.SubscribeToWeakAttackP2Event(FusionAttack);
                        input_manager.SubscribeToStrongAttackP2Event(FusionAttack);
                        break;
                    }

                }
            }
            else
            {
                switch (playerType)
                {
                    case EPlayerType.MELEE:
                    {
                        input_manager.SubscribeToHorizontalP1Event(ListenXAxis);
                        input_manager.SubscribeToVerticalP1Event(ListenZAxis);
                        input_manager.SubscribeToWeakAttackP1Event(FusionAttack);
                            break;
                    }
                    case EPlayerType.RANGE:
                    {
                        PlayerEntity melee = EntityManager.Instance.GetMeleePlayer();
                        transform.parent = melee.transform;
                        location = Vector3.zero;

                        input_manager.SubscribeToWeakAttackP1Event(FusionAttack);
                        input_manager.SubscribeToVerticalP1Event(AimV); 

                            break;
                    }
                }
            }

            // change the graphic position of PlayerNephew
            // change the controller
        }

        public void FusionAskRefused()
        {
            if (IsAskingFusion)
                SwitchPlayerState(EPlayerState.READY_TO_FUSION);
        }

        protected void Fusion(float axe_value)
        {
            print("Fusion = " + axe_value);
            if (currentState == EPlayerState.READY_TO_FUSION)
            {
                currentState = EPlayerState.ASK_TO_FUSION;
            }
        }
        #endregion
    }
}