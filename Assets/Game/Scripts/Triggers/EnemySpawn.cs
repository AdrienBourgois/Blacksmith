using Game.Scripts.SceneObjects;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    public enum EnemyType
    {
        WEAK,
        WEAK_STRONG,
        NORMAL,
        NORMAL_STRONG,
        HEAVY,
        HEAVY_STRONG,
    }

    public enum ItemDrop
    {
        ANY,
        FRENCH_FRIES,
        HAMBURGER,
        ENERGY_DRINK
    }

    public class EnemySpawn : SceneObject
    {
        [Header("EnemySpawn")]
        public EnemyType type;
        public ItemDrop drop = ItemDrop.ANY;

        protected override void LateUpdate()
        {
            return;
        }
    }
}
