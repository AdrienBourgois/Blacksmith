using System.Collections;
using Game.Scripts.Timer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class UIManager : MonoBehaviour
    {
        static private UIManager instance;
        static public UIManager Instance
        {
            get { return instance; }
        }

        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private Transform healthUi;
        [SerializeField] private Slider furySlider;
        [SerializeField] private Slider fusionSlider;

        [SerializeField] private float maxFury;
        private float fury;

        [SerializeField] [Range(0, 100)] private int percentageLostWhenIt;
        [SerializeField] private float recoveryTimeFusionHit;

        private Coroutine fusionCoroutine;
        private int fusionTimerId;

        private bool inFusionRecoveryHit;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            furySlider.maxValue = maxFury;
            furySlider.value = fury = maxFury;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                furySlider.value = fury = furySlider.maxValue;

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

        public void FusionHit()
        {
            if (inFusionRecoveryHit)
                return;

            float duration = fusionSlider.maxValue;
            Timer.Timer timer = TimerManager.Instance.GetTimer(fusionTimerId);
            duration -= duration * ((1f - (float)percentageLostWhenIt / 100f));
            timer.CurrentTime -= duration;

            TimerManager.Instance.AddTimer("Fusion hit recovery", recoveryTimeFusionHit, true, false, () => inFusionRecoveryHit = false);
            inFusionRecoveryHit = true;
        }

        private IEnumerator FusionCoroutine(int _timer_id, float _max_fusion)
        {
            fusionTimerId = _timer_id;
            fusionSlider.transform.parent.gameObject.SetActive(true);

            healthUi.gameObject.SetActive(false);
            furySlider.transform.parent.gameObject.SetActive(false);

            furySlider.value = fury = 0;

            fusionSlider.maxValue = _max_fusion;
            fusionSlider.value = _max_fusion;

            while (true)
            {
                fusionSlider.value = TimerManager.Instance.GetCurrentTime(fusionTimerId);
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
