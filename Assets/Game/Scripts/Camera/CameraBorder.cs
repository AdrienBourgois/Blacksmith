using UnityEngine;

namespace Game.Scripts.Camera
{
    public class CameraBorder : MonoBehaviour
    {
        [SerializeField] private EBorderSide side;
        private BoxCollider2D cameraCollider;
        private CameraController cameraController;

        private delegate Vector3 ComputeDelegate();
        private ComputeDelegate computePositionFunction;

        private void Start()
        {
            cameraController = GetComponentInParent<CameraController>();
            cameraCollider = gameObject.GetComponent<BoxCollider2D>();

            if (side == EBorderSide.RIGHT)
                computePositionFunction = ComputeRightPosition;
            else
                computePositionFunction = ComputeLeftPosition;
        }

        private void Update()
        {
            ComputeTransform();
        }

        private void ComputeTransform()
        {
            transform.position = computePositionFunction();
            ComputeScale();
        }

        private Vector3 ComputeRightPosition()
        {
            Vector3 position = transform.parent.position;
            position.x += cameraController.HalfHorizontalViewingVolume;
            return position;
        }

        private Vector3 ComputeLeftPosition()
        {
            Vector3 position = transform.parent.position;
            position.x -= cameraController.HalfHorizontalViewingVolume;
            return position;
        }

        private void ComputeScale()
        {
            Vector2 collider_size = cameraCollider.size;
            collider_size.y = cameraController.VerticalViewingVolume;
            cameraCollider.size = collider_size;
        }
    }
}