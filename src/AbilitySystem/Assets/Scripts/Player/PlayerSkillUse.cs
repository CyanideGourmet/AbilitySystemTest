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

    public Active skill0;
    public Active skill1;
    public Active skill2;
    public Active skill3;
    public Active skill4;

    void SkillUsed()
    {
        _gcd = globalCooldown;
        StartCoroutine(GlobalCooldownDecay());
    }
    IEnumerator GlobalCooldownDecay()
    {
        while(GlobalCooldown > 0)
        {
            _gcd -= Time.deltaTime;
            yield return null;
        }
        _gcd = 0f;
        yield return null;
    }

    public void Skill0(InputAction.CallbackContext callbackContext)
    {
        if(GlobalCooldown == 0)
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
            SkillUsed();
        }
    }
    public void Skill2(InputAction.CallbackContext callbackContext)
    {
        if (GlobalCooldown == 0)
        {
            Debug.Log("Skill2");
            SkillUsed();
        }
    }
    public void Skill3(InputAction.CallbackContext callbackContext)
    {
        if (GlobalCooldown == 0)
        {
            Debug.Log("Skill3");
            SkillUsed();
        }
    }
    public void Skill4(InputAction.CallbackContext callbackContext)
    {
        if (GlobalCooldown == 0)
        {
            Debug.Log("Skill4");
            SkillUsed();
        }
    }

    float _gcd;
}
