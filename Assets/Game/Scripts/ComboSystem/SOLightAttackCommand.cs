using System.Runtime.InteropServices;
using UnityEngine;
using Game.Scripts.InputManager;

namespace Game.Scripts.ComboSystem
{
    [CreateAssetMenu(fileName = "LightAttackCommand", menuName = "Combo/AttackType/LightAttack", order = 1)]
    public class SOLightAttackCommand : ACommand
    {
        public override void Init()
        {
            //Debug.Log("Command.Init()");
            InputManager.InputManager.Instance.SubscribeToWeakAttackP1Event(Execute);
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