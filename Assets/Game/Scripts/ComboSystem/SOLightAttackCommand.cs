using System.Runtime.InteropServices;
using Game.Scripts.Entity;
using UnityEngine;
using Game.Scripts.InputManager;

namespace Game.Scripts.ComboSystem
{
    [CreateAssetMenu(fileName = "LightAttackCommand", menuName = "Combo/AttackType/LightAttack", order = 1)]
    public class SOLightAttackCommand : ACommand
    {
        public override void Init(PlayerEntity.EPlayerType _player_type)
        {
            //Debug.Log("Command.Init()");

            if (_player_type == PlayerEntity.EPlayerType.MELEE)
                InputManager.InputManager.Instance.SubscribeToWeakAttackP1Event(Execute);
            else
                InputManager.InputManager.Instance.SubscribeToWeakAttackP2Event(Execute);

            commandName = "LightAttackCommand";
        }

        protected override void Execute()
        {
            //Debug.Log("SOLightAttackCommand.Execute()");
            if (commandFunction != null)
                commandFunction(this);
        }
    }
}