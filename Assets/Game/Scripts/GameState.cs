﻿using Game.Scripts.Triggers.ZoneTrigger;
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

        public bool IsTwoPlayer
        {
            get { return isTwoPlayer; }
        }
        private bool isTwoPlayer;
        private int currentLevel;

        private EGameState eGameState = EGameState.MAIN_MENU;
        private EGamePlayState eGamePlayState = EGamePlayState.EXPLORATION;

        public delegate void GameStateDelegate(EGameState _e_game_state);
        public delegate void GamePlayStateDelegate(EGamePlayState _e_game_play_state);

        private GameStateDelegate gameStateCallback;
        private GamePlayStateDelegate gamePlayStateCallback;

        public enum EGameState
        {
            MAIN_MENU,
            IN_GAME,
            PAUSED
        }

        public enum EGamePlayState
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
            //DontDestroyOnLoad(this);
            //SceneManager.sceneLoaded += OnSceneLoaded;
        }

        #endregion

        #region CallbackSubscription
        public void SubscribeToGameStateCallback(GameStateDelegate _function_pointer)
        {
            gameStateCallback += _function_pointer;
        }

        public void SubscribeToGamePlayStateCallback(GamePlayStateDelegate _function_pointer)
        {
            gamePlayStateCallback += _function_pointer;
        }
        #endregion

        #region CallbackUnsubscription
        public void UnsubscribeToGameStateCallback(GameStateDelegate _function_pointer)
        {
            gameStateCallback -= _function_pointer;
        }

        public void UnsubscribeToGamePlayStateCallback(GamePlayStateDelegate _function_pointer)
        {
            gamePlayStateCallback -= _function_pointer;
        }
        #endregion

        /*void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "3CLevel")
            {
                ZoneTrigger[] triggers = FindObjectsOfType<ZoneTrigger>();
                foreach (ZoneTrigger zone_trigger in triggers)
                    zone_trigger.SubscribeToonStayZoneCallback(ListenToCallback);

                SwitchGameState(EGameState.IN_GAME);
            }
        }*/

        public void StartGame()
        {
            ZoneTrigger[] triggers = FindObjectsOfType<ZoneTrigger>();
            foreach (ZoneTrigger zone_trigger in triggers)
                zone_trigger.SubscribeToonStayZoneCallback(ListenToCallback);

            SwitchGameState(EGameState.IN_GAME);
            SwitchGamePlayState(EGamePlayState.EXPLORATION);
        }

        private void ListenToCallback(Collider2D _other, ZoneTrigger _trigger)
        {
            if (_trigger.Type == EZoneTriggerType.COMBAT)
            {
                if (_other.gameObject.name == "Camera")
                {
                    _trigger.gameObject.SetActive(false);
                    SwitchGamePlayState(EGamePlayState.COMBAT);
                }
            }

            if (_trigger.Type == EZoneTriggerType.GAME_OVER)
            {
                if (_other.gameObject.name == "Camera")
                    GameInstance.Instance.InvokeGameOver();
            }
        }

        private void ListenToCallback()
        {
        }

        private void SwitchGameState(EGameState _new_e_game_state)
        {
            eGameState = _new_e_game_state;

            if (gameStateCallback != null)
                gameStateCallback(eGameState);
        }

        private void SwitchGamePlayState(EGamePlayState _new_e_game_play_state)
        {
            eGamePlayState = _new_e_game_play_state;

            if (gamePlayStateCallback != null)
                gamePlayStateCallback(eGamePlayState);
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