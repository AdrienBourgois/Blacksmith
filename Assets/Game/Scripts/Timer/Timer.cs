using System;
using System.Runtime.InteropServices;
using UnityEditor.PackageManager.Requests;

namespace Game.Scripts.Timer
{
    [Serializable]
    public class Timer
    {
        private int id;
        private string name;
        private float timeOut;
        private float currentTime;
        private bool loopAtElapsed;
        private E_TIMER_STATE status;
        private TimerManager.TimerDelegate elapsedCallback;

        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public bool LoopAtElapsed
        {
            get { return loopAtElapsed; }
            set { loopAtElapsed = value; }
        }
        public float CurrentTime { get { return currentTime; } }
        public E_TIMER_STATE Status { get { return status; } }

        public Timer(TimerManager.TimerDelegate _listener_function, int _timer_id, string _timer_name, float _elapse_at, bool _start_on_creation, bool _loop_at_elapsed)
        {
            elapsedCallback += _listener_function;
            id = _timer_id;
            name = _timer_name;
            timeOut = _elapse_at;
            currentTime = timeOut;
            loopAtElapsed = _loop_at_elapsed;
            status = _start_on_creation ?  E_TIMER_STATE.RUNNING : E_TIMER_STATE.STOP;
        }

        public void Start()
        {
            status = E_TIMER_STATE.RUNNING;
        }

        public void Pause()
        {
            status = E_TIMER_STATE.PAUSE;
        }

        public void Stop()
        {
            currentTime = timeOut;
            status = E_TIMER_STATE.STOP;
        }

        public void Reset()
        {
            Stop();
            Start();
        }

        public void Update(float _delta_time)
        {
            if (status == E_TIMER_STATE.RUNNING)
            {
                currentTime -= _delta_time;

                if (currentTime <= 0f)
                {
                    if (elapsedCallback != null)
                        elapsedCallback();

                    if (loopAtElapsed == true)
                        Reset();
                    else
                        Stop();
                }
            }
        }
    }
}