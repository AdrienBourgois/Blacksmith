using System.Runtime.InteropServices;
using UnityEngine;
using Game.Scripts.InputManager;

namespace Game.Scripts.ComboSystem
{
    [CreateAssetMenu(fileName = "LightAttackCommand", menuName = "Combo/AttackType/LightAttack", order = 1)]
    public class SOLightAttackCommand : ACommand
    {
        //private void OnEnable()
        //{
        //    if (Application.isPlaying)
        //        Debug.Log("SOLightAttackCommand.OnEnable()");
        //    InputManager.InputManager.Instance.SubscribeToWeakAttackP1Event(Execute);
        //}

        //private void OnDisable()
        //{
        //    InputManager.InputManager.Instance.UnsubscribeFromWeakAttackP1Event(Execute);
        //}

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