using Game.Scripts.Collectibles;
using Game.Scripts.SceneObjects;
using Game.Scripts.Sounds;
using UnityEngine;

public class CollectibleTest : MonoBehaviour
{
    public LifeCollectible collectible;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            CollectibleInstance.Create(GetComponent<SceneObject>().location, collectible);
    }
}
