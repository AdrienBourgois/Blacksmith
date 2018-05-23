using System;
using Game.Scripts.Entity;
using UnityEngine;
using Game.Scripts.Interfaces;
using Game.Scripts.SceneObjects;
using UnityEngine.Assertions;

namespace Game.Scripts.Triggers
{
    public class FightTrigger : ATriggerAction
    {
        [Serializable]
        public struct Wave
        {
            public EnemySpawn[] spawns;
        }

        public Wave[] waves;

        public override void Trigger()
        {
            foreach (Wave wave in waves)
            {
                EnemySpawn[] spawn_array = wave.spawns;
                foreach (EnemySpawn enemy_spawn in spawn_array)
                {
                    if (enemy_spawn.type == EnemyType.NORMAL)
                    {
                        SceneObjects.SceneObject enemy = Instantiate(EntityManager.instance.normalEnemyPrefab, Vector3.zero, Quaternion.identity).GetComponent<SceneObjects.SceneObject>();
                        Assert.IsNotNull(enemy, "[FightTrigger.Trigger()] Error : the variable 'enemy' is NULL");
                        enemy.location = enemy_spawn.location;
                        enemy.ToFloor();
                    }
                    else if (enemy_spawn.type == EnemyType.WEAK)
                    {
                        SceneObjects.SceneObject enemy = Instantiate(EntityManager.instance.weakEnemyPrefab, Vector3.zero, Quaternion.identity).GetComponent<SceneObjects.SceneObject>();
                        Assert.IsNotNull(enemy, "[FightTrigger.Trigger()] Error : the variable 'enemy' is NULL");
                        enemy.location = enemy_spawn.location;
                        enemy.ToFloor();
                    }
                }
            }
            Debug.Log("END LOOP");
        }
    }
}
