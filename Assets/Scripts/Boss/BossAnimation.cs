using UnityEngine;

namespace BossInfo
{
    public class BossAnimation : MonoBehaviour
    {
        private BossController _controller;
        private Animator _animator;
        private BossState _state;

        void Start()
        {
            _controller = GetComponent<BossController>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (_state == _controller.State)
                return;

            if (_controller.State != BossState.Idle)
                _animator.SetBool(_controller.State.ToString(), true);

            if (_state != BossState.Idle)
                _animator.SetBool(_state.ToString(), false);

            _state = _controller.State;
        }
    }
}
