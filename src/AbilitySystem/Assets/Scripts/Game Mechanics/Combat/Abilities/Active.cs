using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Active : Ability
{
    public virtual void Use()
    {
        Debug.Log(NameCode + " used by: " + transform.parent.parent.parent.name);
    }
}
