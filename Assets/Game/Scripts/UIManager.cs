using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnableMenu(Transform menu)
    {
        menu.gameObject.SetActive(true);
    }

    public void DisableMenu(Transform menu)
    {
        menu.gameObject.SetActive(false);
    }
}
