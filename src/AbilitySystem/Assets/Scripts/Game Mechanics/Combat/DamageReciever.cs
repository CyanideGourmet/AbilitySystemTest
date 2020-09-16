using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damage;

public sealed class DamageReciever : MonoBehaviour
{
    Resources resources;

    private void Awake()
    {
        resources = GetComponent<Resources>();
    }
    public void Recieve(DamagePacket damagePacket)
    {
        resources.Health.LoseResource(damagePacket.damageValue, true);
        if(resources.Health.Value == 0)
        {
            resources.Stamina.LoseResourcePercentage(100f);
            resources.Mana.LoseResourcePercentage(100f);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
