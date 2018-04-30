using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

[Serializable]
public class Timer
{
    private int id;
    private string name;
    private float countdown;
    private bool loopAtElapsed;
    private TimerManager.TimerDelegate elapsedCallback;

    public int Id { get { return id; } }
    public string Name { get { return name; } }
    public bool LoopAtElapsed
    {
        get { return loopAtElapsed; }
        set { loopAtElapsed = value; }
    }

    public Timer(TimerManager.TimerDelegate _listener_function, int _timer_id, string _timer_name, float _elapse_at, bool _loop_at_elapsed)
    {
        elapsedCallback += _listener_function;
        id = _timer_id;
        name = _timer_name;
        countdown = _elapse_at;
        loopAtElapsed = _loop_at_elapsed;
    }

    public void Start()
    {

    }

    public void Pause()
    {

    }

    public void Stop()
    {

    }

    public void Update(float _delta_time)
    {

    }
}

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private List<Timer> timerList;

    public delegate void TimerDelegate();

    private void Awake()
    {
        timerList = new List<Timer>();
    }

	private void Update ()
	{
	    float delta_time = Time.deltaTime;

	    foreach (Timer timer in timerList)
	        timer.Update(delta_time);

	    int i = Guid.NewGuid().GetHashCode();
	    i += UnityEngine.Random.Range(1, 1000);
	    //i += (int)(Time.realtimeSinceStartup);

        print("i = " + i);
    }

    private Timer GetTimer(int _id)
    {
        foreach (Timer timer in timerList)
            if (timer.Id == _id)
                return timer;

        Debug.LogError("Error : the timer with id " + _id + "was not found.");

        return null;
    }

    private Timer GetTimer(string _name)
    {
        foreach (Timer timer in timerList)
            if (timer.Name == _name)
                return timer;

        Debug.LogError("Error : the timer named " + _name + "was not found.");

        return null;
    }

    ///**************************************///
    /// ADD A PARAMETER CALLED "START ON CREATION" ///
    ///**************************************///

    public int AddTimer(TimerDelegate _listener_function, string _timer_name, float _elapse_at, bool _loop_at_elapsed = false)
    {
        int id = Guid.NewGuid().GetHashCode() + UnityEngine.Random.Range(1, 1000);

        timerList.Add(new Timer(_listener_function, id, _timer_name,  _elapse_at,  _loop_at_elapsed));

        return id;
    }

    public void StartTimer(int _id)
    {
        GetTimer(_id).Start();
    }

    public void StartTimer(string _name)
    {
        GetTimer(_name).Start();
    }

    public void PauseTimer(int _id)
    {
        GetTimer(_id).Pause();
    }

    public void PauseTimer(string _name)
    {
        GetTimer(_name).Pause();
    }

    public void StopTimer(int _id)
    {
        GetTimer(_id).Stop();
    }

    public void StopTimer(string _name)
    {
        GetTimer(_name).Stop();
    }

    public bool GetLoopAtElapsed(int _id)
    {
        return GetTimer(_id).LoopAtElapsed;
    }

    public bool GetLoopAtElapsed(string _name)
    {
        return GetTimer(_name).LoopAtElapsed;
    }

    public void SetLoopAtElapsed(int _id, bool _value)
    {
        GetTimer(_id).LoopAtElapsed = _value;
    }

    public void SetLoopAtElapsed(string _name, bool _value)
    {
        GetTimer(_name).LoopAtElapsed = _value;
    }
}
