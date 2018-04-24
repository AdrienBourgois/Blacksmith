using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZoneTrigger : MonoBehaviour
{
    public delegate void CombatZoneDelegate();

    private CombatZoneDelegate stayCombatZoneCallback;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SubscribeToEnterCombatZoneCallback(CombatZoneDelegate _function_pointer)
    {
        stayCombatZoneCallback += _function_pointer;
    }

    public void UnsubscribeFromEnterCombatZoneCallback(CombatZoneDelegate _function_pointer)
    {
        stayCombatZoneCallback -= _function_pointer;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (stayCombatZoneCallback != null)
            stayCombatZoneCallback();
    }
}
