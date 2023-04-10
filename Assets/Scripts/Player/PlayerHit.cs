using System.Collections;
using UnityEngine;

namespace PlayerInfo
{
    public class PlayerHit : MonoBehaviour
    {
        private Player _player;
        private Coroutine _guarding;

        public float HitAnimTime;   // Hit 애니메이션 출력 시간
        public float DieAnimTime;   // Die 애니메이션 출력 시간

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            // 가드키 입력
            if (Input.GetKey(KeyCode.Z) && _player.PlayerFixedState())
            {
                _player.State = PlayerState.Guard;
            }
            else if (Input.GetKeyUp(KeyCode.Z) && _player.State == PlayerState.Guard)
            {
                _player.State = PlayerState.Idle;
            }

            if (Input.GetKey(KeyCode.H))
            {
                GetDamage(0);
            }
        }

        /// <summary>
        /// 공격을 맞을 시 체력에 따라 처리하는 함수입니다.
        /// </summary>
        /// <param name="_damage">적이 공격한 데미지</param>
        public void GetDamage(int _damage)
        {
            // 공격 받았을 때 가드 중인 경우
            if (_player.State == PlayerState.Guard || _player.State == PlayerState.Guarding)
            {
                if (_guarding != null)
                {
                    StopCoroutine(_guarding);
                }
                _guarding = StartCoroutine(Guarding());
            }
            // 일반적인 피격 로직 실행
            else if (_player.State != PlayerState.Hit && _player.State != PlayerState.Die)
            {
                StopAllCoroutines();
                StartCoroutine(Hit(_damage));
            }
        }

        private IEnumerator Guarding()
        {
            yield return null;
            _player.State = PlayerState.Guarding;
            float _timer = 0f;
            while (_timer <= 0.5f)
            {
                if (_player.State != PlayerState.Guarding)
                    break;
                _timer += Time.fixedUnscaledDeltaTime;
                yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
            }
            if (_player.State == PlayerState.Guarding)
                _player.State = PlayerState.Idle;

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
                yield return new WaitForSeconds(HitAnimTime);
                _player.State = PlayerState.Idle;
            }

            // 체력이 다 떨어짐
            else
            {
                _player.State = PlayerState.Die;
                yield return new WaitForSeconds(HitAnimTime);
            }
        }
    }
}