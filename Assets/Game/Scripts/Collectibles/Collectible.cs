using FMODUnity;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.Collectibles
{
    public class Collectible : ScriptableObject
    {
        public Sprite sprite;

        [EventRef]
        public string sound;

        public virtual void Collect(PlayerEntity _player)
        {

        }
    }
}
