using UnityEngine;
using Game.Scripts.Entity;

namespace Game.Scripts.ComboSystem
{
    [System.Serializable]
    public abstract class ACommand : ScriptableObject
    {
        public delegate void CommandDelegate(ACommand command);

        protected CommandDelegate commandFunction;
        protected string commandName;

        public string CommandName
        {
            get { return commandName; }
        }

        public void SubscribeToCommandFunction(CommandDelegate _function_pointer)
        {
            commandFunction += _function_pointer;
        }

        public void UnsubscribeToCommandFunction(CommandDelegate _function_pointer)
        {
            if (commandFunction != null)
                commandFunction -= _function_pointer;
        }

        public abstract void Init(PlayerEntity.EPlayerType _player_type);

        protected abstract void Execute();
    }
}