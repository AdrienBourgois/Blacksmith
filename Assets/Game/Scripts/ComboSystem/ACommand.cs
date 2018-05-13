using UnityEngine;

namespace Game.Scripts.ComboSystem
{
    [System.Serializable]
    public abstract class ACommand : ScriptableObject
    {
        public delegate void CommandDelegate(ACommand command);

        protected CommandDelegate commandFunction;

        public void SubscribeToCommandFunction(CommandDelegate _function_pointer)
        {
            commandFunction += _function_pointer;
        }

        public void UnsubscribeToCommandFunction(CommandDelegate _function_pointer)
        {
            if (commandFunction != null)
                commandFunction -= _function_pointer;
        }

        public abstract void Init();

        protected abstract void Execute();
    }
}