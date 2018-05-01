using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Timer;
using UnityEngine;

public class TimerCreator : MonoBehaviour
{
    private int id1;
    private int id2;
    // Use this for initialization
    void Start ()
	{
	    id1 = TimerManager.Instance.AddTimer("OnElapsed1", 5f, true, false, OnElapased1);
	    id2 = TimerManager.Instance.AddTimer("OnElapased2", 3f, true, true, OnElapased2);
	}
	
	// Update is called once per frame
	void Update () {
		//TimerManager.Instance.Display(id);
	 //   E_TIMER_STATE state = TimerManager.Instance.GetStatus(id);
	 //   print((int) state);
	}

    private void OnElapased1()
    {
        print("OnElapased1");
    }

    private void OnElapased2()
    {
        print("OnElapased2");
    }
}
