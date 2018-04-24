using UnityEngine;

namespace Game.Scripts.Camera
{

    public enum EColliderCallbackType
    {
        ENTER,
        STAY,
        EXIT
    };

    public class CameraScrollZone : MonoBehaviour
    {
        [SerializeField] private EBorderSide side;
        [SerializeField] private float percent_size;
        private BoxCollider2D collider;
        private CameraController cameraController;

        private delegate void ComputeDelegate();
        private ComputeDelegate ComputePositionFunction;
        public delegate void TriggerDelegate(Collider2D _collider, EBorderSide _collider_side, EColliderCallbackType _callback_type);
        private TriggerDelegate triggerStayCallback;
        private TriggerDelegate triggerExitCallback;

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

        public void SubscribeToTriggerStayCallback(TriggerDelegate _function_pointer)
        {
            triggerStayCallback += _function_pointer;
        }

        public void SubscribeToTriggerExitCallback(TriggerDelegate _function_pointer)
        {
            triggerExitCallback += _function_pointer;
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

        private void OnTriggerStay2D(Collider2D _other)
        {
            if (triggerStayCallback != null)
                triggerStayCallback(_other, side, EColliderCallbackType.STAY);
        }

        private void OnTriggerExit2D(Collider2D _other)
        {
            if (triggerExitCallback != null)
                triggerExitCallback(_other, side, EColliderCallbackType.EXIT);
        }
    }
}