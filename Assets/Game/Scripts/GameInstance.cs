﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour {

    #region Instance
    private static GameInstance instance;
    public static GameInstance Instance
    {
        get { return instance; }
    }
    #endregion

    #region UnityEvents

    [SerializeField]
    private UnityEvent startGame;

    [SerializeField]
    private UnityEvent restartGame;

    [System.Serializable]
    public class PauseEvent : UnityEvent<bool> { } // bool _pause

    [SerializeField]
    PauseEvent pauseGame;

    [SerializeField]
    private UnityEvent gameOver;

    [SerializeField]
    private UnityEvent quitGame;
    #endregion

    private string levelToLoad;

    #region Unity Methods

    private void Awake()
    {
        instance = this;
    }

    private void Start ()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(FindObjectOfType<UIManager>());


        startGame.AddListener(LoadLevel);
        quitGame.AddListener(() =>
        {
            Application.Quit();
        });

        gameOver.AddListener(() =>
        {
            Destroy(gameObject);
            Destroy(GameObject.Find("EventSystem"));
            Destroy(FindObjectOfType<GameState>().gameObject);
            Destroy(FindObjectOfType<EntityManager>().gameObject);
            Destroy(FindObjectOfType<UIManager>().gameObject);

            print("Game Over");
        });
    }

    private void Update ()
    {
		
	}
    #endregion

    #region public Methods
    public void SetLevelToLoad(string _levelToLoad)
    {
        levelToLoad = _levelToLoad;
    }

    private void LoadLevel()
    {
        print("Loading scene: " + levelToLoad);

        SceneManager.LoadScene(levelToLoad);
    }
    #endregion

    #region Invokes
    public void InvokeStartGame()
    {
        if (startGame != null)
            startGame.Invoke();
    }

    public void InvokeRestartGame()
    {
        if (restartGame != null)
            restartGame.Invoke();
    }

    public void InvokePauseGame(bool pause)
    {
        if (pauseGame != null)
            pauseGame.Invoke(pause);
    }

    public void InvokeGameOver()
    {
        if (gameOver != null)
            gameOver.Invoke();
    }

    public void InvokeQuitGame()
    {
        if (quitGame != null)
            quitGame.Invoke();
    }
    #endregion
}
