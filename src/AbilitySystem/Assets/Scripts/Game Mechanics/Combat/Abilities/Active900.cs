using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active900 : Active
{
    private int gainedAtt = 50;

    private void Awake()
    {
        CreateAbility("Increase Vitality", "addvit", 900, null);

        _resCost = 0;
        _castTime = 0f;
        _gcd = false;
    }
    protected void Start()
    {
        _resType = resources.Mana.Name;
    }

    public override void Use()
    {
        base.Use();
        attributes.Body.FindAttribute("Vitality").Add(gainedAtt);

        Debug.Log(gainedAtt + " of Vitality was added to the Player");
        Debug.Log("Maximum Health is now " + resources.Health.Value);
    }
}
