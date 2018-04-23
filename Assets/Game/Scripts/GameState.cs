using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    #region Instance
    private static GameState instance;
    public static GameState Instance
    {
        get { return instance; }
    }
    #endregion

    private bool isTwoPlayer;
    private int currentLevel;

    private EGameState eGameState = EGameState.MAIN_MENU;
    private EGamePlayState eGamePlayState = EGamePlayState.EXPLORATION;

    private enum EGameState
    {
        MAIN_MENU,
        IN_GAME,
        PAUSED
    }

    private enum EGamePlayState
    {
        COMBAT,
        EXPLORATION,
        CINEMATIC
    }

    #region Unity Methods

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {

    }
    #endregion

    private void SwitchGameState(EGameState _new_e_game_state)
    {
        eGameState = _new_e_game_state;
    }
    public void SetIsTwoPlayers(bool _twoPlayer)
    {
        isTwoPlayer = _twoPlayer;
    }

    public void IsGameOver(uint _knockedCount)
    {
        if (_knockedCount == 2)
            GameInstance.Instance.InvokeGameOver();
    }
}
