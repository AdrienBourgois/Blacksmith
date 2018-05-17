using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Game.Scripts.Timer
{
    public class TimerManager : MonoBehaviour
    {
        private List<Timer> timerList;
        public delegate void TimerDelegate();
        static private TimerManager instance;

        static public TimerManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            timerList = new List<Timer>();
            instance = this;
        }

        private void Update ()
        {
            float delta_time = Time.deltaTime;

            foreach (Timer timer in timerList)
                timer.Update(delta_time);
        }

        public Timer GetTimer(int _id)
        {
            foreach (Timer timer in timerList)
                if (timer.Id == _id)
                    return timer;

            Debug.LogError("Error : the timer with the following id <" + _id + "> was not found.");

            return null;
        }

        private Timer GetTimer(string _name)
        {
            foreach (Timer timer in timerList)
                if (timer.Name == _name)
                    return timer;

            Debug.LogError("Error : the timer named " + _name + " was not found.");

            return null;
        }

        private int CreateUniqueId()
        {
            int id =  Guid.NewGuid().GetHashCode() + Random.Range(1, 1000);

            while(IsIdUnique(id) == false)
                id = Guid.NewGuid().GetHashCode() + Random.Range(1, 1000);

            return id;
        }

        private bool IsIdUnique(int _id)
        {
            foreach (Timer timer in timerList)
                if (timer.Id == _id)
                    return false;

            return true;
        }

        private void SecurityAsserts(string _timer_name, float _expire_at, TimerDelegate _expired_listener_function)
        {
            string error_prefix = "[TimerManager] Error In function AddTimer() : ";
            Assert.AreNotEqual("", _timer_name, error_prefix + "the given _timer_name string must be bigger than 0 characters.");
            Assert.AreNotApproximatelyEqual(0f, _expire_at, error_prefix + "the given _expire_at value must be greater than 0.");
            Assert.IsNotNull(_expired_listener_function, error_prefix + "the given _expired_listener_function must be different than NULL");
        }

        public void DestroyAllTimers()
        {
            foreach (Timer timer in timerList)
            {
                timer.Stop();
            }

            timerList.Clear();
        }

        public int AddTimer(string _timer_name, float _expire_at, bool _start_on_creation, bool _loop_at_expired, TimerDelegate _expired_listener_function)
        {
            SecurityAsserts(_timer_name, _expire_at, _expired_listener_function);

            int id = CreateUniqueId();

            timerList.Add(new Timer(id, _timer_name, _expire_at, _start_on_creation, _loop_at_expired, _expired_listener_function, null, null, null, null));

            return id;
        }

        public int AddTimer(string _timer_name, float _expire_at, bool _start_on_creation, bool _loop_at_expired, TimerDelegate _expired_listener_function, TimerDelegate _start_listener_function, TimerDelegate _pause_listener_function, TimerDelegate _stop_listener_function, TimerDelegate _reset_listener_function)
        {
            SecurityAsserts(_timer_name, _expire_at, _expired_listener_function);

            int id = CreateUniqueId();

            timerList.Add(new Timer(id, _timer_name, _expire_at, _start_on_creation, _loop_at_expired, _expired_listener_function, _start_listener_function, _pause_listener_function, _stop_listener_function, _reset_listener_function));

            return id;
        }

        public void DestroyTimer(int _id)
        {
            bool success = timerList.Remove(GetTimer(_id));

            if (success == false)
                Debug.LogError("Error : the timer with the following id <" + _id + "> can't be successfully destroyed.");
        }

        public void DestroyTimer(string _name)
        {
            bool success = timerList.Remove(GetTimer(_name));

            if (success == false)
                Debug.LogError("Error : the timer named " + _name + " can't be successfully destroyed.");
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

        public void ResetTimer(int _id)
        {
            GetTimer(_id).Reset();
        }

        public void ResetTimer(string _name)
        {
            GetTimer(_name).Reset();
        }

        public bool GetLoopAtExpired(int _id)
        {
            return GetTimer(_id).LoopAtExpired;
        }

        public bool GetLoopAtExpired(string _name)
        {
            return GetTimer(_name).LoopAtExpired;
        }

        public void SetLoopAtExpired(int _id, bool _value)
        {
            GetTimer(_id).LoopAtExpired = _value;
        }

        public void SetLoopAtExpired(string _name, bool _value)
        {
            GetTimer(_name).LoopAtExpired = _value;
        }

        public ETimerState GetStatus(int _id)
        {
            return GetTimer(_id).Status;
        }

        public ETimerState GetStatus(string _name)
        {
            return GetTimer(_name).Status;
        }

        public int GetId(string _name)
        {
            return GetTimer(_name).Id;
        }

        public string GetName(int _id)
        {
            return GetTimer(_id).Name;
        }

        public float GetCurrentTime(int _id)
        {
            return GetTimer(_id).CurrentTime;
        }

        public float GetCurrentTime(string _name)
        {
            return GetTimer(_name).CurrentTime;
        }

        public bool IsRunning(int _id)
        {
            return GetStatus(_id) == ETimerState.RUNNING;
        }

        public bool IsRunning(string _name)
        {
            return GetStatus(_name) == ETimerState.RUNNING;
        }

        public void Display(int _id)
        {
            print("Timer id <" +_id + "> : " + GetCurrentTime(_id));
        }

        public void Display(string _name)
        {
            print("Timer named <" + _name + "> : " + GetCurrentTime(_name));
        }
    }
}