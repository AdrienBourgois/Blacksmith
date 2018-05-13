using UnityEngine;

namespace Game.Scripts.ComboSystem
{
    [System.Serializable]
    public abstract class ACommand : ScriptableObject
    {
        public delegate void CommandDelegate();

        private CommandDelegate commandFunction;

        public void SubscribeToCommandFunction(CommandDelegate _function_pointer)
        {
            commandFunction += _function_pointer;
        }

        public void UnsubscribeToCommandFunction(CommandDelegate _function_pointer)
        {
            if (commandFunction != null)
                commandFunction -= _function_pointer;
        }

        protected abstract void Execute();
    }
}