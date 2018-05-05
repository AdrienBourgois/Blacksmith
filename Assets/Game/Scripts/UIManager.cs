using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts
{
    public class UiManager : MonoBehaviour
    {

        [SerializeField]
        private EventSystem eventSystem;

        private float fury;

        // Use this for initialization
        private void Start () {
		
        }
	
        // Update is called once per frame
        private void Update () {
		
        }

        public void EnableMenu(Transform _menu)
        {
            _menu.gameObject.SetActive(true);

            eventSystem.SetSelectedGameObject(_menu.GetChild(0).gameObject);
        }

        public void DisableMenu(Transform _menu)
        {
            _menu.gameObject.SetActive(false);
        }
    }
}
