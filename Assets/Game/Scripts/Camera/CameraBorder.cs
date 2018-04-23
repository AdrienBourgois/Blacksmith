using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Camera;
using UnityEngine;

public enum EBorderSide
{
    RIGHT = 0,
    LEFT = 1
};

public class CameraBorder : MonoBehaviour
{
    [SerializeField]
    private EBorderSide side;
    private BoxCollider2D collider;
    private CameraController cameraController;

    public delegate float
    private System.Action positionFunction;

    private void Start()
    {
        cameraController = GetComponentInParent<CameraController>();
        collider = this.gameObject.GetComponent<BoxCollider2D>();

        if (side == EBorderSide.RIGHT)
            positionFunction = ComputeRightPosition;
        else
            positionFunction = ComputeLeftPosition;
    }       

	void Update ()
	{
	    positionFunction();
	}

    private void ComputeRightPosition()
    {
        Vector3 position = this.transform.parent.position;
        position.x += cameraController.HalfHorizontalViewingVolume;
        this.transform.position = position;
        Vector2 collider_size = collider.size;
        collider_size.y = cameraController.VerticalViewingVolume;
        collider.size = collider_size;
    }

    private void ComputeLeftPosition()
    {
        Vector3 position = this.transform.parent.position;
        position.x -= cameraController.HalfHorizontalViewingVolume;
        this.transform.position = position;
        Vector2 collider_size = collider.size;
        collider_size.y = cameraController.VerticalViewingVolume;
        collider.size = collider_size;
    }
}
