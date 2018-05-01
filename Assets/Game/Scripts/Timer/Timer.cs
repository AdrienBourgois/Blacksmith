using System;
using UnityEditor.PackageManager.Requests;

namespace Game.Scripts.Timer
{
    [Serializable]
    public class Timer
    {
        private enum E_TIMER_STATE
        {
            RUNNING,
            PAUSE,
            STOP
        };

        private int id;
        private string name;
        private float timeOut;
        private float currentTime;
        private bool loopAtElapsed;
        private E_TIMER_STATE state;
        private TimerManager.TimerDelegate elapsedCallback;

        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public bool LoopAtElapsed
        {
            get { return loopAtElapsed; }
            set { loopAtElapsed = value; }
        }

        public Timer(TimerManager.TimerDelegate _listener_function, int _timer_id, string _timer_name, float _elapse_at, bool _start_on_creation, bool _loop_at_elapsed)
        {
            elapsedCallback += _listener_function;
            id = _timer_id;
            name = _timer_name;
            timeOut = _elapse_at;
            currentTime = timeOut;
            loopAtElapsed = _loop_at_elapsed;
            state = _start_on_creation ?  E_TIMER_STATE.RUNNING : E_TIMER_STATE.STOP;
        }

        public void Start()
        {
            state = E_TIMER_STATE.RUNNING;
        }

        public void Pause()
        {
            state = E_TIMER_STATE.PAUSE;
        }

        public void Stop()
        {
            currentTime = timeOut;
            state = E_TIMER_STATE.STOP;
        }

        public void Update(float _delta_time)
        {
            if (state == E_TIMER_STATE.RUNNING)
            {
                currentTime -= _delta_time;

                if (currentTime <= 0f)
                    elapsedCallback();

                if(loopAtElapsed == true)
                    Reset();
            }
        }

        private void Reset()
        {
            Stop();
            Start();
        }
    }
}