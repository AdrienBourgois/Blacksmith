using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

namespace Game.Scripts.ComboSystem
{
    //[CreateAssetMenu(fileName = "LightAttackCommand", menuName = "Combo/AttackType/LightAttack", order = 1)]
    public class ReviveLeftCommand : ACommand
    {
        protected override void ListenToInputManager()
        {
            if (playerType == PlayerEntity.EPlayerType.MELEE)
                InputManager.InputManager.Instance.SubscribeToReviveLBP1Event(Execute);
            else
                InputManager.InputManager.Instance.SubscribeToReviveLBP2Event(Execute);
        }

        protected override void StopListenToInputManager()
        {
            if (playerType == PlayerEntity.EPlayerType.MELEE)
                InputManager.InputManager.Instance.UnsubscribeFromReviveLBP1Event(Execute);
            else
                InputManager.InputManager.Instance.UnsubscribeFromReviveLBP2Event(Execute);
        }

        public override void Init(PlayerEntity.EPlayerType _player_type)
        {
            //Debug.Log("SOLightAttackCommand");

            playerType = _player_type;
            commandName = "ReviveLeftCommand";
        }

        protected override void Execute()
        {
            //Debug.Log("SOLightAttackCommand.Execute()");
            if (commandFunction != null)
                commandFunction(this);
        }
    }
}