using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public Attributes EntityAttributes
    {
        get
        {
            return attributes;
        }
    }
    public Resources EntityResources
    {
        get
        {
            return resources;
        }
    }

    Attributes attributes;
    Resources resources;

    private void Awake()
    {
        attributes = GetComponent<Attributes>();
        resources = GetComponent<Resources>();
    }
}
