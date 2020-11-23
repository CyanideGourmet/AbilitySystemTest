using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damage;

public class Active002 : Active
{
    public GameObject prefab;

    GameObject area;
    int baseDamage = 20;

    private void Awake()
    {
        CreateAbility("Lesser Flame Circle", "lessflamecircle", 2, new Damage.Type[1] { Damage.Type.FLAME });

        _resCost = 30f;
        _castTime = 0.5f;
        _cooldown = 5f;
        _gcd = true;

        area = Instantiate(prefab, transform);
    }
    private void Start()
    {
        _resType = resources.Mana.Name;
    }

    public override void Use()
    {
        base.Use();
        if (resources.Mana.LoseResource(ResourceCost))
        {
            DamagePacket damagePacket = new DamagePacket();
            damagePacket.source = this;
            damagePacket.damageValue = baseDamage * (1 + (attributes.Magic.FindAttribute("Flame").Value * 0.001f));
            damagePacket.effects = new List<DamagePacket.Effect>();

            DamagePacket.Effect lessBurnEffect = new DamagePacket.Effect();
            lessBurnEffect.type = typeof(Passive999);
            lessBurnEffect.parameters = new float[2] { 8f, 2f };

            damagePacket.effects.Add(lessBurnEffect);

            area.GetComponent<Area>().AssignDamage(damagePacket);

            float radius = 1f + attributes.Magic.FindAttribute("Aptitude").Value * 0.05f;
            float duration = 1f + attributes.Mind.FindAttribute("Focus").Value * 0.005f;

            area.GetComponent<Area>().ChangeParameters(radius, duration);
            area.SetActive(true);

            StartCooldown();
        }
    }
}
