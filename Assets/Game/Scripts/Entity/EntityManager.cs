using Game.Scripts.Camera;
using Game.Scripts.SceneObjects;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class EntityManager : MonoBehaviour
    {
        [SerializeField] public GameObject meleeEnemyPrefab;
        [SerializeField] public GameObject rangeEnemyPrefab;

        [SerializeField] private GameObject meleePlayerPrefab;
        [SerializeField] private GameObject rangePlayerPrefab;

        public PlayerEntity MeleePlayer { get; private set; }
        public PlayerEntity RangePlayer { get; private set; }

        [SerializeField] private float fusionInputTimeOut;
        [SerializeField] private float fusionDuration;

        [SerializeField] private float distanceRevivePlayer;

        [SerializeField] private SceneObject spawn;

        public PlayerEntity CurrentPlayer
        {
            get { return currentPlayer; }
        }
        private PlayerEntity currentPlayer;

        private int enemyNum;
        private int fusionInputTimeOutId;

        public static EntityManager instance;
        public static EntityManager Instance
        {
            get { return instance; }
        }

        public float DistanceRevivePlayer
        {
            get { return distanceRevivePlayer; }
        }

        public int EnemyNum
        {
            get { return enemyNum;}
            set
            {
                enemyNum = value;
                if (enemyNum == 0)
                {
                    FindObjectOfType<CameraController>().SubscribeToCameraScrollZoneEvents();
                    MeleePlayer.Revive();
                    RangePlayer.Revive();
                }
            }
        }

        private uint playerKncokedDown;
        public uint PlayerKncokedDown
        {
            get { return playerKncokedDown; }
            set
            {
                playerKncokedDown = value;
                GameState.Instance.IsGameOver(playerKncokedDown);
            }
        }

        #region Unity Methods
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            MeleePlayer = Instantiate(meleePlayerPrefab, Vector3.zero,
                    Quaternion.identity,
                    GameInstance.Instance.currentLevel)
                .GetComponent<PlayerEntity>();

            currentPlayer = MeleePlayer;
            currentPlayer.SubscribeByType(false);

            RangePlayer = Instantiate(rangePlayerPrefab, Vector3.zero,
                    Quaternion.identity,
                    GameInstance.Instance.currentLevel)
                .GetComponent<PlayerEntity>();

            MeleePlayer.location = spawn.location;
            RangePlayer.location = spawn.location;

            MeleePlayer.ToFloor();
            RangePlayer.ToFloor();

            ListenToPlayersCallbacks();

            if (!GameState.Instance.IsTwoPlayer)
            {
             //   RangePlayer.gameObject.SetActive(false);
            }
            else
            {
                RangePlayer.SubscribeByType(false);
            }

            if (!GameState.Instance.IsTwoPlayer)
                InputManager.InputManager.Instance.SubscribeToSwapP1Event(SwitchPlayer);

            enemyNum = FindObjectsOfType<TmpEnemyEntity>().Length;
            fusionInputTimeOutId = TimerManager.Instance.AddTimer("FuryInput", fusionInputTimeOut, false, false, OnFusionTimerExpired);

            if (GameState.Instance.IsTwoPlayer == false)
            {
                PlayerEntity p2 = GetP2();
                //p2.transform.position = currentPlayer.transform.position;
                //ReparentPlayers(currentPlayer, p2);
            }
        }

        private void Update()
        {
           // if (Input.GetKeyDown(KeyCode.F))
             //   AcceptPlayerFusion();

            //if (GameState.Instance.IsTwoPlayer == false)
            //{
            //    if (MeleePlayer.enabled == true)
            //    {
            //        RangePlayer.location = MeleePlayer.location;
            //    }
            //    else
            //    {
            //        MeleePlayer.location = RangePlayer.location;
            //    }
            //}
        }

        #endregion

        #region Getters
        public PlayerEntity GetP2()
        {
            if (GameState.Instance.IsTwoPlayer)
                return RangePlayer;

            if (MeleePlayer == currentPlayer)
                return RangePlayer;

            return MeleePlayer;
        }

        public PlayerEntity GetOppositePlayer(PlayerEntity _entity)
        {
            if (_entity == MeleePlayer)
                return RangePlayer;

            return MeleePlayer;
        }

        #endregion

        public PlayerEntity GetActivePlayerInSoloMode()
        {
            if (GameState.Instance.IsTwoPlayer == false)
            {
                Debug.Log("[EntityManager.GetActivePlayerInSoloMode()] Error : You are calling a method that can be called only in SinglePlayer");
                return null;
            }

            if (MeleePlayer.enabled == true)
                return MeleePlayer;

            return RangePlayer;
        }

        public void SwitchPlayer()
        {
            if (currentPlayer.CurrentState != PlayerEntity.EPlayerState.KNOCKED_OUT)
            {
                if (currentPlayer.CurrentState != PlayerEntity.EPlayerState.NORMAL || currentPlayer.IsInRecovery)
                    return; 
            }

            InputManager.InputManager.Instance.UnsubscribeFromMoveAndAttackControls();

            PlayerEntity p2 = GetP2();
            p2.transform.localScale = currentPlayer.transform.localScale;
            p2.location = currentPlayer.location;

            currentPlayer.gameObject.SetActive(false);

            PlayerEntity old_player = currentPlayer;

            currentPlayer = p2;
            //DetachPlayerParent(currentPlayer);
            //ReparentPlayers(currentPlayer, old_player);
            //old_player.transform.SetParent(currentPlayer.transform);
            currentPlayer.SubscribeToP1(false);
            currentPlayer.gameObject.SetActive(true);
        }

        public void ReparentPlayers(PlayerEntity parent, PlayerEntity child)
        {
            child.transform.SetParent(parent.transform);
        }

        public void DetachPlayerParent(PlayerEntity child)
        {
            child.transform.parent = null;
        }

        public void ListenToPlayersCallbacks()
        {
            MeleePlayer.SubscribeToAskToFusionCallback(OnFusionAsking);
            RangePlayer.SubscribeToAskToFusionCallback(OnFusionAsking);
        }

        public void StopListenToPlayersCallbacks()
        {
            MeleePlayer.UnsubscribeToAskToFusionCallback(OnFusionAsking);
            RangePlayer.UnsubscribeToAskToFusionCallback(OnFusionAsking);
        }

        public GameObject SpawnEntity(GameObject _entity)
        {
            return Instantiate(_entity);
        }

        private void OnFusionAsking()
        {
            TimerManager.Instance.StartTimer(fusionInputTimeOutId);
            if (GameState.Instance.IsTwoPlayer && AreBothPlayerAskToFusion())
                AcceptPlayerFusion();
            else if (!GameState.Instance.IsTwoPlayer)
                AcceptPlayerFusion();
        }

        private bool AreBothPlayerAskToFusion()
        {
            if (RangePlayer.IsAskingFusion == false || MeleePlayer.IsAskingFusion == false)
                return false;

            return true;
        }

        private void AcceptPlayerFusion()
        {
            InputManager.InputManager.Instance.UnsubscribeFromMoveAndAttackControls();

            MeleePlayer.FusionAskAccepted();
            RangePlayer.FusionAskAccepted();

            int timer_id = TimerManager.Instance.AddTimer("Fusion Timer", fusionDuration, true, false, OnFusionExpired);
            UIManager.Instance.StartFusionUi(timer_id, fusionDuration);

            TimerManager.Instance.StopTimer(fusionInputTimeOutId);
        }

        private void OnFusionTimerExpired()
        {
            /// WTF ????? MeleePlayer.FusionAskAccepted();
            MeleePlayer.FusionAskRefused();
            RangePlayer.FusionAskRefused();
        }

        private void OnFusionExpired()
        {
            InputManager.InputManager.Instance.UnsubscribeFromMoveAndAttackControls();

            MeleePlayer.FusionEnded();
            RangePlayer.FusionEnded();

            // stop timer ? 
            UIManager.Instance.EndFusionUi();
        }
    }
}