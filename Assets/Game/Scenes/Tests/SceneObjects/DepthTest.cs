using Game.Scripts.SceneObjects;
using UnityEngine;

public class DepthTest : SceneObject
{
    public SceneObject scene;

    public bool isOverlap = false;

    public float depth = 1f;

    protected override void Update()
    {
        SceneObject[] objects_in_layer = GetObjectsInLayer(depth, LayerMask.GetMask("Collectible"));
        isOverlap = objects_in_layer != null && objects_in_layer.Length > 0;
    }
}
