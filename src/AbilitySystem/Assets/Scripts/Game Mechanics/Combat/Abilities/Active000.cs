using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Active000 : Active
{
    public GameObject prefab;

    GameObject guide;

    private void Awake()
    {
        CreateAbility("Light Guide", "lightguide", 0, null);

        _resCost = 12;
        _castTime = 0f;
        _cooldown = 0.5f;
        _gcd = false;

        guide = Instantiate(prefab, transform);
        guide.SetActive(false);
    }

    protected void Start()
    {
        _resType = resources.Mana.Name;
    }

    public override void Use()
    {
        base.Use();
        if (guide.activeSelf)
        {
            guide.SetActive(false);
            _isToggled = false;
            StartCooldown();
        }
        else if (resources.Mana.LoseResource(ResourceCost))
        {
            guide.GetComponent<Light2D>().pointLightOuterRadius = 5 * (1 + attributes.Magic.FindAttribute("Light").Value * 0.01f);
            guide.SetActive(true);
            _isToggled = true;

            StartCooldown();
            Debug.Log("Light Spell used UWU. " + resources.Mana.Value);
        }
    }
}
