using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private EventSystem eventSystem;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnableMenu(Transform menu)
    {
        menu.gameObject.SetActive(true);

        eventSystem.SetSelectedGameObject(menu.GetChild(0).gameObject);
    }

    public void DisableMenu(Transform menu)
    {
        menu.gameObject.SetActive(false);
    }
}
