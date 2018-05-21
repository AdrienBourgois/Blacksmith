using System;
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

        }
    }
}
