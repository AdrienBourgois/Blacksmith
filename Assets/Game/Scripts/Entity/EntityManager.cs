using Game.Scripts.Camera;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class EntityManager : MonoBehaviour
    {
        private PlayerEntity[] players;

        private int enemyNum;
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

        private void Start()
        {
            players = FindObjectsOfType<PlayerEntity>();

            instance = this;
            enemyNum = FindObjectsOfType<TmpEnemyEntity>().Length;
        }

        public void SpawnEntity(GameObject _entity)
        {
            Instantiate(_entity);
        }
    }
}
