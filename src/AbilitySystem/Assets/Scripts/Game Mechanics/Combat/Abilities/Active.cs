using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Active : Ability
{
    public float CastTime
    {
        get
        {
            return _castTime;
        }
    }
    public float Cooldown
    {
        get
        {
            return _cooldown;
        }
    }
    public bool IsOnCooldown
    {
        get
        {
            return _isOnCd;
        }
    }
    public bool GlobalCooldown
    {
        get
        {
            return _gcd;
        }
    }
    public bool IsToggled
    {
        get
        {
            return _isToggled;
        }
    }
    public string ResourceType
    {
        get
        {
            return _resType;
        }
    }
    public float ResourceCost
    {
        get
        {
            return _resCost;
        }
    }

    protected float _castTime;
    protected float _cooldown = 0f;
    protected bool _isOnCd = false;
    protected bool _gcd = true;
    protected bool _isToggled = false;
    protected string _resType;
    protected float _resCost;

    public virtual void Use()
    {
        Debug.Log(NameCode + " used by: " + transform.parent.parent.name);
    }
    protected virtual void StartCooldown()
    {
        _isOnCd = true;
        StartCoroutine(CooldownDecay(Cooldown));
    }
    private IEnumerator CooldownDecay(float time)
    {
        while ((time -= Time.deltaTime) > 0f) yield return null;
        _isOnCd = false;
        yield return null;
    }
}
