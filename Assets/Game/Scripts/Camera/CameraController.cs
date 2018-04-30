using System;
using UnityEngine;

namespace Game.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private GameObject player1;
        [SerializeField]
        private GameObject player2;
        [SerializeField]
        private float maxZoomInDistance;
        [SerializeField]
        private float maxZoomOutDistance;
        [SerializeField]
        private float attenuationFactor;
        [SerializeField]
        [Range(0f, 0.5f)]
        private float zoomSmoothness;
        //[SerializeField]
        //[Range(0f, 1f)]
        //private float scrollSmoothness;
        [SerializeField]
        private float scrollSpeed;

        [SerializeField]
        private CameraScrollZone rightScrollZone;
        [SerializeField]
        private CameraScrollZone leftScrollZone;


        private UnityEngine.Camera gameCamera;
        private byte forwardScrollMask;
        private byte backwardScrollMask;
        private BoxCollider2D cameraCollider;

        public CameraController(byte _forward_scroll_mask)
        {
            forwardScrollMask = _forward_scroll_mask;
        }

        private float AspectRatio
        {
            get { return (float)gameCamera.pixelWidth / gameCamera.pixelHeight; }
        }

        public float VerticalViewingVolume
        {
            get { return gameCamera.orthographicSize * 2f; }
        }

        public float HorizontalViewingVolume
        {
            get { return VerticalViewingVolume * AspectRatio; }
        }

        public float HalfHorizontalViewingVolume
        {
            get { return HorizontalViewingVolume / 2f; }
        }

        private void Awake()
        {
            forwardScrollMask = 0;
            backwardScrollMask = 0;
        }

        private void Start ()
        {
            gameCamera = GetComponent<UnityEngine.Camera>();
            cameraCollider = gameObject.GetComponent<BoxCollider2D>();
            SubscribeToCameraScrollZoneEvents();
            FindObjectOfType<GameState>().SubscribeToGamePlayStateCallback(ListenToGamePlayState);
        }

        private void Update ()
        {
            float distance = UnsignedHorizontalDistance(player1.transform.position, player2.transform.position);
            ComputeZoom(distance);
            ComputeVerticalPosition();
            ComputeCameraColliderScale();

            CheckIfCameraCanScroll();
        }

        public void ListenToGamePlayState(GameState.EGamePlayState _e_game_play_state)
        {
            //print("ListenToGamePlayState");
            switch (_e_game_play_state)
            {
                case GameState.EGamePlayState.EXPLORATION:
                    SubscribeToCameraScrollZoneEvents();
                    break;
                case GameState.EGamePlayState.COMBAT:
                    UnsubscribeToCameraScrollZoneEvents();
                    break;
                case GameState.EGamePlayState.CINEMATIC:
                    UnsubscribeToCameraScrollZoneEvents();
                    break;
                default:
                    break;
            }
        }

        public void SubscribeToCameraScrollZoneEvents()
        {
            rightScrollZone.SubscribeToTriggerStayCallback(ComputeScroll);
            rightScrollZone.SubscribeToTriggerExitCallback(ComputeScroll);
            leftScrollZone.SubscribeToTriggerStayCallback(ComputeScroll);
            leftScrollZone.SubscribeToTriggerExitCallback(ComputeScroll);
        }

        private void UnsubscribeToCameraScrollZoneEvents()
        {
            rightScrollZone.UnsubscribeToTriggerStayCallback(ComputeScroll);
            rightScrollZone.UnsubscribeToTriggerExitCallback(ComputeScroll);
            leftScrollZone.UnsubscribeToTriggerStayCallback(ComputeScroll);
            leftScrollZone.UnsubscribeToTriggerExitCallback(ComputeScroll);
            forwardScrollMask = 0;
            backwardScrollMask = 0;

        }

        private float ComputeDistance()
        {
            Vector3 direction = player1.transform.position - player2.transform.position;
            return Mathf.Abs(direction.x);
        }

        private float SignedHorizontalDistance(Vector3 A, Vector3 B)
        {
            Vector3 direction = A - B;
            return direction.x;
        }

        private float UnsignedHorizontalDistance(Vector3 A, Vector3 B)
        {
            Vector3 direction = A - B;
            return Mathf.Abs(direction.x);
        }

        private void ComputeZoom(float _distance)
        {
            float raw_zoom = _distance / attenuationFactor;
            float smooth_zoom = Mathf.Lerp(gameCamera.orthographicSize, raw_zoom, zoomSmoothness);

            if ((UnsignedHorizontalDistance(this.transform.position, player1.transform.position) + 0.3f) >= HalfHorizontalViewingVolume
                ||
                (UnsignedHorizontalDistance(this.transform.position, player2.transform.position) + 0.3f) >= HalfHorizontalViewingVolume)
                return;

            if (MaxZoomInReached(smooth_zoom) || MaxZoomOutReached(smooth_zoom))
                return;

            gameCamera.orthographicSize = smooth_zoom;
        }

        private void ComputeVerticalPosition()
        {
            Vector3 transform_position = transform.position;
            transform_position.y = gameCamera.orthographicSize;
            transform.position = transform_position;
        }

        private void ComputeCameraColliderScale()
        {
            Vector2 current_scale = cameraCollider.size;
            current_scale.x = HorizontalViewingVolume;
            current_scale.y = VerticalViewingVolume;

            cameraCollider.size = current_scale;
        }

        private bool AreCloseEnough(float _distance)
        {
            return _distance <= maxZoomInDistance;
        }

        private bool MaxZoomInReached(float _value)
        {
            return _value <= maxZoomInDistance;
        }

        private bool MaxZoomOutReached(float _value)
        {
            return _value >= maxZoomOutDistance;
        }

        private void ComputeScroll(Collider2D _entity, EBorderSide _collider_side, EColliderCallbackType _callback_type)
        {
            switch (_collider_side)
            {
                case EBorderSide.RIGHT:
                    SetForwardMask(_entity, _callback_type);
                    break;
                case EBorderSide.LEFT:
                    SetBackwardMask(_entity, _callback_type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("_collider_side", _collider_side, null);
            }

            //const byte mask = 0 | (1 << 1) | (1 << 2); // mask = 6;
            //if ((forwardScrollMask ^ mask) == 0)
            //    ForwardScroll();
            //else if ((backwardScrollMask ^ mask) == 0)
            //    BackwardScroll();
        }

        private void CheckIfCameraCanScroll()
        {
            const byte mask = 0 | (1 << 1) | (1 << 2); // mask = 6;
            if ((forwardScrollMask ^ mask) == 0)
                ForwardScroll();
            else if ((backwardScrollMask ^ mask) == 0)
                BackwardScroll();
        }

        private void SetForwardMask(Collider2D _entity, EColliderCallbackType _callback_type)
        {
            if (_entity.gameObject == player1)
            {
                switch (_callback_type)
                {
                    case EColliderCallbackType.STAY:
                        forwardScrollMask |= 1 << 1;
                        break;
                    case EColliderCallbackType.EXIT:
                        forwardScrollMask ^= 1 << 1;
                        break;
                    case EColliderCallbackType.ENTER:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("_callback_type", _callback_type, null);
                }
            }
            else if (_entity.gameObject == player2)
            {
                switch (_callback_type)
                {
                    case EColliderCallbackType.STAY:
                        forwardScrollMask |= 1 << 2;
                        break;
                    case EColliderCallbackType.EXIT:
                        forwardScrollMask ^= 1 << 2;
                        break;
                    case EColliderCallbackType.ENTER:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("_callback_type", _callback_type, null);
                }
            }
        }

        private void SetBackwardMask(Collider2D _entity, EColliderCallbackType _callback_type)
        {
            if (_entity.gameObject == player1)
            {
                switch (_callback_type)
                {
                    case EColliderCallbackType.STAY:
                        backwardScrollMask |= 1 << 1;
                        break;
                    case EColliderCallbackType.EXIT:
                        backwardScrollMask ^= 1 << 1;
                        break;
                    case EColliderCallbackType.ENTER:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("_callback_type", _callback_type, null);
                }
            }
            else if (_entity.gameObject == player2)
            {
                switch (_callback_type)
                {
                    case EColliderCallbackType.STAY:
                        backwardScrollMask |= 1 << 2;
                        break;
                    case EColliderCallbackType.EXIT:
                        backwardScrollMask ^= 1 << 2;
                        break;
                    case EColliderCallbackType.ENTER:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("_callback_type", _callback_type, null);
                }
            }
        }

        private void ForwardScroll()
        {
            //Vector3 pos = transform.position;
            //float incrementation = Mathf.Lerp(pos.x, pos.x + 1, scrollSmoothness);
            //pos.x = incrementation * Time.deltaTime;
            //transform.position = pos;
            //print("pos.x = " + pos.x);

            transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed);
        }

        private void BackwardScroll()
        {
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed);
        }
    }
}