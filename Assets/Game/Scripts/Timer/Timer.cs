using System;

namespace Game.Scripts.Timer
{
    [Serializable]
    public class Timer
    {
        private int id;
        private string name;
        private float timeOut;
        private float currentTime;
        private bool loopAtExpired;
        private ETimerState status;

        private TimerManager.TimerDelegate expiredCallback;
        private TimerManager.TimerDelegate startCallback;
        private TimerManager.TimerDelegate pauseCallback;
        private TimerManager.TimerDelegate stopCallback;
        private TimerManager.TimerDelegate resetCallback;

        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public bool LoopAtExpired
        {
            get { return loopAtExpired; }
            set { loopAtExpired = value; }
        }
        public float CurrentTime { get { return currentTime; } set { currentTime = value; } }
        public ETimerState Status { get { return status; } }

        public Timer(int _timer_id, string _timer_name, float _expire_at, bool _start_on_creation, bool _loop_at_elapsed, TimerManager.TimerDelegate _elapsed_listener_function, TimerManager.TimerDelegate _start_listener_function, TimerManager.TimerDelegate _pause_listener_function, TimerManager.TimerDelegate _stop_listener_function, TimerManager.TimerDelegate _reset_listener_function)
        {
            id = _timer_id;
            name = _timer_name;
            timeOut = _expire_at;
            currentTime = timeOut;
            loopAtExpired = _loop_at_elapsed;

            expiredCallback += _elapsed_listener_function;
            startCallback += _start_listener_function;
            pauseCallback += _pause_listener_function;
            stopCallback += _stop_listener_function;
            resetCallback += _reset_listener_function;

            if (_start_on_creation)
                Start();
            else
                status = ETimerState.STOP;
            
            //status = _start_on_creation ?  E_TIMER_STATE.RUNNING : E_TIMER_STATE.STOP;

        }

        public void Start()
        {
            status = ETimerState.RUNNING;

            if (startCallback != null)
                startCallback();
        }

        public void Pause()
        {
            status = ETimerState.PAUSE;

            if (pauseCallback != null)
                pauseCallback();
        }

        public void Stop()
        {
            status = ETimerState.STOP;
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
            if (status == ETimerState.RUNNING)
            {
                currentTime -= _delta_time;

                if (currentTime <= 0f)
                {
                    if (expiredCallback != null)
                        expiredCallback();

                    if (loopAtExpired)
                        Reset();
                    else
                        Stop();
                }
            }
        }
    }
}