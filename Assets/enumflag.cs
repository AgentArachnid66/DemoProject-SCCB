using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
// Powers of two
public enum AttackType
{
    // Decimal     // Binary
    None = 0,    // 000000
    Melee = 1,    // 000001
    Fire = 2,    // 000010
    Ice = 4,    // 000100
    Poison = 8     // 001000
}
// ...
public class enumflag : MonoBehaviour
{
    public AttackType attackType = AttackType.Melee | AttackType.Fire;

}
