﻿using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Timer;
using UnityEngine;

public class TimerCreator : MonoBehaviour
{
    private int id;
	// Use this for initialization
	void Start ()
	{
	    id = TimerManager.Instance.AddTimer(OnElapased1, "OnElapsed1", 5f, true);
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
}