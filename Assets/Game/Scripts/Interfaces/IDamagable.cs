namespace Game.Scripts.Interfaces
{
    public interface IDamagable
    {
        void ReceiveDamages(float _damages);

        void StartRecovery();

        void Die();
    }
}
