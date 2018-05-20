using Game.Scripts.Camera;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class EntityManager : MonoBehaviour
    {
        [SerializeField] private GameObject meleePlayerPrefab;
        [SerializeField] private GameObject rangePlayerPrefab;

        private PlayerEntity[] players;
        [SerializeField] private float fusionInputTimeOut;
        [SerializeField] private float fusionDuration;

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

        public int EnemyNum
        {
            get { return enemyNum;}
            set
            {
                enemyNum = value;
                if (enemyNum == 0)
                {
                    FindObjectOfType<CameraController>().SubscribeToCameraScrollZoneEvents();
                    foreach (PlayerEntity player_entity in players)
                    {
                        player_entity.Revive();
                    }
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

            Transform spawn = GameObject.FindGameObjectWithTag("StartSpawn").transform;
            currentPlayer = Instantiate(meleePlayerPrefab, spawn.position,
                    Quaternion.identity,
                    GameObject.Find("3CLevel(Clone)").transform)
                .GetComponent<PlayerEntity>();

            currentPlayer.SubscribeByType(false);

            PlayerEntity range = Instantiate(rangePlayerPrefab, spawn.position,
                    Quaternion.identity,
                    GameObject.Find("3CLevel(Clone)").transform)
                .GetComponent<PlayerEntity>();

            players = FindObjectsOfType<PlayerEntity>();
            ListenToPlayersCallbacks();

            if (!GameState.Instance.IsTwoPlayer)
            {
                range.gameObject.SetActive(false);
            }
            else
            {
                range.SubscribeByType(false);
            }
        }

        private void Start()
        {
            if (!GameState.Instance.IsTwoPlayer)
                InputManager.InputManager.Instance.SubscribeToSwapP1Event(SwitchPlayer);

            enemyNum = FindObjectsOfType<TmpEnemyEntity>().Length;
            fusionInputTimeOutId = TimerManager.Instance.AddTimer("FuryInput", fusionInputTimeOut, false, false, OnFusionTimerExpired);
        }

        private void Update()
        {
           // if (Input.GetKeyDown(KeyCode.F))
             //   AcceptPlayerFusion();
        }

        #endregion

        #region Getters
        public PlayerEntity GetMeleePlayer()
        {
            if (players[0].PlayerType == PlayerEntity.EPlayerType.MELEE)
                return players[0];

            return players[1];
        }

        public PlayerEntity GetRangePlayer()
        {
            if (players[0].PlayerType == PlayerEntity.EPlayerType.RANGE)
                return players[0];

            return players[1];
        }

        public PlayerEntity GetP2()
        {
            if (GameState.Instance.IsTwoPlayer)
                return GetRangePlayer();

            if (players[0] == currentPlayer)
                return players[1];

            return players[0];
        }
        #endregion

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

            currentPlayer = p2;
            currentPlayer.SubscribeToP1(false);
            currentPlayer.gameObject.SetActive(true);
        }

        public void ListenToPlayersCallbacks()
        {
            foreach (PlayerEntity player_entity in players)
            {
                player_entity.SubscribeToAskToFusionCallback(OnFusionAsking);
            }
        }

        public void StopListenToPlayersCallbacks()
        {
            foreach (PlayerEntity player_entity in players)
            {
                player_entity.UnsubscribeToAskToFusionCallback(OnFusionAsking);
            }
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
            foreach (PlayerEntity player_entity in players)
            {
                if (player_entity.IsAskingFusion == false)
                    return false;
            }

            return true;
        }

        private void AcceptPlayerFusion()
        {
            InputManager.InputManager.Instance.UnsubscribeFromMoveAndAttackControls();

            foreach (PlayerEntity player_entity in players)
            {
                player_entity.FusionAskAccepted();
            }

            int timer_id = TimerManager.Instance.AddTimer("Fusion Timer", fusionDuration, true, false, OnFusionExpired);
            UiManager.Instance.StartFusionUi(timer_id, fusionDuration);

            TimerManager.Instance.StopTimer(fusionInputTimeOutId);
        }

        private void OnFusionTimerExpired()
        {
            foreach (PlayerEntity player_entity in players)
            {
                player_entity.FusionAskRefused();
            }
        }

        private void OnFusionExpired()
        {
            InputManager.InputManager.Instance.UnsubscribeFromMoveAndAttackControls();

            foreach (PlayerEntity player_entity in players)
            {
                player_entity.FusionEnded();
            }

            // stop timer ? 
            UiManager.Instance.EndFusionUi();
        }
    }
}
