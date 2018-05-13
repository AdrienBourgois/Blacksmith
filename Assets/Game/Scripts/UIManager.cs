using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class UiManager : MonoBehaviour
    {
        static private UiManager instance;
        static public UiManager Instance
        {
            get { return instance; }
        }

        [SerializeField]
        private EventSystem eventSystem;

        [SerializeField] private float maxFury;

        private float fury;
        [SerializeField] private Slider furySlider;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            furySlider.value = fury;
        }

        public bool CanAskForFusion()
        {
            return fury == maxFury;
        }

        public void IncreaseFury(int _value)
        {
            if (fury >= maxFury)
                return;

            ++fury;
            furySlider.value = fury;
        }

        public void ShowGameObject(GameObject _game_object)
        {
            _game_object.SetActive(true);
        }

        public void HideGameObject(GameObject _game_object)
        {
            _game_object.SetActive(false);
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
