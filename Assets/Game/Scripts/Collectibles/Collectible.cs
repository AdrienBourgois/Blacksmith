using FMODUnity;
using Game.Scripts.Entity;
using Game.Scripts.Sounds;
using UnityEngine;

namespace Game.Scripts.Collectibles
{
    [CreateAssetMenu(fileName = "Collectible", menuName = "Collectibles/Base", order = 1)]
    public class Collectible : ScriptableObject
    {
        public Sprite sprite;

        [EventRef]
        public string sound;

        public virtual void Collect(PlayerEntity _player)
        {
            SoundOneShot.PlayOneShot(sound, 1f, _player.transform.position);
        }

        
    }
}
