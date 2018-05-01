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
        private TimerManager.TimerDelegate startCallback;
        private TimerManager.TimerDelegate pauseCallback;
        private TimerManager.TimerDelegate stopCallback;
        private TimerManager.TimerDelegate resetCallback;

        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public bool LoopAtElapsed
        {
            get { return loopAtElapsed; }
            set { loopAtElapsed = value; }
        }
        public float CurrentTime { get { return currentTime; } }
        public E_TIMER_STATE Status { get { return status; } }

        public Timer(int _timer_id, string _timer_name, float _elapse_at, bool _start_on_creation, bool _loop_at_elapsed, TimerManager.TimerDelegate _elapsed_listener_function, TimerManager.TimerDelegate _start_listener_function, TimerManager.TimerDelegate _pause_listener_function, TimerManager.TimerDelegate _stop_listener_function, TimerManager.TimerDelegate _reset_listener_function)
        {
            id = _timer_id;
            name = _timer_name;
            timeOut = _elapse_at;
            currentTime = timeOut;
            loopAtElapsed = _loop_at_elapsed;

            elapsedCallback += _elapsed_listener_function;
            startCallback += _start_listener_function;
            pauseCallback += _pause_listener_function;
            stopCallback += _stop_listener_function;
            resetCallback += _reset_listener_function;

            if (_start_on_creation == true)
                Start();
            else
                status = E_TIMER_STATE.STOP;
            
            //status = _start_on_creation ?  E_TIMER_STATE.RUNNING : E_TIMER_STATE.STOP;

        }

        public void Start()
        {
            status = E_TIMER_STATE.RUNNING;

            if (startCallback != null)
                startCallback();
        }

        public void Pause()
        {
            status = E_TIMER_STATE.PAUSE;

            if (pauseCallback != null)
                pauseCallback();
        }

        public void Stop()
        {
            status = E_TIMER_STATE.STOP;
            currentTime = timeOut;

            if (stopCallback != null)
                stopCallback();
        }

        public void Reset()
        {
            //Stop();
            //Start();
            currentTime = timeOut;

            if (resetCallback != null)
                resetCallback();
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