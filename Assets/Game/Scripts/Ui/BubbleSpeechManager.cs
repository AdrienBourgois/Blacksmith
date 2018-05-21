using System;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.Ui
{
    public struct SpeechParameters
    {
        public string text;
        public float duration;
        public PlayerEntity.EPlayerType player;

        public SpeechParameters(string _text, float _duration, PlayerEntity.EPlayerType _player)
        {
            text = _text;
            duration = _duration;
            player = _player;
        }
    }

    public class BubbleSpeechManager : MonoBehaviour
    {
        public static BubbleSpeechManager Instance { get; private set; }

        [SerializeField]
        private GUISkin guiSkin;

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
                SpeechParameters parameters = nextSpeeches.Dequeue();
                currentBubble.SetParameters(parameters);
                currentBubble.SetGuiStyle(guiSkin.customStyles[0]);
                GameObject go;
                switch (parameters.player)
                {
                    case PlayerEntity.EPlayerType.MELEE:
                        go = EntityManager.Instance.MeleePlayer.gameObject;
                        break;
                    case PlayerEntity.EPlayerType.RANGE:
                        go = EntityManager.Instance.RangePlayer.gameObject;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                currentBubble.SetFollowedGameObject(go, new Vector2(-3f, 6f));
            }
        }
    }
}
