using System;
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
            MELEE = 0,
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

        private InputManager.InputManager inputManager;

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
            inputManager = FindObjectOfType<InputManager.InputManager>();

            SubscribeByType(false);

            healthSlider = GameObject.FindGameObjectWithTag("HealthUI").transform.GetChild((int)playerType).GetComponent<Slider>();

            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;

            KnockedOutEvent += KnockedOut;
            KnockedOutEvent += () => { ++FindObjectOfType<EntityManager>().PlayerKncokedDown; };
        }

        protected override void Update()
        {
            base.Update();
        }
        #endregion

        #region Subscribe / Unsubscribe
        public void SubscribeAfterFusion()
        {
            if (GameState.Instance.IsTwoPlayer)
            {
                SubscribeByType(false);
            }
            else
            {
                if (this == EntityManager.Instance.CurrentPlayer)
                    SubscribeToP1(false);
            }
        }

        public void SubscribeByType(bool _fusion)
        {
            switch (playerType)
            {
                case EPlayerType.MELEE: // Player 1
                {
                    SubscribeToP1(_fusion);

                    break;
                }
                case EPlayerType.RANGE: // player 2
                {
                    SubscribeToP2(_fusion);

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SubscribeToP1(bool _fusion)
        {
            inputManager.SubscribeToHorizontalP1Event(ListenXAxis);
            inputManager.SubscribeToVerticalP1Event(ListenZAxis);
            inputManager.SubscribeToJumpP1Event(Jump);
            inputManager.SubscribeToFusionP1Event(Fusion);

            if (_fusion)
            {
                if (GameState.Instance.IsTwoPlayer)
                {
                    inputManager.SubscribeToWeakAttackP1Event(FusionAttack);
                    inputManager.SubscribeToStrongAttackP1Event(FusionAttack);
                }
                else
                    inputManager.SubscribeToWeakAttackP1Event(FusionAttack);
            }
            else
            {
                inputManager.SubscribeToWeakAttackP1Event(LightGroundedAttack);
                inputManager.SubscribeToStrongAttackP1Event(HeavyGroundedAttack);
            }
        }

        public void SubscribeToP2(bool _fusion)
        {
            if (_fusion)
            {
                if (GameState.Instance.IsTwoPlayer)
                {
                    inputManager.SubscribeToWeakAttackP2Event(FusionAttack);
                    inputManager.SubscribeToStrongAttackP2Event(FusionAttack);
                }
                else
                    inputManager.SubscribeToStrongAttackP1Event(FusionAttack);
            }
            else
            {
                inputManager.SubscribeToHorizontalP2Event(ListenXAxis);
                inputManager.SubscribeToVerticalP2Event(ListenZAxis);
                inputManager.SubscribeToJumpP2Event(Jump);

                inputManager.SubscribeToWeakAttackP2Event(LightGroundedAttack);
                inputManager.SubscribeToStrongAttackP2Event(HeavyGroundedAttack);
            }
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

        private void OnEntityHit(BaseEntity _entity, float _useless)
        {
            // if _entity is Enemy
            UiManager.Instance.IncreaseFury(1);
        }

        #region Movement

        private void ListenXAxis(float _value)
        {
            if (currentState != EPlayerState.KNOCKED_OUT && !isAttacking)
                TryMove(new Vector3(_value, 0f, 0f));
        }

        private void ListenZAxis(float _value)
        {
            if (currentState != EPlayerState.KNOCKED_OUT && !isAttacking)
                TryMove(new Vector3(0f, 0f, _value));
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

            SubscribeByType(true);

            if (playerType == EPlayerType.RANGE)
            {
                PlayerEntity melee = EntityManager.Instance.GetMeleePlayer();
                transform.parent = melee.transform;
                location = Vector3.zero;
            }
        }

        public void FusionAskRefused()
        {
            if (IsAskingFusion)
                SwitchPlayerState(EPlayerState.READY_TO_FUSION);
        }

        public void FusionEnded()
        {
            if (playerType == EPlayerType.RANGE)
                transform.parent = transform.parent.parent;

            SwitchPlayerState(EPlayerState.NORMAL);
            SubscribeByType(false);
        }

        protected void Fusion(float _axe_value)
        {
            print("Fusion = " + _axe_value);
            if (currentState == EPlayerState.READY_TO_FUSION)
            {
                currentState = EPlayerState.ASK_TO_FUSION;
            }
        }
        #endregion
    }
}