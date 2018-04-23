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
        [Range(0f, 1f)]
        private float smoothness;

        private UnityEngine.Camera gameCamera;

        private float AspectRatio
        {
            get { return (float)(gameCamera.pixelWidth) / (float)(gameCamera.pixelHeight); }
        }

        public float VerticalViewingVolume
        {
            get { return gameCamera.orthographicSize * 2f; }
        }

        private float HorizontalViewingVolume
        {
            get { return VerticalViewingVolume * AspectRatio; }
        }

        public float HalfHorizontalViewingVolume
        {
            get { return HorizontalViewingVolume / 2f; }
        }

        void Start ()
        {
            gameCamera = this.GetComponent<UnityEngine.Camera>();
        }
	
        void Update ()
        {
            float distance = ComputeDistance();

            ComputeZoom(distance);
            //else
            //    Zoom(_maxZoomInDistance);
            //print("pixelWidth " + _gameCamera.pixelWidth);
            //print("scaledPixelWidth " + _gameCamera.scaledPixelWidth);
            //print("rect.x " + _gameCamera.rect.x);
            //print("rect.y " + _gameCamera.rect.y);
            //print("rect.size " + _gameCamera.rect.size);
            //print("rect.width " + _gameCamera.rect.width);
            //print("rect.heigt " + _gameCamera.rect.height);
            //print("pixelRect.x " + _gameCamera.pixelRect.x);
            //   print("pixelRect.y " + _gameCamera.pixelRect.y);
            //   print("pixelRect.size " + _gameCamera.pixelRect.size);
            //   print("pixelRect.width " + _gameCamera.pixelRect.width);
            //   print("pixelRect.heigt " + _gameCamera.pixelRect.height);
            print("HorizontalViewingVolume = " + HorizontalViewingVolume / 2f);
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
    }
}