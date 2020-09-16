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

    private void Start()
    {
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
            skill0.Use();
            SkillUsed();
        }
    }
    public void Skill1(InputAction.CallbackContext callbackContext)
    {
        if (GlobalCooldown == 0)
        {
            Debug.Log("Skill1");
            skill1.Use();
            SkillUsed();
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
