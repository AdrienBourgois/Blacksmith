﻿using UnityEngine;

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
        [Range(0f, 1f)]
        private float smoothness;

        [SerializeField]
        private CameraScrollZone rightScrollZone;
        [SerializeField]
        private CameraScrollZone leftScrollZone;


        private UnityEngine.Camera gameCamera;
        private byte forwardScrollMask;
        private byte backwardScrollMask;

        public CameraController(byte _forward_scroll_mask)
        {
            forwardScrollMask = _forward_scroll_mask;
        }

        private float AspectRatio
        {
            get { return (float)(gameCamera.pixelWidth) / (float)(gameCamera.pixelHeight); }
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

        void Awake()
        {
            forwardScrollMask = 0;
        }

        void Start ()
        {
            gameCamera = this.GetComponent<UnityEngine.Camera>();
            CameraScrollZone camera_scroll_zone = FindObjectOfType<CameraScrollZone>();

            rightScrollZone.SubscribeToTriggerStayCallback(ComputeScroll);
            rightScrollZone.SubscribeToTriggerExitCallback(ComputeScroll);
            leftScrollZone.SubscribeToTriggerStayCallback(ComputeScroll);
            leftScrollZone.SubscribeToTriggerExitCallback(ComputeScroll);
        }
	
        void Update ()
        {
            float distance = ComputeDistance();
            ComputeZoom(distance);
            
            //print("HorizontalViewingVolume = " + HorizontalViewingVolume / 2f);
        }

        float ComputeDistance()
        {
            Vector3 direction = player1.transform.position - player2.transform.position;
            return Mathf.Abs(direction.x);
        }

        void ComputeZoom(float _distance)
        {
            float raw_zoom = _distance / attenuationFactor;
            float smooth_zoom = Mathf.Lerp(gameCamera.orthographicSize, raw_zoom, smoothness);
            // print("smooth_zoom : " + smooth_zoom);

            if (MaxZoomInReached(smooth_zoom) || MaxZoomOutReached(smooth_zoom))
                return;

            gameCamera.orthographicSize = smooth_zoom;
        }

        bool AreCloseEnough(float _distance)
        {
            return _distance <= maxZoomInDistance;
        }

        bool MaxZoomInReached(float _value)
        {
            return _value <= maxZoomInDistance;
        }

        bool MaxZoomOutReached(float _value)
        {
            return _value >= maxZoomOutDistance;
        }

        public void ComputeScroll(Collider2D _entity, EBorderSide _collider_side, EColliderCallbackType _callback_type)
        {
            if (_collider_side == EBorderSide.RIGHT)
                SetForwardMask(_entity, _callback_type);
            else if (_collider_side == EBorderSide.LEFT)
                SetBackwardMask(_entity, _callback_type);

            //print("forwardScrollMask = " + forwardScrollMask);
            //print("backwardScrollMask = " + backwardScrollMask);

            byte mask = 0 | ((1 << 1) | (1 << 2)); // mask = 6;
            if ((forwardScrollMask ^ mask) == 0)
                ForwardScroll();
            else if ((backwardScrollMask ^ mask) == 0)
                BackwardScroll();
        }

        public void SetForwardMask(Collider2D _entity, EColliderCallbackType _callback_type)
        {
            if (_entity.gameObject == player1)
            {
                print(_entity.gameObject.name);
                if (_callback_type == EColliderCallbackType.STAY)
                    forwardScrollMask |= 1 << 1;
                else if (_callback_type == EColliderCallbackType.EXIT)
                    forwardScrollMask ^= 1 << 1;
            }
            else if (_entity.gameObject == player2)
            {
                print(_entity.gameObject.name);
                if (_callback_type == EColliderCallbackType.STAY)
                    forwardScrollMask |= 1 << 2;
                else if (_callback_type == EColliderCallbackType.EXIT)
                    forwardScrollMask ^= 1 << 2;
            }
        }

        public void SetBackwardMask(Collider2D _entity, EColliderCallbackType _callback_type)
        {
            print("SetBackwardMask()");
            if (_entity.gameObject == player1)
            {
                print(_entity.gameObject.name);
                if (_callback_type == EColliderCallbackType.STAY)
                    backwardScrollMask |= 1 << 1;
                else if (_callback_type == EColliderCallbackType.EXIT)
                    backwardScrollMask ^= 1 << 1;
            }
            else if (_entity.gameObject == player2)
            {
                print(_entity.gameObject.name);
                if (_callback_type == EColliderCallbackType.STAY)
                    backwardScrollMask |= 1 << 2;
                else if (_callback_type == EColliderCallbackType.EXIT)
                    backwardScrollMask ^= 1 << 2;
            }
        }

        public void ForwardScroll()
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }

        public void BackwardScroll()
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
    }
}