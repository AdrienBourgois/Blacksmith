using Game.Scripts.Entity;
using UnityEngine;
using Game.Scripts.InputManager;

namespace Game.Scripts.ComboSystem
{
    //[CreateAssetMenu(fileName = "HeavyAttackCommand", menuName = "Combo/AttackType/HeavyAttack", order = 1)]
    public class SOHeavyAttackCommand : ACommand
    {
        public override void Init(PlayerEntity.EPlayerType _player_type)
        {
            //Debug.Log("SOHeavyAttackCommand");

            if (_player_type == PlayerEntity.EPlayerType.MELEE)
                InputManager.InputManager.Instance.SubscribeToStrongAttackP1Event(Execute);
            else
                InputManager.InputManager.Instance.SubscribeToStrongAttackP2Event(Execute);

            commandName = "HeavyAttackCommand";
        }

        protected override void Execute()
        {
            //Debug.Log("SOHeavyAttackCommand.Execute()");
            if (commandFunction != null)
                commandFunction(this);
        }
    }
}