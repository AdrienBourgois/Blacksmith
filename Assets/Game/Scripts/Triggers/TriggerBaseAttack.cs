using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Entity;
using UnityEngine;

public class TriggerBaseAttack : MonoBehaviour
{
    public delegate void TriggerHandler(BaseEntity _entity, float _damages);
    public TriggerHandler onEntityHit;

    [HideInInspector] public float damages;
}
