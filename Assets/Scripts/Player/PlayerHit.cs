using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInfo
{
    public class PlayerHit : MonoBehaviour
    {
        private Player _player;
        private float _timer;

        public float HitAnimTime;   // Hit 애니메이션 출력 시간
        public float DieAnimTime;   // Die 애니메이션 출력 시간
        public float Defence;       // 가드율 0 ~ 1
        public float ParryTime;     // 패링 판정 시간

        private void Start()
        {
            _player = GetComponent<Player>();
            _timer = ParryTime;
        }

        private void Update()
        {
            // 가드키 입력
            if (Input.GetKey(KeyCode.LeftAlt) && _player.PlayerFixedState())
            {
                _player.State = PlayerState.Guard;
                ParryTime -= Time.deltaTime;
            }
            else if(Input.GetKeyUp(KeyCode.LeftAlt) && _player.State == PlayerState.Guard)
            {
                _player.State = PlayerState.Idle;
            }

            SetTimer();
        }

        private void SetTimer()
        {
            if (_player.State == PlayerState.Guard)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _timer = ParryTime;
            }
        }

        /// <summary>
        /// 공격을 맞을 시 체력에 따라 처리하는 함수입니다.
        /// </summary>
        /// <param name="_damage">적이 공격한 데미지</param>
        public void GetDamage(int _damage)
        {
            // 공격 받았을 때 가드 중인 경우
            if(_player.State == PlayerState.Guard)
            {
                if(_timer > 0)
                {
                    // Parry 구현
                }
                else
                {
                    _player.Hp -= _damage * Defence;
                }
            }
            // 일반적인 피격 로직 실행
            else
            {
                StopAllCoroutines();
                StartCoroutine(Hit(_damage));
            }
        }

        /// <summary>
        /// 공격을 맞을 시 Hit상태로 전환하는 코루틴 입니다.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Hit(int _damage)
        {
            _player.Hp -= _damage;
            // 체력이 남아있음
            if (_player.Hp > 0)
            {
                _player.State = PlayerState.Hit;
            }

            // 체력이 다 떨어짐
            else
            {
                _player.State = PlayerState.Die;
            }
            yield return new WaitForSeconds(HitAnimTime);
            _player.State = PlayerState.Idle;
        }
    }
}