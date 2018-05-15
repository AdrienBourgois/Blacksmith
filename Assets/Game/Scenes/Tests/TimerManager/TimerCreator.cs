using UnityEngine;

namespace Game.Scenes.Tests.TimerManager
{
    public class TimerCreator : MonoBehaviour
    {
        void Start ()
        {
            Scripts.Timer.TimerManager.Instance.AddTimer("OnElapsed1", 5f, true, false, OnElapased1);
            Scripts.Timer.TimerManager.Instance.AddTimer("OnElapased2", 3f, true, true, OnElapased2);
        }

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

        private void OnElapased3()
        {
            print("OnElapased3");
        }
    }
}
