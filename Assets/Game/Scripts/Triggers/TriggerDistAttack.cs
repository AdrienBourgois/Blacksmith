using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

public class TriggerDistAttack : TriggerBaseAttack {

    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (!_other.gameObject.CompareTag(tag))
        {
            BaseEntity entity = _other.gameObject.GetComponent<BaseEntity>();
            if (entity != null)
            {
                onEntityHit(entity, damages);
                Destroy(gameObject);
            }
        }
    }
}
