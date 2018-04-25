using Game.Scripts.Camera;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public class EntityManager : MonoBehaviour
    {
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
                    FindObjectOfType<CameraController>().SubscribeToCameraScrollZoneEvents(); 
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
            instance = this;
            enemyNum = FindObjectsOfType<TmpEnemyEntity>().Length;
        }

        public void SpawnEntity(GameObject _entity)
        {
            Instantiate(_entity);
        }
    }
}
