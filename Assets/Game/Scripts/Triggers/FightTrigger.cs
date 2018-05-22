using System;
using Game.Scripts.Entity;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    public class FightTrigger : MonoBehaviour, ITriggerAction
    {
        [Serializable]
        public struct Wave
        {
            public EnemySpawn[] spawns;
        }

        public Wave[] waves;

        public void Trigger()
        {
            Debug.Log("FIGHTTRIGGER");
            //this.gameObject.SetActive(false);
            //foreach (Wave wave in waves)
            //{
            //    EnemySpawn[] spawn_array = wave.spawns;
            //    foreach (EnemySpawn enemy_spawn in spawn_array)
            //    {
            //        if (enemy_spawn.type == EnemyType.NORMAL)
            //        {
            //            EnemyEntity enemy = Instantiate(EntityManager.instance.meleeEnemyPrefab, Vector3.zero, Quaternion.identity).GetComponent<EnemyEntity>();
            //            enemy.location = enemy_spawn.location;
            //            enemy.ToFloor();
            //        }
            //        else if (enemy_spawn.type == EnemyType.WEAK)
            //        {
            //            EnemyEntity enemy = Instantiate(EntityManager.instance.rangeEnemyPrefab, Vector3.zero, Quaternion.identity).GetComponent<EnemyEntity>();
            //            enemy.location = enemy_spawn.location;
            //            enemy.ToFloor();
            //        }
            //    }
            //}
            //Debug.Log("END LOOP");
        }
    }
}
