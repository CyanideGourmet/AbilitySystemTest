using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute
{
    public Attribute(string nam, int val, float mult)
    {
        _name = nam;
        _value = val;
        multiplier = mult;
    }
    public string Name
    {
        get
        {
            return _name;
        }
    }
    public float multiplier;
    public int Value
    {
        get
        {
            return Mathf.FloorToInt(_value * multiplier);
        }
    }
    public string _name;

    public void Add(int add)
    {
        if(add <= 0)
        {
            throw new System.Exception("Incorrect value to add!");
        }
        _value += add;
    }
    public void Subtract(int sub)
    {
        if (sub > 0)
        {
            throw new System.Exception("Incorrect value to subtract!");
        }
        _value -= sub;
    }

    private int _value;


}
public class AttributeClass
{
    public string Name
    {
        get
        {
            return _name;
        }
    }
    public AttributeClass(string name, Attribute[] attributes)
    {
        _name = name;
        _attributes = attributes;
    }
    public Attribute FindAttribute(string name)
    {
        Attribute returnVal = null;
        for(int i = 0; i < Attributes.Length; i++)
        {
            if(Attributes[i].Name == name)
            {
                returnVal = Attributes[i];
                break;
            }
        }
        return returnVal;
    }

    public Attribute[] Attributes
    {
        get
        {
            return _attributes;
        }
    }

    string _name;
    Attribute[] _attributes;
}
public class Attributes : MonoBehaviour
{

    public AttributeClass Body { get; } = new AttributeClass
    (
        "Body",
        new Attribute[2]
        {
            new Attribute("Strength", 10, 1f),
            new Attribute("Agility", 10, 1f)
        }
    );
    public AttributeClass Mind { get; } = new AttributeClass
    (
        "Mind",
        new Attribute[2]
        {
            new Attribute("Intelligence", 10, 1f),
            new Attribute("Wisdom", 0, 1f)
        }
    );
    public AttributeClass Magic { get; } = new AttributeClass
    (
        "Magic",
        new Attribute[3]
        {
            new Attribute("Flame", 5, 1f),
            new Attribute("Water", 5, 1f),
            new Attribute("Light", 5, 0.75f)
        }
    );
}
