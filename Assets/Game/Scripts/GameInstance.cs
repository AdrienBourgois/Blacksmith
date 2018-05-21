using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using Object = UnityEngine.Object;

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

		[Serializable]
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
            startGame.AddListener(LoadLevel);
            gameOver.AddListener(() =>
            {
                foreach (GameObject game_object in GameObject.FindGameObjectsWithTag("DestroyOnGameOver"))
                {
                    Destroy(game_object);
                }
            });

            quitGame.AddListener(() => { Application.Quit(); });
		}

	    private void Update()
	    {
	        if (Input.GetKeyDown(KeyCode.G))
                InvokeGameOver();
	    }

	    #endregion

		#region public Methods
		public void SetLevelToLoad(string _level_to_load)
		{
		    levelToLoad = _level_to_load;
		}
        #endregion


        [HideInInspector] public Transform currentLevel;
        private void LoadLevel()
        {
            Object prefab = Resources.Load(levelToLoad);
            if (prefab != null)
            {
                currentLevel = ((GameObject)Instantiate(prefab)).transform;
                Debug.Log("is here: " + currentLevel);

                Assert.IsNotNull(currentLevel, "quenelle");
            }
        }

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
