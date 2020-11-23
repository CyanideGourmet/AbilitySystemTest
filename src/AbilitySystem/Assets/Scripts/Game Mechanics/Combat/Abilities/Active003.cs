using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active003 : Active
{
    public GameObject prefab;

    GameObject area;
    int baseDamage = 20;

    private void Awake()
    {
        CreateAbility("Lesser Flame Cataclysm", "lessflamecata", 3, new Damage.Type[2] { Damage.Type.FLAME, Damage.Type.DARK });

        _resCost = 30f;
        _castTime = 1f;
        _cooldown = 7f;
        _gcd = true;

        area = Instantiate(prefab);
        area.name = GetComponentInParent<Rigidbody2D>().gameObject.name + "'s " + Name;
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
            Damage.DamagePacket damagePacket = new Damage.DamagePacket();
            damagePacket.source = this;
            damagePacket.damageValue = baseDamage * (1 + (attributes.Magic.FindAttribute("Flame").Value * 0.0005f) + (attributes.Magic.FindAttribute("Dark").Value * 0.0005f));
            damagePacket.effects = new List<Damage.DamagePacket.Effect>();

            Damage.DamagePacket.Effect lessBurnEffect = new Damage.DamagePacket.Effect();
            lessBurnEffect.type = typeof(Passive999);
            lessBurnEffect.parameters = new float[2] { 5f * (1 + (attributes.Magic.FindAttribute("Flame").Value * 0.0005f)), 2f + (1 + (attributes.Mind.FindAttribute("Focus").Value * 0.0005f)) };

            damagePacket.effects.Add(lessBurnEffect);

            area.GetComponent<Area>().AssignDamage(damagePacket);

            float radius = 1f + attributes.Magic.FindAttribute("Aptitude").Value * 0.05f;
            float duration = 1f + attributes.Mind.FindAttribute("Focus").Value * 0.005f;

            area.GetComponent<Area>().ChangeParameters(radius, duration);

            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(UnityEngine.InputSystem.Mouse.current.position.ReadValue());

            area.SetActive(true);
            area.transform.position = mouseWorldPosition;

            StartCooldown();
        }
    }
}
