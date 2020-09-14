using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public string Name
    {
        get
        {
            return _name;
        }
    }
    public string NameCode
    {
        get
        {
            return _nameCode;
        }
    }
    public int ID
    {
        get
        {
            return _id;
        }
    }
    public Damage.Type[] DamageTypes
    {
        get
        {
            return _damageTypes;
        }
    }
    public Abilities EntityAbilities
    {
        get
        {
            return abilityBook;
        }
    }

    protected virtual void Awake()
    {
        abilityBook = transform.parent.GetComponent<Abilities>();
    }
    public virtual void CreateSpell(string name, string nameCode, int id, Damage.Type[] damageTypes)
    {
        _name = name;
        _nameCode = nameCode;
        _id = id;
        _damageTypes = damageTypes;
    }

    string _name;
    string _nameCode;
    int _id;
    Damage.Type[] _damageTypes;
    Abilities abilityBook;
}
