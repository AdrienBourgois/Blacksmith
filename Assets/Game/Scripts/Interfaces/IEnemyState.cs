using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void ToIdleState();

    void ToSelectTargetState();

    void ToChaseState(); 

    void ToAttackState();

    void Update();
}
