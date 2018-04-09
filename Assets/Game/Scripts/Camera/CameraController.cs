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
    private float _attenuationFactor;

    void Start () {
		
	}
	
	void Update ()
	{
	    float distance = ComputeDistance();
        print("distance : " + distance);

	    if (!AreCloseEnough(distance))
            Zoom(distance);
	}



    float ComputeDistance()
    {
        Vector3 direction = _player1.transform.position - _player2.transform.position;
        return Mathf.Abs(direction.x);
    }

    void Zoom(float distance)
    {
        float zoom = distance / _attenuationFactor;
        this.GetComponent<Camera>().orthographicSize = zoom;
    }

    bool AreCloseEnough(float distance)
    {

        return distance <= _maxZoomInDistance;
    }
}
