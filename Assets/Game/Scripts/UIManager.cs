using System.Collections;
using Game.Scripts.Timer;
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

        [SerializeField] Transform healthUi;
        [SerializeField] private Slider furySlider;
        [SerializeField] private Slider fusionSlider;

        private Coroutine fusionCoroutine;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            furySlider.maxValue = maxFury;
            furySlider.value = fury;
        }

        public void StartFusionUi(int _timer_id, float _max_fusion)
        {
            fusionCoroutine = StartCoroutine(FusionCoroutine(_timer_id, _max_fusion));
        }

        public void EndFusionUi()
        {
            StopCoroutine(fusionCoroutine);

            fusionSlider.transform.parent.gameObject.SetActive(false);

            healthUi.gameObject.SetActive(true);
            furySlider.transform.parent.gameObject.SetActive(true);
        }

        private IEnumerator FusionCoroutine(int _timer_id, float _max_fusion)
        {
            fusionSlider.transform.parent.gameObject.SetActive(true);

            healthUi.gameObject.SetActive(false);
            furySlider.transform.parent.gameObject.SetActive(false);

            furySlider.value = fury = 0;

            fusionSlider.maxValue = _max_fusion;
            fusionSlider.value = _max_fusion;

            while (true)
            {
                fusionSlider.value = TimerManager.Instance.GetCurrentTime(_timer_id);
                yield return null;
            }
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
