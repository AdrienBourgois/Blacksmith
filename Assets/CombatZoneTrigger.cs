using UnityEngine;

public class CombatZoneTrigger : MonoBehaviour
{
	public delegate void CombatZoneDelegate(Collider2D _collider, GameObject trigger);

	private CombatZoneDelegate stayCombatZoneCallback;

	public void SubscribeToEnterCombatZoneCallback(CombatZoneDelegate _function_pointer)
	{
		stayCombatZoneCallback += _function_pointer;
	}

	public void UnsubscribeFromEnterCombatZoneCallback(CombatZoneDelegate _function_pointer)
	{
		stayCombatZoneCallback -= _function_pointer;
	}

	private void OnTriggerStay2D(Collider2D _other)
	{
	    //print("OnTriggerStay2D = " + _other.gameObject.name);

        if (stayCombatZoneCallback != null)
			stayCombatZoneCallback(_other, this.gameObject);
	}
}
