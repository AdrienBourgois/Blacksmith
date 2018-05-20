using UnityEngine;
using Game.Scripts.InputManager;

namespace Game.Scripts.ComboSystem
{
    [CreateAssetMenu(fileName = "HeavyAttackCommand", menuName = "Combo/AttackType/HeavyAttack", order = 1)]
    public class SOHeavyAttackCommand : ACommand
    {
        //private void OnEnable()
        //{
        //    Debug.Log("SOHeavyAttackCommand.OnEnable()");
        //}

        //private void OnDisable()
        //{
        //    //InputManager.InputManager.Instance.UnsubscribeFromStrongAttackP1Event(Execute);
        //}

        public override void Init()
        {
            InputManager.InputManager.Instance.SubscribeToStrongAttackP1Event(Execute);

        }

        protected override void Execute()
        {
            Debug.Log("SOHeavyAttackCommand.Execute()");
            if (commandFunction != null)
                commandFunction(this);
        }
    }
}