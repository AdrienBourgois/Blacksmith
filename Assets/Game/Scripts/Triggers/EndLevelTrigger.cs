using Game.Scripts.Interfaces;

namespace Game.Scripts.Triggers
{
    public class EndLevelTrigger : ATriggerAction
    {
        public override void Trigger()
        {
            GameInstance.Instance.InvokeGameOver();
        }
    }
}
