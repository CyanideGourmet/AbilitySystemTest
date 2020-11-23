using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive999 : Passive
{
    float damagePerSecond = 13f;
    float duration = 5f;

    protected override void Awake()
    {
        base.Awake();
        CreateAbility("Lesser Burning", "lessburn", 999, new Damage.Type[1] { Damage.Type.FLAME });
    }
    private void Start()
    {
        Destroy(this, duration);
    }
    private void FixedUpdate()
    {
        Damage.DamagePacket damagePacket = new Damage.DamagePacket();
        damagePacket.damageValue = damagePerSecond * Time.deltaTime;
        damagePacket.source = this;
        damagePacket.effects = new List<Damage.DamagePacket.Effect>();
        transform.parent.parent.GetComponentInChildren<DamageReciever>().Recieve(damagePacket);
        Debug.Log(resources.Health.Value);
    }

    public override void AddParameters(float[] parameters)
    {
        if(parameters.Length < 2)
        {
            Debug.LogError("Incorrect Parameters for " + NameCode + " Passive!");
        }
        ChangeDamageAndDuration(parameters[0], parameters[1]);
    }
    public void ChangeDamageAndDuration(float newDamagePerSecond, float newDuration)
    {
        damagePerSecond = newDamagePerSecond;
        duration = newDuration;
    }
}
