using UnityEngine;
using Game.Scripts.Entity;

namespace Game.Scripts.ComboSystem
{
    //[System.Serializable]
    public abstract class ACommand
    {
        public delegate void CommandDelegate(ACommand command);

        protected PlayerEntity.EPlayerType playerType;
        protected CommandDelegate commandFunction;
        protected string commandName;

        public string CommandName
        {
            get { return commandName; }
        }

        public abstract void Init(PlayerEntity.EPlayerType _player_type);

        public void Active(CommandDelegate _function_pointer)
        {
            ListenToInputManager();
            SubscribeToCommandFunction(_function_pointer);
        }

        public void Deactive(CommandDelegate _function_pointer)
        {
            StopListenToInputManager();
            UnsubscribeToCommandFunction(_function_pointer);
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

        protected abstract void ListenToInputManager();
        protected abstract void StopListenToInputManager();
        protected abstract void Execute();

    }
}