using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive : Ability
{
    protected virtual void Awake()
    {
        Debug.Log(NameCode + " Passive was activated.");
    }
    private void OnDestroy()
    {
        Debug.Log(NameCode + " Passive was removed.");
    }
}
