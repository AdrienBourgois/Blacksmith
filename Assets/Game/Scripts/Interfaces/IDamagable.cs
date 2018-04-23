using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void ReceiveDamages(int _damages);

    void Die();
}
