using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour {

    #region UnityEvents
    [SerializeField]
    private UnityEvent _startGame;

    [SerializeField]
    private UnityEvent _restartGame;

    [System.Serializable]
    public class PauseEvent : UnityEvent<bool> { }

    [SerializeField]
    PauseEvent _pauseGame;

    [SerializeField]
    private UnityEvent _quitGame;
    #endregion

    #region Unity Methods
    private void Start ()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(Camera.main);

        _startGame.AddListener(() =>
       {
           print("Start Game");
           //SceneManager.LoadScene(1, LoadSceneMode.Single);
       });

        _quitGame.AddListener(() =>
        {
            print("Quit Game");
            Application.Quit();
        });
    }
	
	// Update is called once per frame
	private void Update ()
    {
		
	}
    #endregion

    #region Invokes
    public void InvokeStartGame()
    {
        if (_startGame != null)
            _startGame.Invoke();
    }

    public void InvokeRestartGame()
    {
        if (_restartGame != null)
            _restartGame.Invoke();
    }

    public void InvokePauseGame(bool pause)
    {
        if (_pauseGame != null)
            _pauseGame.Invoke(pause);
    }

    public void InvokeQuitGame()
    {
        if (_quitGame != null)
            _quitGame.Invoke();
    }
    #endregion
}
