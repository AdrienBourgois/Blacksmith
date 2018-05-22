using Game.Scripts.Entity;

namespace Game.Scripts.ComboSystem
{
    //[CreateAssetMenu(fileName = "LightAttackCommand", menuName = "Combo/AttackType/LightAttack", order = 1)]
    public class SOLightAttackCommand : ACommand
    {
        protected override void ListenToInputManager()
        {
            if (playerType == PlayerEntity.EPlayerType.MELEE)
                InputManager.InputManager.Instance.SubscribeToWeakAttackP1Event(Execute);
            else
                InputManager.InputManager.Instance.SubscribeToWeakAttackP2Event(Execute);
        }

        protected override void StopListenToInputManager()
        {
            if (playerType == PlayerEntity.EPlayerType.MELEE)
                InputManager.InputManager.Instance.UnsubscribeFromWeakAttackP1Event(Execute);
            else
                InputManager.InputManager.Instance.UnsubscribeFromWeakAttackP2Event(Execute);
        }

        public override void Init(PlayerEntity.EPlayerType _player_type)
        {
            //Debug.Log("SOLightAttackCommand");

            playerType = _player_type;
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