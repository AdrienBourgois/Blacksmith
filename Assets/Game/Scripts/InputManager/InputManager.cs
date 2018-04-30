using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Scripts.InputManager
{
    public class InputManager : MonoBehaviour
    {
        public delegate void AxisCallback(float _axe);
        public delegate void GamePadButtonEvent();

        private event AxisCallback HorizontalP1Event;
        private event AxisCallback VerticalP1Event;
        private event GamePadButtonEvent JumpReviveP1Event;
        private event GamePadButtonEvent WeakAttackP1Event;
        private event GamePadButtonEvent StrongAttackP1Event;
        private event GamePadButtonEvent SpecialMoveP1Event;
        private event GamePadButtonEvent FusionP1Event;
        private event GamePadButtonEvent PauseP1Event;

        private event AxisCallback HorizontalP2Event;
        private event AxisCallback VerticalP2Event;
        private event GamePadButtonEvent JumpReviveP2Event;
        private event GamePadButtonEvent WeakAttackP2Event;
        private event GamePadButtonEvent StrongAttackP2Event;
        private event GamePadButtonEvent SpecialMoveP2Event;
        private event GamePadButtonEvent FusionP2Event;
        private event GamePadButtonEvent PauseP2Event;

        private static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                Assert.IsNotNull(instance, "Error : InputManager.instance = null.");
                return instance;
            }
        }

        #region SubscribeToEvents
        public void SubscribeToHorizontalP1Event(AxisCallback _function_to_bind)
        {
            HorizontalP1Event += _function_to_bind;
        }

        public void SubscribeToVerticalP1Event(AxisCallback _function_to_bind)
        {
            VerticalP1Event += _function_to_bind;
        }

        public void SubscribeToJumpReviveP1Event(GamePadButtonEvent _function_to_bind)
        {
            JumpReviveP1Event += _function_to_bind;
        }

        public void SubscribeToWeakAttackP1Event(GamePadButtonEvent _function_to_bind)
        {
            WeakAttackP1Event += _function_to_bind;
        }

        public void SubscribeToStrongAttackP1Event(GamePadButtonEvent _function_to_bind)
        {
            StrongAttackP1Event += _function_to_bind;
        }

        public void SubscribeToSpecialMoveP1Event(GamePadButtonEvent _function_to_bind)
        {
            SpecialMoveP1Event += _function_to_bind;
        }

        public void SubscribeToFusionP1Event(GamePadButtonEvent _function_to_bind)
        {
            FusionP1Event += _function_to_bind;
        }

        public void SubscribeToPauseP1Event(GamePadButtonEvent _function_to_bind)
        {
            PauseP1Event += _function_to_bind;
        }

        public void SubscribeToHorizontalP2Event(AxisCallback _function_to_bind)
        {
            HorizontalP2Event += _function_to_bind;
        }

        public void SubscribeToVerticalP2Event(AxisCallback _function_to_bind)
        {
            VerticalP2Event += _function_to_bind;
        }

        public void SubscribeToJumpReviveP2Event(GamePadButtonEvent _function_to_bind)
        {
            JumpReviveP2Event += _function_to_bind;
        }

        public void SubscribeToWeakAttackP2Event(GamePadButtonEvent _function_to_bind)
        {
            WeakAttackP2Event += _function_to_bind;
        }

        public void SubscribeToStrongAttackP2Event(GamePadButtonEvent _function_to_bind)
        {
            StrongAttackP2Event += _function_to_bind;
        }

        public void SubscribeToSpecialMoveP2Event(GamePadButtonEvent _function_to_bind)
        {
            SpecialMoveP2Event += _function_to_bind;
        }

        public void SubscribeToFusionP2Event(GamePadButtonEvent _function_to_bind)
        {
            FusionP2Event += _function_to_bind;
        }

        public void SubscribeToPauseP2Event(GamePadButtonEvent _function_to_bind)
        {
            PauseP2Event += _function_to_bind;
        }
        #endregion

        #region UnsubscribeFromEvents
        public void UnsubscribeFromHorizontalP1Event(AxisCallback _function_to_unbind)
        {
            HorizontalP1Event -= _function_to_unbind;
        }

        public void UnsubscribeFromVerticalP1Event(AxisCallback _function_to_unbind)
        {
            VerticalP1Event -= _function_to_unbind;
        }

        public void UnsubscribeFromJumpReviveP1Event(GamePadButtonEvent _function_to_un_bind)
        {
            JumpReviveP1Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromWeakAttackP1Event(GamePadButtonEvent _function_to_un_bind)
        {
            WeakAttackP1Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromStrongAttackP1Event(GamePadButtonEvent _function_to_un_bind)
        {
            StrongAttackP1Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromSpecialMoveP1Event(GamePadButtonEvent _function_to_un_bind)
        {
            SpecialMoveP1Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromFusionP1Event(GamePadButtonEvent _function_to_un_bind)
        {
            FusionP1Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromPauseP1Event(GamePadButtonEvent _function_to_un_bind)
        {
            PauseP1Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromHorizontalP2Event(AxisCallback _function_to_unbind)
        {
            HorizontalP2Event -= _function_to_unbind;
        }

        public void UnsubscribeFromVerticalP2Event(AxisCallback _function_to_unbind)
        {
            VerticalP2Event -= _function_to_unbind;
        }

        public void UnsubscribeFromJumpReviveP2Event(GamePadButtonEvent _function_to_un_bind)
        {
            JumpReviveP2Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromWeakAttackP2Event(GamePadButtonEvent _function_to_un_bind)
        {
            WeakAttackP2Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromStrongAttackP2Event(GamePadButtonEvent _function_to_un_bind)
        {
            StrongAttackP2Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromSpecialMoveP2Event(GamePadButtonEvent _function_to_un_bind)
        {
            SpecialMoveP2Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromFusionP2Event(GamePadButtonEvent _function_to_un_bind)
        {
            FusionP2Event -= _function_to_un_bind;
        }

        public void UnsubscribeFromPauseP2Event(GamePadButtonEvent _function_to_un_bind)
        {
            PauseP2Event -= _function_to_un_bind;
        }
        #endregion

        private void Awake()
        {
            instance = this;
        }

        private void Update ()
        {
            float horizontal_p1_axe = Input.GetAxis("Horizontal_P1");
            float vertical_p1_axe = Input.GetAxis("Vertical_P1");
            float horizontal_p2_axe = Input.GetAxis("Horizontal_P2");
            float vertical_p2_axe = Input.GetAxis("Vertical_P2");

            if (horizontal_p1_axe != 0f)
                if(HorizontalP1Event != null)
                    HorizontalP1Event(horizontal_p1_axe);
            if (vertical_p1_axe != 0f)
                if(VerticalP1Event != null)
                    VerticalP1Event(vertical_p1_axe);
            if (horizontal_p2_axe != 0f)
                if (HorizontalP2Event != null)
                    HorizontalP2Event(horizontal_p2_axe);
            if (vertical_p2_axe != 0f)
                if (VerticalP2Event != null)
                    VerticalP2Event(vertical_p2_axe);

            if (Input.GetButtonDown("Jump/Revive_P1"))
                if (JumpReviveP1Event != null)
                    JumpReviveP1Event();
            if (Input.GetButtonDown("Jump/Revive_P2"))
                if (JumpReviveP2Event != null)
                    JumpReviveP2Event();
            if (Input.GetButtonDown("WeakAttack_P1"))
                if (WeakAttackP1Event != null)
                    WeakAttackP1Event();
            if (Input.GetButtonDown("WeakAttack_P2"))
                if (WeakAttackP2Event != null)
                    WeakAttackP2Event();
            if (Input.GetButtonDown("StrongAttack_P1"))
                if (StrongAttackP1Event != null)
                    StrongAttackP1Event();
            if (Input.GetButtonDown("StrongAttack_P2"))
                if (StrongAttackP2Event != null)
                    StrongAttackP2Event();
            if (Input.GetButtonDown("SpecialMove_P1"))
                if (SpecialMoveP1Event != null)
                    SpecialMoveP1Event();
            if (Input.GetButtonDown("SpecialMove_P2"))
                if (SpecialMoveP2Event != null)
                    SpecialMoveP2Event();
            if (Input.GetButtonDown("Fusion_P1"))
                if (FusionP1Event != null)
                    FusionP1Event();
            if (Input.GetButtonDown("Fusion_P2"))
                if (FusionP2Event != null)
                    FusionP2Event();
            if (Input.GetButtonDown("Pause_P1"))
                if (PauseP1Event != null)
                    PauseP1Event();
            if (Input.GetButtonDown("Pause_P2"))
                if (PauseP2Event != null)
                    PauseP2Event();
        }
    }
}
