using System;

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
        private float countdown;
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
            countdown = _elapse_at;
            loopAtElapsed = _loop_at_elapsed;
            if (_start_on_creation == false)
                state = E_TIMER_STATE.STOP;
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
}