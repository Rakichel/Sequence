using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInfo
{
    public class PlayerHit : MonoBehaviour
    {
        public float HitAnimTime;   // Hit 애니메이션 출력 시간
        public float DieAnimTime;   // Die 애니메이션 출력 시간
        private Player _player;
        private void Start()
        {
            _player = GetComponent<Player>();
        }
        
        /// <summary>
        /// 공격을 맞을 시 체력에 따라 처리하는 함수입니다.
        /// </summary>
        /// <param name="_damage">적이 공격한 데미지</param>
        public void GetDamage(int _damage)
        {
            _player.Hp = _player.Hp - _damage;
            // 체력이 남아있음
            if (_player.Hp > 0)
            {
                StopAllCoroutines();
                StartCoroutine(Hit());
            }

            // 체력이 다 떨어짐
            else
            {
                StopAllCoroutines();
                _player.State = PlayerState.Die;
            }
        }

        /// <summary>
        /// 공격을 맞을 시 Hit상태로 전환하는 코루틴 입니다.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Hit()
        {
            _player.State = PlayerState.Hit;
            yield return new WaitForSeconds(HitAnimTime);
            _player.State = PlayerState.Idle;
        }
    }
}

