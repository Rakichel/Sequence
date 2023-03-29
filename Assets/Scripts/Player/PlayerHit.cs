using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInfo
{
    public class PlayerHit : MonoBehaviour
    {
        public float HitAnimTime;   // Hit �ִϸ��̼� ��� �ð�
        public float DieAnimTime;   // Die �ִϸ��̼� ��� �ð�
        private Player _player;
        private void Start()
        {
            _player = GetComponent<Player>();
        }
        
        /// <summary>
        /// ������ ���� �� ü�¿� ���� ó���ϴ� �Լ��Դϴ�.
        /// </summary>
        /// <param name="_damage">���� ������ ������</param>
        public void GetDamage(int _damage)
        {
            _player.Hp = _player.Hp - _damage;
            // ü���� ��������
            if (_player.Hp > 0)
            {
                StopAllCoroutines();
                StartCoroutine(Hit());
            }

            // ü���� �� ������
            else
            {
                StopAllCoroutines();
                _player.State = PlayerState.Die;
            }
        }

        /// <summary>
        /// ������ ���� �� Hit���·� ��ȯ�ϴ� �ڷ�ƾ �Դϴ�.
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

