using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageMedium
{
    protected override void Start()
    {
        base.Start();
        Destroy(this.gameObject, 10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DamagePacket.source.transform.parent.parent.gameObject == collision.gameObject) return;
        DamageReciever damageReciever = collision.GetComponentInChildren<DamageReciever>();
        if(damageReciever != null)
        {
            damageReciever.Recieve(DamagePacket);
        }
        Destroy(this.gameObject);
    }
}
