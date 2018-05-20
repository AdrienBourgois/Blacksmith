using Game.Scripts.ComboSystem;
using Game.Scripts.Timer;
using UnityEngine;

[System.Serializable]
public class ComboData
{
    public ACommand command;
    public float timeOut;
}


public class SOCombo : MonoBehaviour
{
    [SerializeField] private ComboData[] comboArray;

    private int[] timerIdArray;
    private int comboIdx;

	private void Start ()
	{
        timerIdArray = new int[comboArray.Length];
	    comboIdx = 0;

	    for (int i = 0; i < comboArray.Length  - 1 /*for the last timer? */; ++i)
	    {
	        timerIdArray[i] = TimerManager.Instance.AddTimer("Combo", comboArray[i].timeOut, false, false, OnTimeExpired);
	        comboArray[i].command.Init();
        }

	    ListenToCallback(comboArray[0].command); 
    }
	
    private void OnCommandFired(ACommand command)
    {
        print("OnCommandFired : " + command.CommandName);

        if (comboIdx == comboArray.Length - 1)
        {
            ComboExecuted();
            return;
        }

        //stop the current timer. Stop listen to current command callback.
        if (IsFirstHit())
        {
            TimerManager.Instance.StartTimer(timerIdArray[comboIdx]);
            StopListenToCallback(command);
            ++comboIdx;
            ListenToCallback(comboArray[comboIdx].command);
        }
        else
        {
            TimerManager.Instance.StopTimer(timerIdArray[comboIdx - 1]);
            TimerManager.Instance.StartTimer(timerIdArray[comboIdx]);

            ++comboIdx;

            StopListenToCallback(command);
            ListenToCallback(comboArray[comboIdx].command);
        }

    }

    private void ComboExecuted()
    {
        print("ComboExecuted");
        TimerManager.Instance.StopTimer(timerIdArray[comboIdx - 1]);
        StopCombo();
    }

    private bool IsFirstHit()
    {
        return comboIdx == 0;
    }

    private void OnTimeExpired()
    {
        print("Combo Failed");
        StopCombo();
    }

    private void StopCombo()
    {
        StopListenToCallback(comboArray[comboIdx].command);

        comboIdx = 0;

        ListenToCallback(comboArray[comboIdx].command);
    }

    private void ListenToCallback(ACommand command)
    {
        command.SubscribeToCommandFunction(OnCommandFired);
    }

    private void StopListenToCallback(ACommand command)
    {
        command.UnsubscribeToCommandFunction(OnCommandFired);
    }
}