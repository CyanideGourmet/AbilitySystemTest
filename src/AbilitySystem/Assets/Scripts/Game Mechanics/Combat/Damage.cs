using System.Collections;
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
        LIGHT
    }
    public struct DamagePacket
    {
        public float damageValue;
        public Ability source;
        public List<System.Type> effects;
    }
}
