using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
	public delegate void ZoneTriggerDelegate(Collider2D _collider, GameObject trigger);

	private ZoneTriggerDelegate onStayZoneCallback;

	public void SubscribeToonStayZoneCallback(ZoneTriggerDelegate _function_pointer)
	{
	    onStayZoneCallback += _function_pointer;
	}

	public void UnsubscribeFromonStayZoneCallback(ZoneTriggerDelegate _function_pointer)
	{
	    onStayZoneCallback -= _function_pointer;
	}

	private void OnTriggerStay2D(Collider2D _other)
	{
        //print("OnTriggerStay2D = " + _other.gameObject.name);

	    if (onStayZoneCallback != null)
	        onStayZoneCallback(_other, this.gameObject);
	}
}
