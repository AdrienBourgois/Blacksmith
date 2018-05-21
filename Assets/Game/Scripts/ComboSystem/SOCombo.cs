using Game.Scripts.ComboSystem;
using Game.Scripts.Entity;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Timer;
using UnityEngine;

public enum ECommandType
{
    LIGHT_ATTACK_COMMAND,
    HEAVY_ATTACK_COMMAND
}

[System.Serializable]
public class ComboData
{
    public ECommandType commandType;
    [HideInInspector] public ACommand command;
    public float timeOut;
}

[CreateAssetMenu(fileName = "ComboSequence", menuName = "Combo/ComboSequence", order = 1)]
public class SOCombo : ScriptableObject
{
    [SerializeField] private ComboData[] commandArray;

    [Space]

    [SerializeField] private SoBaseAttack baseAttack;

    private int[] timerIdArray;
    private int comboIdx;
    private TimerManager timerManager;

    private System.Action<SoBaseAttack> ComboExecutedCallback;

	public void Init (PlayerEntity.EPlayerType _player_type, System.Action<SoBaseAttack> _function_pointer)
	{
        //Debug.Log("SOCombo.Init() : " + this.name);

        timerManager = TimerManager.Instance;
        timerIdArray = new int[commandArray.Length - 1];
	    comboIdx = 0;

	    InitTimers();
        InitCommands(_player_type);

        //Debug.Log("comboIdx : " + comboIdx);
	    ActiveCommand(commandArray[comboIdx].command);

	    ComboExecutedCallback = _function_pointer;
	}

    private void InitTimers()
    {
        for (int i = 0; i < commandArray.Length - 1 /* for the last timer? */; ++i)
        {
            timerIdArray[i] = timerManager.AddTimer("Sequence : " + i, commandArray[i].timeOut, false, false, OnTimeExpired);
        }
    }

    private void InitCommands(PlayerEntity.EPlayerType _player_type)
    {
        for (int i = 0; i < commandArray.Length; ++i)
        {
            if (commandArray[i].commandType == ECommandType.LIGHT_ATTACK_COMMAND)
                commandArray[i].command = new SOLightAttackCommand();
            else if (commandArray[i].commandType == ECommandType.HEAVY_ATTACK_COMMAND)
                commandArray[i].command = new SOHeavyAttackCommand();

            commandArray[i].command.Init(_player_type);
        }
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
            timerManager.StartTimer(timerIdArray[comboIdx]);

            ++comboIdx;

            DeactiveCommand(command);
            ActiveCommand(commandArray[comboIdx].command);
        }
        else
        {
            timerManager.StopTimer(timerIdArray[comboIdx - 1]);
            timerManager.StartTimer(timerIdArray[comboIdx]);

            ++comboIdx;

            DeactiveCommand(command);
            ActiveCommand(commandArray[comboIdx].command);
        }
    }

    private void ComboExecuted()
    {
        Debug.Log("ComboExecuted");


        if (ComboExecutedCallback != null)
            ComboExecutedCallback(baseAttack);

        timerManager.StopTimer(timerIdArray[comboIdx - 1]);
        StopCombo();

        //PrintAllRunningTimers();
    }

    private void PrintAllRunningTimers()
    {
        for (int i = 0; i < timerIdArray.Length; ++i)
        {
            if (timerManager.IsRunning(timerIdArray[i]))
                Debug.Log("Timer at index : " + i + " is still running : " + timerIdArray[i]);
        }
    }

    private bool IsFirstHit()
    {
        return comboIdx == 0;
    }

    private void OnTimeExpired()
    {
        Debug.Log("Combo Failed");

        //foreach (int i in timerIdArray)
        //{
        //    Debug.Log("timerIdArray : " + i);
        //}

        StopCombo();
    }

    private void StopCombo()
    {
        DeactiveCommand(commandArray[comboIdx].command);

        comboIdx = 0;

        ActiveCommand(commandArray[comboIdx].command);
    }

    private void ActiveCommand(ACommand command)
    {
        command.Active(OnCommandFired);
    }

    private void DeactiveCommand(ACommand command)
    {
        command.Deactive(OnCommandFired);
    }
}