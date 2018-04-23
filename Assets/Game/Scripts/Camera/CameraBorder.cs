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

    private delegate Vector3 ComputeDelegate();
    private ComputeDelegate ComputePositionFunction;

    private void Start()
    {
        cameraController = GetComponentInParent<CameraController>();
        collider = this.gameObject.GetComponent<BoxCollider2D>();

        if (side == EBorderSide.RIGHT)
            ComputePositionFunction = ComputeRightPosition;
        else
            ComputePositionFunction = ComputeLeftPosition;
    }       

	void Update ()
	{
	    transform.position = ComputePositionFunction();
        ComputeScale();
	}

    private Vector3 ComputeRightPosition()
    {
        Vector3 position = this.transform.parent.position;
        position.x += cameraController.HalfHorizontalViewingVolume;
        return position;
    }

    private Vector3 ComputeLeftPosition()
    {
        Vector3 position = this.transform.parent.position;
        position.x -= cameraController.HalfHorizontalViewingVolume;
        return position;
    }

    private void ComputeScale()
    {
        Vector2 collider_size = collider.size;
        collider_size.y = cameraController.VerticalViewingVolume;
        collider.size = collider_size;
    }
}
