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


			startGame.AddListener(LoadLevel);
			quitGame.AddListener(Application.Quit);

			gameOver.AddListener(() =>
			{
				Destroy(gameObject);
				Destroy(GameObject.Find("EventSystem"));
				Destroy(FindObjectOfType<GameState>().gameObject);
				Destroy(FindObjectOfType<EntityManager>().gameObject);
				Destroy(FindObjectOfType<UiManager>().gameObject);

				print("Game Over");
			});
		}
		#endregion

		#region public Methods
		public void SetLevelToLoad(string _level_to_load)
		{
			levelToLoad = _level_to_load;
		}

		private void LoadLevel()
		{
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

		public void InvokePauseGame(bool _pause)
		{
			if (pauseGame != null)
				pauseGame.Invoke(_pause);
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
}
