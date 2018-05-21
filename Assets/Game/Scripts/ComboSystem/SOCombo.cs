using Game.Scripts.ComboSystem;
using Game.Scripts.Timer;
using UnityEngine;

[System.Serializable]
public class ComboData
{
    public ACommand command;
    public float timeOut;
}


public class SOCombo : ScriptableObject
{
    [SerializeField] private ComboData[] commandArray;

    private int[] timerIdArray;
    private int comboIdx;

	private void Init ()
	{
        timerIdArray = new int[commandArray.Length];
	    comboIdx = 0;

	    for (int i = 0; i < commandArray.Length  - 1 /*for the last timer? */; ++i)
	    {
	        timerIdArray[i] = TimerManager.Instance.AddTimer("Combo", commandArray[i].timeOut, false, false, OnTimeExpired);
	        commandArray[i].command.Init();
        }

	    ListenToCallback(commandArray[0].command); 
    }
	
    private void OnCommandFired(ACommand command)
    {
        Debug.Log("OnCommandFired : " + command.CommandName);

        if (comboIdx == commandArray.Length - 1)
        {
            ComboExecuted();
            return;
        }

        if (IsFirstHit())
        {
            TimerManager.Instance.StartTimer(timerIdArray[comboIdx]);
            StopListenToCallback(command);
            ++comboIdx;
            ListenToCallback(commandArray[comboIdx].command);
        }
        else
        {
            TimerManager.Instance.StopTimer(timerIdArray[comboIdx - 1]);
            TimerManager.Instance.StartTimer(timerIdArray[comboIdx]);

            ++comboIdx;

            StopListenToCallback(command);
            ListenToCallback(commandArray[comboIdx].command);
        }

    }

    private void ComboExecuted()
    {
        Debug.Log("ComboExecuted");
        TimerManager.Instance.StopTimer(timerIdArray[comboIdx - 1]);
        StopCombo();
    }

    private bool IsFirstHit()
    {
        return comboIdx == 0;
    }

    private void OnTimeExpired()
    {
        Debug.Log("Combo Failed");
        StopCombo();
    }

    private void StopCombo()
    {
        StopListenToCallback(commandArray[comboIdx].command);

        comboIdx = 0;

        ListenToCallback(commandArray[comboIdx].command);
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