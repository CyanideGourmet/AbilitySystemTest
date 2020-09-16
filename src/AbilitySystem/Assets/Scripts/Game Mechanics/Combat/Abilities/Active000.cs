using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Active000 : Active
{
    public GameObject prefab;

    GameObject guide;

    protected override void Awake()
    {
        base.Awake();
        CreateSpell("Light Guide", "lightguide", 0, null);
        _resCost = 12;
        _castTime = 0f;
        _gcd = false;
        guide = Instantiate(prefab, transform);
        guide.SetActive(false);
    }

    protected void Start()
    {
        _resType = EntityAbilities.EntityResources.Mana.Name;
    }

    public override void Use()
    {
        base.Use();
        bool isActive = guide.activeSelf;
        if (isActive)
        {
            guide.SetActive(false);
        }
        else if (EntityAbilities.EntityResources.Mana.LoseResource(ResourceCost))
        {
            guide.GetComponent<Light2D>().pointLightOuterRadius = 5 * (1 + EntityAbilities.EntityAttributes.Magic.FindAttribute("Light").Value * 0.01f);
            guide.SetActive(true);
            Debug.Log("Light Spell used UWU. " + EntityAbilities.EntityResources.Mana.Value);
        }
    }
}
