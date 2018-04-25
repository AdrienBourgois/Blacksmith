using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

public class TmpEnemyEntity : AttackEntity {

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (soAttack.CanAttack())
            soAttack.LightGroundedAttack();
    }

    public override void Die()
    {
        base.Die();

        EntityManager.Instance.EnemyNum -= 1;
        this.gameObject.SetActive(false);
        // notify entity manager
    }
}
