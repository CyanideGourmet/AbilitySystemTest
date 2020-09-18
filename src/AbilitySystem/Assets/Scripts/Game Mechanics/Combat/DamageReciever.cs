using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damage;

public sealed class DamageReciever : MonoBehaviour
{
    Resources resources;
    GameObject passives;
    static GameObject deadEntitiesBin;

    private void Awake()
    {
        resources = GetComponent<Resources>();
        passives = resources.transform.Find("Passive").gameObject;
        if(deadEntitiesBin == null)
        {
            deadEntitiesBin = new GameObject();
            deadEntitiesBin.name = "Dead Entities Bin";
            deadEntitiesBin.transform.position = Vector3.zero;
        }
    }
    public void Recieve(DamagePacket damagePacket)
    {
        foreach (System.Type effect in damagePacket.effects) passives.AddComponent(effect);
        resources.Health.LoseResource(damagePacket.damageValue, true);
        if(resources.Health.Value == 0)
        {
            resources.Stamina.LoseResourcePercentage(100f);
            resources.Mana.LoseResourcePercentage(100f);
            transform.parent.SetParent(deadEntitiesBin.transform);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
