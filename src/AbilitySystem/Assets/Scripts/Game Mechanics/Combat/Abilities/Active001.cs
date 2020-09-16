using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active001 : Active
{
    public GameObject prefab;

    List<Projectile> projectiles = new List<Projectile>();
    int baseDamage = 35;
    float projectileExitSpeed = 15f;

    protected override void Awake()
    {
        base.Awake();
        CreateSpell("Lesser Fireball", "lessfireball", 1, new Damage.Type[1] { Damage.Type.FLAME });
        _resCost = 25;
        _castTime = 1f;
        _gcd = true;
    }
    protected void Start()
    {
        _resType = EntityAbilities.EntityResources.Mana.Name;
    }

    public override void Use()
    {
        base.Use();
        if (EntityAbilities.EntityResources.Mana.LoseResource(ResourceCost))
        {
            Damage.DamagePacket damagePacket = new Damage.DamagePacket();
            damagePacket.source = this;
            damagePacket.effects = null;
            damagePacket.damageValue = Mathf.FloorToInt(baseDamage * 1 + (EntityAbilities.EntityAttributes.Magic.FindAttribute("Flame").Value * 0.001f));

            projectiles.Add(Instantiate(prefab, transform).GetComponent<Projectile>());
            projectiles[projectiles.Count - 1].AssignDamage(damagePacket);
            projectiles[projectiles.Count - 1].GetComponent<Rigidbody2D>().AddForce(transform.parent.parent.up * projectileExitSpeed);
        }
    }
}
