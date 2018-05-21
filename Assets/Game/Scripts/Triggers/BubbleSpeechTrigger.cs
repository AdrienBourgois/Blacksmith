using Game.Scripts.Entity;
using Game.Scripts.Interfaces;
using Game.Scripts.Ui;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    public class BubbleSpeechTrigger : MonoBehaviour, ITriggerAction
    {
        [SerializeField] private string text;
        [SerializeField] private float duration;
        [SerializeField] private PlayerEntity.EPlayerType player;

        public void Trigger()
        {
            BubbleSpeechManager.Instance.AddSpeech(new SpeechParameters(text, duration, player));
        }
    }
}
