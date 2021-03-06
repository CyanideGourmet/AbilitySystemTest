﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
    public enum Type
    {
        PHYSICAL,
        RAW,
        FLAME,
        WATER,
        LIGHT,
        DARK
    }
    public struct DamagePacket
    {
        public struct Effect
        {
            public System.Type type;
            public float[] parameters;
        }
        public float damageValue;
        public Ability source;
        public List<Effect> effects;
    }
}
