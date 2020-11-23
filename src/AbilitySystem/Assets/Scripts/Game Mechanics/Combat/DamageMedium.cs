using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageMedium : MonoBehaviour
{
    public Damage.DamagePacket DamagePacket
    {
        get
        {
            return damagePacket;
        }
    }


    Damage.DamagePacket damagePacket;

    protected virtual void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public virtual void AssignDamage(Damage.DamagePacket _damagePacket)
    {
        damagePacket = _damagePacket;
    }
}
