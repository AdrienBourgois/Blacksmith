using Game.Scripts.Interfaces;
using Game.Scripts.Ui;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    public class BubbleSpeechTrigger : MonoBehaviour, ITriggerAction
    {
        [SerializeField] private string text;
        [SerializeField] private float duration;

        public void Trigger()
        {
            BubbleSpeechManager.Instance.AddSpeech(new SpeechParameters(text, duration));
        }
    }
}
