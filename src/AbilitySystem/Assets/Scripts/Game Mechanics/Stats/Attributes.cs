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
    public int Value
    {
        get
        {
            return Mathf.FloorToInt(_value * multiplier);
        }
    }                                       //_value * multiplier

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
    private string _name;
    private float multiplier;
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
        new Attribute[5]
        {
            new Attribute("Strength", 10, 1f),
            new Attribute("Endurance", 0, 1f),
            new Attribute("Agility", 10, 1f),
            new Attribute("Vitality", 0, 1f),
            new Attribute("Recovery", 0, 0.8f)
        }
    );
    public AttributeClass Mind { get; } = new AttributeClass
    (
        "Mind",
        new Attribute[3]
        {
            new Attribute("Intelligence", 10, 1f),
            new Attribute("Wisdom", 0, 0.8f),
            new Attribute("Focus", 0, 0.8f)
        }
    );
    public AttributeClass Magic { get; } = new AttributeClass
    (
        "Magic",
        new Attribute[6]
        {
            new Attribute("Raw", 5, 1f),
            new Attribute("Flame", 5, 1f),
            new Attribute("Water", 5, 1f),
            new Attribute("Light", 5, 0.75f),
            new Attribute("Dark", 5, 0.75f),
            new Attribute("Aptitude", 0, 1f)
        }
    );
}
