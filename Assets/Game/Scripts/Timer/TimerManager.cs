using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Timer
{
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
        }

        private Timer GetTimer(int _id)
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
            return Guid.NewGuid().GetHashCode() + UnityEngine.Random.Range(1, 1000);
        }

        public int AddTimer(TimerDelegate _listener_function, string _timer_name, float _elapse_at, bool _start_on_creation, bool _loop_at_elapsed = false)
        {
            int id = CreateUniqueId();

            timerList.Add(new Timer(_listener_function, id, _timer_name,  _elapse_at, _start_on_creation, _loop_at_elapsed));

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
}