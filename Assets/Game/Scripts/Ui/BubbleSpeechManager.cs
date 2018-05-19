using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Ui
{
    public struct SpeechParameters
    {
        public string text;
        public float duration;

        public SpeechParameters(string _text, float _duration)
        {
            text = _text;
            duration = _duration;
        }
    }

    public class BubbleSpeechManager : MonoBehaviour
    {
        public static BubbleSpeechManager Instance { get; private set; }

        [SerializeField]
        private GUISkin guiSkin;

        [SerializeField] private GameObject go;

        private readonly Queue<SpeechParameters> nextSpeeches = new Queue<SpeechParameters>();
        private BubbleSpeech currentBubble;

        private void Awake()
        {
            if (Instance)
            {
                Debug.LogError("BubbleSpeechManager instance already exist !");
                Destroy(this);
                return;
            }

            Instance = this;

            transform.position = Vector3.zero;
        }

        public void AddSpeech(SpeechParameters _parameters)
        {
            nextSpeeches.Enqueue(_parameters);
        }

        private void Update()
        {
            if (currentBubble && currentBubble.isFinished)
            {
                Destroy(currentBubble);
                currentBubble = null;
            }

            if (!currentBubble && nextSpeeches.Count > 0)
            {
                currentBubble = gameObject.AddComponent<BubbleSpeech>();
                currentBubble.SetParameters(nextSpeeches.Dequeue());
                currentBubble.SetGuiStyle(guiSkin.customStyles[0]);
                currentBubble.SetFollowedGameObject(go, new Vector2(-3f, 6f));
            }
        }
    }
}
