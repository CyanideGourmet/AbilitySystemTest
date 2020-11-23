using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityUse : MonoBehaviour
{
    public float GlobalCooldown
    {
        get
        {
            return _gcd;
        }
    }
    public float globalCooldown = 0.5f;

    public string abilityNameCode0;
    public string abilityNameCode1;
    public string abilityNameCode2;
    public string abilityNameCode3;
    public string abilityNameCode4;

    Active[] abilities = new Active[5];

    Caster caster;
    Rigidbody2D rb;
    Resources resources;
    Dictionary<string, Resources.Resource> resourceTypes = new Dictionary<string, Resources.Resource>();


    private void Awake()
    {
        caster = GetComponent<Caster>();
        rb = GetComponentInParent<Rigidbody2D>();
        resources = transform.parent.parent.GetComponentInChildren<Resources>();
    }
    private void Start()
    {
        resourceTypes.Add(resources.Mana.Name, resources.Mana);
        resourceTypes.Add(resources.Stamina.Name, resources.Stamina);
        resourceTypes.Add(resources.Health.Name, resources.Health);
        FindAbilities();
    }

    void AbilityUsed()
    {
        _gcd = globalCooldown;
        StartCoroutine(GlobalCooldownDecay());
    }
    void UseAbility(int abilityNr)
    {
        bool offGCD = GlobalCooldown == 0;
        bool offCD = !abilities[abilityNr].IsOnCooldown;
        bool notMovingOrNoGCD = rb.velocity == Vector2.zero || (rb.velocity != Vector2.zero && abilities[abilityNr].CastTime == 0f);
        bool abilityAssigned = abilities[abilityNr] != null;
        if (offGCD && notMovingOrNoGCD && abilityAssigned && offCD)
        {
            Debug.Log("Ability" + abilityNr.ToString());
            Resources.Resource resource = resourceTypes[abilities[abilityNr].ResourceType];
            if (resource.TryLoseResource(abilities[abilityNr].ResourceCost) <= resource.Value || abilities[abilityNr].IsToggled)
            {
                caster.Cast(abilities[abilityNr]);
                if (abilities[abilityNr].GlobalCooldown && !abilities[abilityNr].IsToggled) AbilityUsed();
            }
            else Debug.LogError("Not enough resource!");
        }
    }

    public void Ability0(InputAction.CallbackContext callbackContext) => UseAbility(0);
    public void Ability1(InputAction.CallbackContext callbackContext) => UseAbility(1);
    public void Ability2(InputAction.CallbackContext callbackContext) => UseAbility(2);
    public void Ability3(InputAction.CallbackContext callbackContext) => UseAbility(3);
    public void Ability4(InputAction.CallbackContext callbackContext) => UseAbility(4);

    public void FindAbilities()
    {
        Active[] activeAbilitys = transform.parent.parent.GetComponentsInChildren<Active>();
        foreach (Active activeAbility in activeAbilitys)
        {
            if (activeAbility.NameCode == abilityNameCode0) abilities[0] = activeAbility;
            if (activeAbility.NameCode == abilityNameCode1) abilities[1] = activeAbility;
            if (activeAbility.NameCode == abilityNameCode2) abilities[2] = activeAbility;
            if (activeAbility.NameCode == abilityNameCode3) abilities[3] = activeAbility;
            if (activeAbility.NameCode == abilityNameCode4) abilities[4] = activeAbility;
        }
    }
    IEnumerator GlobalCooldownDecay()
    {
        while (GlobalCooldown > 0)
        {
            _gcd -= Time.deltaTime;
            yield return null;
        }
        _gcd = 0f;
        yield return null;
    }
    float _gcd;
}
