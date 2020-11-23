using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Damage;

public class Active001 : Active
{
    public GameObject prefab;

    List<Projectile> projectiles = new List<Projectile>();
    int baseDamage = 35;
    float projectileExitSpeed = 15f;

    private void Awake()
    {
        CreateAbility("Lesser Fireball", "lessfireball", 1, new Damage.Type[1] { Damage.Type.FLAME });

        _resCost = 25f;
        _castTime = 1f;
        _cooldown = 2f;
        _gcd = true;
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
            lessBurnEffect.parameters = new float[2] { 10f, 3f };

            damagePacket.effects.Add(lessBurnEffect);

            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mouseWorldDirection = mouseWorldPosition - new Vector2(transform.position.x, transform.position.y);

            projectiles.Add(Instantiate(prefab, transform).GetComponent<Projectile>());
            projectiles[projectiles.Count - 1].AssignDamage(damagePacket);
            projectiles[projectiles.Count - 1].GetComponent<Rigidbody2D>().AddForce(mouseWorldDirection.normalized * projectileExitSpeed);

            StartCooldown();
        }
    }
}
