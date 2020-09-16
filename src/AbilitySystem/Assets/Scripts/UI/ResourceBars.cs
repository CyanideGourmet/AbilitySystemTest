using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBars : MonoBehaviour
{
    public Resources playerResources;

    public Text healthText;
    public Text staminaText;
    public Text manaText;

    ResourceBar health;
    ResourceBar stamina;
    ResourceBar mana;

    ResourceBar[] resourceBars = null;

    private void Start()
    {
        resourceBars = new ResourceBar[3];
        StartCoroutine(WaitForResources());
    }

    private void FixedUpdate()
    {
        foreach(ResourceBar resourceBar in resourceBars)
        {
            if(resourceBar != null) resourceBar.UpdateResourceBar();
        }
    }
    IEnumerator WaitForResources()
    {
        while(playerResources.Health == null)
        {
            yield return null;
        }
        health = new ResourceBar(playerResources.Health, healthText);
        stamina = new ResourceBar(playerResources.Stamina, staminaText);
        mana = new ResourceBar(playerResources.Mana, manaText);
        resourceBars[0] = health;
        resourceBars[1] = stamina;
        resourceBars[2] = mana;
        yield return null;
    }

    public class ResourceBar
    {
        public ResourceBar(Resources.Resource resourceObject, Text resourceText)
        {
            resource = resourceObject;
            uiText = resourceText;
        }

        Resources.Resource resource;
        Text uiText;

        public void UpdateResourceBar()
        {
            uiText.text = resource.Name + ": " + resource.Value.ToString() + "/" + resource.MaxValue.ToString();
        }
    }
}
