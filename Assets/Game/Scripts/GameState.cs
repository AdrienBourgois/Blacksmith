using UnityEngine;

namespace Game.Scripts
{
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

        private void Start()
        {
            DontDestroyOnLoad(this);
            FindObjectOfType<CombatZoneTrigger>().SubscribeToEnterCombatZoneCallback(SwitchToCombatState);
        }

        #endregion

        private void SwitchToCombatState()
        {
            print("SwitchToCombatState");
        }

        private void SwitchGameState(EGameState _new_e_game_state)
        {
            eGameState = _new_e_game_state; 

            // invoke event with state ?
        }
        public void SetIsTwoPlayers(bool _two_player)
        {
            isTwoPlayer = _two_player;
        }

        public void IsGameOver(uint _knocked_count)
        {
            if (_knocked_count == 2)
                GameInstance.Instance.InvokeGameOver();
        }
    }
}
