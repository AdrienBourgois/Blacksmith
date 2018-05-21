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

        [Header("Event System")]
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private StandaloneInputModule standAloneInputModule;

        [Header("HUD")]
        [SerializeField] private Transform healthUi;
        [SerializeField] private Slider furySlider;
        [SerializeField] private Slider fusionSlider;

        [Header("Stats")]
        [SerializeField] private float maxFury;
        [SerializeField] [Range(0, 100)] private int percentageLostWhenIt;
        [SerializeField] private float recoveryTimeFusionHit;

        private Coroutine fusionCoroutine;
        private int fusionTimerId;

        private bool inFusionRecoveryHit;
        private GameObject currentMenu;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            furySlider.maxValue = maxFury;
            furySlider.value  = maxFury;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                furySlider.value = furySlider.maxValue;

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

            furySlider.value = 0;

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
            return furySlider.value == maxFury;
        }

        public void IncreaseFury(int _value)
        {
            if (furySlider.value >= maxFury)
                return;

            ++furySlider.value;
        }

        public void EnableMenu(Transform _menu)
        {
            currentMenu = _menu.gameObject;
            _menu.gameObject.SetActive(true);

            eventSystem.SetSelectedGameObject(_menu.GetChild(0).gameObject);
        }

        public void DisableMenu(Transform _menu)
        {
            _menu.gameObject.SetActive(false);
        }

    }
}
