using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillUse : MonoBehaviour
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

    Active skill0;
    Active skill1;
    Active skill2;
    Active skill3;
    Active skill4;

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
        FindSkills();
    }

    void SkillUsed()
    {
        _gcd = globalCooldown;
        StartCoroutine(GlobalCooldownDecay());
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

    public void FindSkills()
    {
        Active[] activeSkills = transform.parent.parent.GetComponentsInChildren<Active>();
        foreach (Active activeSkill in activeSkills)
        {
            if (activeSkill.NameCode == abilityNameCode0)
            {
                skill0 = activeSkill;
            }
            if (activeSkill.NameCode == abilityNameCode1)
            {
                skill1 = activeSkill;
            }
            if (activeSkill.NameCode == abilityNameCode2)
            {
                skill2 = activeSkill;
            }
            if (activeSkill.NameCode == abilityNameCode3)
            {
                skill3 = activeSkill;
            }
            if (activeSkill.NameCode == abilityNameCode4)
            {
                skill4 = activeSkill;
            }
        }
    }

    public void Skill0(InputAction.CallbackContext callbackContext)
    {
        if (GlobalCooldown == 0)
        {
            Debug.Log("Skill0");
            if (rb.velocity == Vector2.zero || (rb.velocity != Vector2.zero && skill0.CastTime == 0f))
            {
                Resources.Resource resource = resourceTypes[skill0.ResourceType];
                if(resource.TryLoseResource(skill0.ResourceCost) <= resource.Value)
                {
                    caster.Cast(skill0);
                    if (skill0.GlobalCooldown) SkillUsed();
                }
                else
                {
                    Debug.LogError("Not enough resource!");
                }
            }
        }
    }
    public void Skill1(InputAction.CallbackContext callbackContext)
    {
        if (GlobalCooldown == 0)
        {
            Debug.Log("skill1");
            if (rb.velocity == Vector2.zero || (rb.velocity != Vector2.zero && skill1.CastTime == 0f))
            {
                Resources.Resource resource = resourceTypes[skill1.ResourceType];
                if (resource.TryLoseResource(skill1.ResourceCost) <= resource.Value)
                {
                    caster.Cast(skill1);
                    if (skill1.GlobalCooldown) SkillUsed();
                }
                else
                {
                    Debug.LogError("Not enough resource!");
                }
            }
        }
    }
    public void Skill2(InputAction.CallbackContext callbackContext)
    {
        if (GlobalCooldown == 0)
        {
            Debug.Log("Skill2");
            skill2.Use();
            SkillUsed();
        }
    }
    public void Skill3(InputAction.CallbackContext callbackContext)
    {
        if (GlobalCooldown == 0)
        {
            Debug.Log("Skill3");
            skill3.Use();
            SkillUsed();
        }
    }
    public void Skill4(InputAction.CallbackContext callbackContext)
    {
        if (GlobalCooldown == 0)
        {
            Debug.Log("Skill4");
            skill4.Use();
            SkillUsed();
        }
    }

    float _gcd;
}
