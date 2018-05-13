using System.Collections;
using System.Collections.Generic;
using Game.Scripts.ComboSystem;
using Game.Scripts.Timer;
using UnityEngine;

//[System.Serializable]
//public class ComboData<TYPE1, TYPE2>
//{
//    public TYPE2 command;
//    public TYPE1 value;
//}

[System.Serializable]
public class ComboData
{
    public ACommand command;
    public float timeOut;
}


public class SOCombo : MonoBehaviour
{
    //[SerializeField] private ACommand[] commandArray;

    //[System.Serializable] public class FArray : ComboData<float, ACommand> { }

    [SerializeField] private ComboData[] comboArray;

    private int[] timerIdArray;
    //[SerializeField] private Dictionary<int, float> dict;

	void Start () {
        timerIdArray = new int[comboArray.Length];

	    for (int i = 0; i < comboArray.Length; ++i)
	    {
	        timerIdArray[i] = TimerManager.Instance.AddTimer("Combo", comboArray[i].timeOut, false, false, OnTimeExpired);
        }
	}
	
	void Update () {
		
	}

    private void OnTimeExpired()
    {
        print("Combo Failed");
    }
}