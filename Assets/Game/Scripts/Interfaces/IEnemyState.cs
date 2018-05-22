public interface IEnemyState
{
    void ToIdleState();

    void ToSelectTargetState();

    void ToChaseState(); 

    void ToAttackState();

    void Update();
}
