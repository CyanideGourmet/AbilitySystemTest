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
    public bool GlobalCooldown
    {
        get
        {
            return _gcd;
        }
    }
    public string ResourceType
    {
        get
        {
            return _resType;
        }
    }
    public int ResourceCost
    {
        get
        {
            return _resCost;
        }
    }

    protected float _castTime;
    protected bool _gcd = true;
    protected string _resType;
    protected int _resCost;

    public virtual void Use()
    {
        Debug.Log(NameCode + " used by: " + transform.parent.parent.name);
    }
}
