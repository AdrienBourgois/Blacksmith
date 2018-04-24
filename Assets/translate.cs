using UnityEngine;

public class Translate : MonoBehaviour {

	// Use this for initialization
    private void Start () {
		
	}
	
	// Update is called once per frame
    private void Update () {
		transform.Translate(Vector3.right * Time.deltaTime);
	}
}
