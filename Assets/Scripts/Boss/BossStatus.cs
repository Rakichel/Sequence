using System;
using UnityEngine;

namespace BossInfo
{
    [Serializable]
    public class BossStatus
    {
        #region Field
        [field: SerializeField] public int Hp { get; set; }
        [field: SerializeField] public int Speed { get; private set; }
        [field: SerializeField] public int Power { get; private set; }
        [field: SerializeField] public int Posture { get; set; }
        [field: SerializeField] public int JumpPower { get; private set; }
        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float GravityAccel { get; private set; }
        #endregion

        public BossStatus()
        {
            Hp = 600;
            Speed = 8;
            Power = 7;
            Posture = 5;
            JumpPower = 20;
            Gravity = 5f;
            GravityAccel = -9.8f;
        }
    }
}