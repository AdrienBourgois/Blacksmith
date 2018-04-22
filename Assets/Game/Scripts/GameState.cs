using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    private bool isTwoPlayer;
    private int currentLevel;

    private enum EGameState
    {
        PLAY,
        PAUSE
    }

    private enum EGamePlayState
    {
        COMBAT,
        CINEMATIC
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
