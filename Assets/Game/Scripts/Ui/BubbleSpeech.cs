using UnityEngine;

namespace Game.Scripts.Ui
{
    public class BubbleSpeech : MonoBehaviour
    {
        public bool isFinished;

        private SpeechParameters parameters;
        private GameObject followedGameObject;
        private Vector2 offset;

        private int currentIndex;
        private float currentTime;
        private float characterTimeRatio;

        private GUIStyle guiStyle;

        private const int height = 75;
        private const int width = 150;

        public void SetParameters(SpeechParameters _parameters)
        {
            parameters = _parameters;
            characterTimeRatio = parameters.text.Length / (parameters.duration);
        }

        public void SetFollowedGameObject(GameObject _game_object, Vector2 _offset)
        {
            followedGameObject = _game_object;
            offset = _offset;

            Vector2 position = _game_object.transform.position;
            position += offset;
            transform.position = position;
        }

        public void SetGuiStyle(GUIStyle _style)
        {
            guiStyle = _style;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime > parameters.duration + 1f)
            {
                isFinished = true;
                enabled = false;
            }
        }

        private void OnGUI()
        {
            UpdateText();
            UpdatePosition();
            Display();
        }

        private void UpdateText()
        {
            currentIndex = (int) (characterTimeRatio * currentTime);
        }

        private void UpdatePosition()
        {
            Vector2 position = transform.position;
            Vector2 followed_game_object_position = followedGameObject.transform.position;
            position = Vector2.Lerp(position, followed_game_object_position + offset, 0.6f * Time.deltaTime);
            transform.position = position;
        }

        private void Display()
        {
            string current_text = currentTime < parameters.duration ? parameters.text.Substring(0, currentIndex) : parameters.text;
            GUI.Label(GetRectFromCurrentObject(), current_text, guiStyle);
        }

        private Rect GetRectFromCurrentObject()
        {
            Vector2 position = transform.position;
            position.y *= -1f;
            Vector2 corner = UnityEngine.Camera.main.WorldToScreenPoint(position);
            corner.x -= width / 2f;
            corner.y += height / 2f;
            return new Rect(corner, new Vector2(width, height));
        }
    }
}
