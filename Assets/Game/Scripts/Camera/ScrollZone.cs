using UnityEngine;

namespace Game.Scripts.Camera
{
    public class ScrollZone : MonoBehaviour
    {
        [SerializeField] private EBorderSide side;
        [SerializeField] private float percent_size;
        private BoxCollider2D collider;
        private CameraController cameraController;

        private delegate void ComputeDelegate();
        private ComputeDelegate ComputePositionFunction;

        void Start()
        {
            cameraController = GetComponentInParent<CameraController>();
            collider = this.gameObject.GetComponent<BoxCollider2D>();

            if (side == EBorderSide.RIGHT)
                ComputePositionFunction = ComputeRightPosition;
            else
                ComputePositionFunction = ComputeLeftPosition;
        }

        void Update()
        {
            ComputeTransform();
        }

        private void ComputeTransform()
        {
            ComputeScale();
            ComputePositionFunction();
        }

        private void ComputeScale()
        {
            Vector2 collider_size = collider.size;
            float scale_percent = (percent_size / 100f) * cameraController.HorizontalViewingVolume;
            collider_size.x = scale_percent;
            collider_size.y = cameraController.VerticalViewingVolume;
            collider.size = collider_size;
        }

        private void ComputeRightPosition()
        {
            Vector3 position = transform.parent.position;
            position.x += (cameraController.HalfHorizontalViewingVolume - (collider.size.x / 2f));
            transform.position = position;
        }

        private void ComputeLeftPosition()
        {
            Vector3 position = transform.parent.position;
            position.x -= (cameraController.HalfHorizontalViewingVolume - (collider.size.x / 2f));
            transform.position = position;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            print("Collision");
        }
    }
}