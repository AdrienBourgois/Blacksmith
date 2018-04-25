using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

public class TriggerBaseAttack : MonoBehaviour
{
    public delegate void TriggerHandler(BaseEntity _entity);
    public TriggerHandler onEntityHit;

    [HideInInspector] public float damages;
    //[HideInInspector] public string allyTag;

    private void OnCollisionEnter2D(Collision2D _other)
    {
        print("DETECTED: " + _other.gameObject.name);

        if (!_other.gameObject.CompareTag(tag))
        {
            //print("DETECTED: " + _other.gameObject.tag);
            print("DETECTED: " + _other.gameObject.name);

            BaseEntity entity = _other.gameObject.GetComponent<BaseEntity>();
            if (entity != null)
            {
                onEntityHit(entity);
                Destroy(gameObject);
            }
        }
    }
}
