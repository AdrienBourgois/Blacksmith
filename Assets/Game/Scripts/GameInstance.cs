using Game.Scripts.Entity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
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
		private class PauseEvent : UnityEvent<bool> { } // bool _pause

		[SerializeField] private PauseEvent pauseGame;

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
			DontDestroyOnLoad(FindObjectOfType<UiManager>());
		}
		#endregion

		#region public Methods
		public void SetLevelToLoad(string _level_to_load)
		{
			levelToLoad = _level_to_load;
		}

	    public void StartGame()
	    {
	        LoadLevel();
	    }

	    public void RestartGame()
	    {

	    }

	    public void PauseGame()
	    {

	    }

	    public void GameOver()
	    {
	        Destroy(gameObject);
	        Destroy(GameObject.Find("EventSystem"));
	        Destroy(FindObjectOfType<GameState>().gameObject);
	        Destroy(FindObjectOfType<EntityManager>().gameObject);
	        Destroy(FindObjectOfType<UiManager>().gameObject);

	        print("Game Over");
        }

	    public void QuitGame()
	    {
            InvokeQuitGame();
	        Application.Quit();
	    }
        #endregion

        private void LoadLevel()
		{
			SceneManager.LoadScene(levelToLoad);
		}

		#region Invokes
		private void InvokeStartGame()
		{
			if (startGame != null)
				startGame.Invoke();
		}

		private void InvokeRestartGame()
		{
			if (restartGame != null)
				restartGame.Invoke();
		}

		private void InvokePauseGame(bool _pause)
		{
			if (pauseGame != null)
				pauseGame.Invoke(_pause);
		}

		private void InvokeGameOver()
		{
			if (gameOver != null)
				gameOver.Invoke();
		}

		private void InvokeQuitGame()
		{
			if (quitGame != null)
				quitGame.Invoke();
		}
		#endregion
	}
}
