using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.ComboSystem
{
    //[CreateAssetMenu(fileName = "LightAttackCommand", menuName = "Combo/AttackType/LightAttack", order = 1)]
    public class ReviveRightCommand : ACommand
    {
        protected override void ListenToInputManager()
        {
            if (playerType == PlayerEntity.EPlayerType.MELEE)
                InputManager.InputManager.Instance.SubscribeToReviveRBP1Event(Execute);
            else
                InputManager.InputManager.Instance.SubscribeToReviveRBP2Event(Execute);
        }

        protected override void StopListenToInputManager()
        {
            if (playerType == PlayerEntity.EPlayerType.MELEE)
                InputManager.InputManager.Instance.UnsubscribeFromReviveRBP1Event(Execute);
            else
                InputManager.InputManager.Instance.UnsubscribeFromReviveRBP2Event(Execute);
        }

        public override void Init(PlayerEntity.EPlayerType _player_type)
        {
            //Debug.Log("SOLightAttackCommand");

            playerType = _player_type;
            commandName = "ReviveRightCommand";
        }

        protected override void Execute()
        {
            //Debug.Log("SOLightAttackCommand.Execute()");
            if (commandFunction != null)
                commandFunction(this);
        }
    }
}