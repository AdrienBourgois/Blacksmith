using Game.Scripts.Camera;
using Game.Scripts.Timer;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class EntityManager : MonoBehaviour
    {
        private PlayerEntity[] players;
        [SerializeField] private float fusionInputTimeOut;

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
        }

        private void Start()
        {
            players = FindObjectsOfType<PlayerEntity>();
            ListenToPlayersCallbacks();

            currentPlayer = GetMeleePlayer();

            enemyNum = FindObjectsOfType<TmpEnemyEntity>().Length;
            fusionInputTimeOutId = TimerManager.Instance.AddTimer("FuryInput", fusionInputTimeOut, false, false, OnFusionTimerExpired);
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
            if (currentPlayer.PlayerType == PlayerEntity.EPlayerType.MELEE)
                return GetRangePlayer();
            return GetMeleePlayer();
        }
        #endregion

        public void SwitchPlayer()
        {
            currentPlayer.UnsubscribeFromP1();

            currentPlayer = GetP2();
            currentPlayer.SubscribeToP1();
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

        public void SpawnEntity(GameObject _entity)
        {
            Instantiate(_entity);
        }

        private void OnFusionAsking()
        {
            TimerManager.Instance.StartTimer(fusionInputTimeOutId);
            if (AreBothPlayerAskToFusion())
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
            foreach (PlayerEntity player_entity in players)
            {
                player_entity.FusionAskAccepted();
            }

            TimerManager.Instance.StopTimer(fusionInputTimeOutId);
        }

        private void OnFusionTimerExpired()
        {
            foreach (PlayerEntity player_entity in players)
            {
                player_entity.FusionAskRefused();
            }
        }
    }
}
