using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public class Resource
    {
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
                return _value;
            }
        }
        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
        }

        Dictionary<Attribute, float> ValueModAttributes;
        Dictionary<Attribute, float> GainModAttributes;
        Dictionary<Attribute, float> LoseModAttributes;

        public Resource(string _nam, Dictionary<Attribute, float> valueMod, Dictionary<Attribute, float> gainMod, Dictionary<Attribute, float> loseMod)
        {
            _name = _nam;
            _maxValue = 100;
            _value = 100;
            ValueModAttributes = valueMod;
            GainModAttributes = gainMod;
            LoseModAttributes = loseMod;
            this.UpdateValues();
        }
        public void UpdateValues()
        {
            UpdateValueModifier();
            UpdateGainModifier();
            UpdateLoseModifier();
        }
        public void UpdateValueModifier()
        {
            float newModifier = 1;                                                                  //New Resource modifier this tick
            foreach (KeyValuePair<Attribute, float> ValueModAttribute in ValueModAttributes)        //Get all the attribute values that influence this Resource's value
                newModifier += (ValueModAttribute.Key.Value * ValueModAttribute.Value);
            int newMaxValue = Mathf.FloorToInt(100 * newModifier);                                  //Calculate nex MaxValue
            if (newMaxValue != MaxValue)                                                            //If the new modifier (and MaxValue) is different from the current update the values
            {
                float resourcePercentage = Value * 1f / MaxValue;                                        //Save current resource percentage
                _maxValue = newMaxValue;
                _value = Mathf.FloorToInt(MaxValue * resourcePercentage);
            }
        }
        public void UpdateGainModifier()
        {
            float newGainModifier = 1;
            foreach (KeyValuePair<Attribute, float> GainModAttribute in GainModAttributes)
                newGainModifier += (GainModAttribute.Key.Value * GainModAttribute.Value);
            gainModifier = newGainModifier;
        }
        public void UpdateLoseModifier()
        {
            float newLoseModifier = 0;
            foreach (KeyValuePair<Attribute, float> LoseModAttribute in LoseModAttributes)
                newLoseModifier += (LoseModAttribute.Key.Value * LoseModAttribute.Value);
            newLoseModifier = 1 - newLoseModifier;
            loseModifier = newLoseModifier;
        }


        public int TryGainResource(int _val)
        {
            return Mathf.FloorToInt(_val * gainModifier);
        }
        public int TryLoseResource(int _val)
        {
            return Mathf.FloorToInt(_val * loseModifier);
        }
        public void GainResource(int _val)
        {
            if (_val < 0)
            {
                throw new System.Exception("Incorrect gain value");
            }
            _value = Mathf.FloorToInt(Mathf.Clamp((_value + _val * gainModifier), 0, _maxValue));
        }
        public bool LoseResource(int _val, bool loseAnyway = false)
        {
            if (_val < 0)
            {
                throw new System.Exception("Incorrect lose value");
            }
            float valueToLose = _val * loseModifier;
            if(valueToLose > Value)
            {
                if (loseAnyway) _value = 0;
                return false;
            }
            else
            {
                _value = Mathf.FloorToInt(Mathf.Clamp((_value - valueToLose), 0, _maxValue));
                return true;
            }
        }
        public void GainResourcePercentage(float _per)
        {
            if (_per <= 0)
            {
                throw new System.Exception("Incorrect gain percentage value");
            }
            _value = Mathf.FloorToInt(Mathf.Clamp(_value * (1 + _per * gainModifier), 0, _maxValue));
        }
        public void LoseResourcePercentage(float _per)
        {
            if (_per <= 0)
            {
                throw new System.Exception("Incorrect lose percentage value");
            }
            _value = Mathf.FloorToInt(Mathf.Clamp(_value * (1 - _per * loseModifier), 0, _maxValue));
        }

        string _name;
        int _value;
        int _maxValue;
        float gainModifier;     //Gain and loss modifiers affect gained and lost resource by a percentage per point in an attribute
        float loseModifier;
    }

    Attributes attributes;

    public Resource Health
    {
        get
        {
            return _health;
        }
    }
    public Resource Stamina
    {
        get
        {
            return _stamina;
        }
    }
    public Resource Mana
    {
        get
        {
            return _mana;
        }
    }

    private void Awake()
    {
        attributes = GetComponent<Attributes>();
    }
    private void Start()
    {
        Dictionary<Attribute, float> valueModifier;
        Dictionary<Attribute, float> gainModifier;
        Dictionary<Attribute, float> loseModifier;

        valueModifier = new Dictionary<Attribute, float> {{ attributes.Body.FindAttribute("Vitality"), 0.005f }};
        gainModifier = new Dictionary<Attribute, float>{{ attributes.Body.FindAttribute("Recovery"), 0.0001f }};
        loseModifier = new Dictionary<Attribute, float>{{ attributes.Body.FindAttribute("Endurance"), 0.0001f }};
        _health = new Resource("Health", valueModifier, gainModifier, loseModifier);

        valueModifier = new Dictionary<Attribute, float> { { attributes.Body.FindAttribute("Endurance"), 0.005f } };
        gainModifier = new Dictionary<Attribute, float> { { attributes.Body.FindAttribute("Recovery"), 0.00002f }, { attributes.Mind.FindAttribute("Focus"), 0.00005f } };
        loseModifier = new Dictionary<Attribute, float> { { attributes.Body.FindAttribute("Strength"), 0.0001f } };
        _stamina = new Resource("Stamina", valueModifier, gainModifier, loseModifier);

        valueModifier = new Dictionary<Attribute, float> { { attributes.Magic.FindAttribute("Aptitude"), 0.005f } };
        gainModifier = new Dictionary<Attribute, float> { { attributes.Mind.FindAttribute("Wisdom"), 0.0001f } };
        loseModifier = new Dictionary<Attribute, float> { { attributes.Mind.FindAttribute("Intelligence"), 0.00005f }, { attributes.Mind.FindAttribute("Focus"), 0.00005f } };
        _mana = new Resource("Mana", valueModifier, gainModifier, loseModifier);
    }

    Resource _health;
    Resource _stamina;
    Resource _mana;
}
