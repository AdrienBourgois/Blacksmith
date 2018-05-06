using Game.Scripts.Camera;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class EntityManager : MonoBehaviour
    {
        private PlayerEntity[] players;
        [SerializeField] private float fusionInputTimeOut;

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

        private uint playerKncokedDown = 0;
        public uint PlayerKncokedDown
        {
            get { return playerKncokedDown; }
            set
            {
                playerKncokedDown = value;
                GameState.Instance.IsGameOver(playerKncokedDown);
            }
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            players = FindObjectsOfType<PlayerEntity>();
            ListenToPlayersCallbacks();

            enemyNum = FindObjectsOfType<TmpEnemyEntity>().Length;
            fusionInputTimeOutId = Timer.TimerManager.Instance.AddTimer("FuryInput", fusionInputTimeOut, false, false, OnFusionTimerExpired);

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
            Timer.TimerManager.Instance.StartTimer(fusionInputTimeOutId);
            if (AreBothPlayerAskToFusion() == true)
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

            Timer.TimerManager.Instance.StopTimer(fusionInputTimeOutId);
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
