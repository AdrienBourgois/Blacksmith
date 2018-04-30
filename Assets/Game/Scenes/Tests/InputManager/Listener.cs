using UnityEngine;

namespace Game.Scenes.Tests.InputManager
{
    public class Listener : MonoBehaviour
    {
        public int playerId;

        private void Start ()
        {
            if (playerId == 1)
                ListenToP1();
            else
                ListenToP2();
        }

        private void ListenToP1()
        {
            Scripts.InputManager.InputManager.Instance.SubscribeToHorizontalP1Event(Horizontal);
            Scripts.InputManager.InputManager.Instance.SubscribeToVerticalP1Event(Vertical);
            Scripts.InputManager.InputManager.Instance.SubscribeToJumpReviveP1Event(JumpRevive);
            Scripts.InputManager.InputManager.Instance.SubscribeToWeakAttackP1Event(WeakAttack);
            Scripts.InputManager.InputManager.Instance.SubscribeToStrongAttackP1Event(StrongAttack);
            Scripts.InputManager.InputManager.Instance.SubscribeToSpecialMoveP1Event(SpecialMove);
            Scripts.InputManager.InputManager.Instance.SubscribeToFusionP1Event(Fusion);
            Scripts.InputManager.InputManager.Instance.SubscribeToPauseP1Event(Pause);
        }

        private void ListenToP2()
        {
            Scripts.InputManager.InputManager.Instance.SubscribeToHorizontalP2Event(Horizontal);
            Scripts.InputManager.InputManager.Instance.SubscribeToVerticalP2Event(Vertical);
            Scripts.InputManager.InputManager.Instance.SubscribeToJumpReviveP2Event(JumpRevive);
            Scripts.InputManager.InputManager.Instance.SubscribeToWeakAttackP2Event(WeakAttack);
            Scripts.InputManager.InputManager.Instance.SubscribeToStrongAttackP2Event(StrongAttack);
            Scripts.InputManager.InputManager.Instance.SubscribeToSpecialMoveP2Event(SpecialMove);
            Scripts.InputManager.InputManager.Instance.SubscribeToFusionP2Event(Fusion);
            Scripts.InputManager.InputManager.Instance.SubscribeToPauseP2Event(Pause);
        }

        private void Horizontal(float _axe)
        {
            print(playerId +" Horizontal : " + _axe);
        }

        private void Vertical(float _axe)
        {
            print(playerId + " Vertical : " + _axe);
        }

        private void JumpRevive()
        {
            print(playerId + " JumpRevive");
        }

        private void WeakAttack()
        {
            print(playerId + " WeakAttack");
        }

        private void StrongAttack()
        {
            print(playerId + " StrongAttack");
        }

        private void SpecialMove()
        {
            print(playerId + " SpecialMove");
        }
        private void Fusion()
        {
            print(playerId + " Fusion");
        }
        private void Pause()
        {
            print(playerId + " Pause");
        }
    }
}
