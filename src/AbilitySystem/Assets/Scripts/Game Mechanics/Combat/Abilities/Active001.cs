using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Active001 : Active
{
    public GameObject prefab;

    List<Projectile> projectiles = new List<Projectile>();
    int baseDamage = 35;
    float projectileExitSpeed = 15f;

    private void Awake()
    {
        CreateAbility("Lesser Fireball", "lessfireball", 1, new Damage.Type[1] { Damage.Type.FLAME });

        _resCost = 25;
        _castTime = 1f;
        _gcd = true;
    }
    protected void Start()
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
            damagePacket.effects = null;
            damagePacket.damageValue = Mathf.FloorToInt(baseDamage * 1 + (attributes.Magic.FindAttribute("Flame").Value * 0.001f));
            damagePacket.effects = new List<System.Type>() { typeof(Passive999) };

            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mouseWorldDirection = mouseWorldPosition - new Vector2(transform.position.x, transform.position.y);

            projectiles.Add(Instantiate(prefab, transform).GetComponent<Projectile>());
            projectiles[projectiles.Count - 1].AssignDamage(damagePacket);
            projectiles[projectiles.Count - 1].GetComponent<Rigidbody2D>().AddForce(mouseWorldDirection.normalized * projectileExitSpeed);
        }
    }
}
