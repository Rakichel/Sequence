using UnityEngine;

namespace BossInfo
{
    [System.Serializable]
    public struct BossStatus
    {
        #region Field
        [SerializeField] private int _hp;
        [SerializeField] private int _speed;
        [SerializeField] private int _jumpPower;
        [SerializeField] private float _gravity;
        [SerializeField] private float _gravityAccel;
        [SerializeField] private BossState _state;
        #endregion

        #region Property
        public int Hp { get { return _hp; } }
        public int Speed { get { return _speed; } }
        public int JumpPower { get { return _jumpPower; } }
        public float Gravity { get { return _gravity; } }
        public float GravityAccel { get { return _gravityAccel; } }
        public BossState State { get { return _state; } }
        #endregion
    }
}

