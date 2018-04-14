using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player1;
    [SerializeField]
    private GameObject _player2;
    [SerializeField]
    private float _maxZoomInDistance;
    [SerializeField]
    private float _maxZoomOutDistance;
    [SerializeField]
    private float _attenuationFactor;
    [SerializeField]
    [Range(0f, 1f)]
    private float _smoothnes;

    private Camera _gameCamera;

    private float AspectRatio
    {
        get { return (float)(_gameCamera.pixelWidth) / (float)(_gameCamera.pixelHeight); }
    }

    private float VerticalViewingVolume
    {
        get { return _gameCamera.orthographicSize * 2f; }
    }

    private float HorizontalViewingVolume
    {
        get { return VerticalViewingVolume * AspectRatio; }
    }

    void Start ()
    {
        _gameCamera = this.GetComponent<Camera>();
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
        Vector3 direction = _player1.transform.position - _player2.transform.position;
        return Mathf.Abs(direction.x);
    }

    void ComputeZoom(float distance)
    {
        float raw_zoom = distance / _attenuationFactor;
        float smooth_zoom = Mathf.Lerp(_gameCamera.orthographicSize, raw_zoom, _smoothnes);
       // print("smooth_zoom : " + smooth_zoom);

        if (MaxZoomInReached(smooth_zoom) || MaxZoomOutReached(smooth_zoom))
            return;

        _gameCamera.orthographicSize = smooth_zoom;
    }

    bool AreCloseEnough(float distance)
    {
        return distance <= _maxZoomInDistance;
    }

    bool MaxZoomInReached(float value)
    {
        return value <= _maxZoomInDistance;
    }

    bool MaxZoomOutReached(float value)
    {
        return value >= _maxZoomOutDistance;
    }
}