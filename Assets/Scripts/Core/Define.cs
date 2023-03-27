using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Types
{
    public enum CreatureType { None = 0, Player, Enemy }
    public enum PlayerState { Idle = 0, Walk, Jump, Attack, Hit, Die }
    public enum EnemyState { Idle = 0, }
}
