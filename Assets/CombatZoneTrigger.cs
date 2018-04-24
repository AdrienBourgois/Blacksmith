using UnityEngine;

public class CombatZoneTrigger : MonoBehaviour
{
	public delegate void CombatZoneDelegate();

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
		if (stayCombatZoneCallback != null)
			stayCombatZoneCallback();
	}
}
