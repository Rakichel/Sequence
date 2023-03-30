using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInfo
{
    public class PlayerHit : MonoBehaviour
    {
        private Player _player;
        private float _timer;

        public float HitAnimTime;   // Hit �ִϸ��̼� ��� �ð�
        public float DieAnimTime;   // Die �ִϸ��̼� ��� �ð�
        public float Defence;       // ������ 0 ~ 1
        public float ParryTime;     // �и� ���� �ð�

        private void Start()
        {
            _player = GetComponent<Player>();
            _timer = ParryTime;
        }

        private void Update()
        {
            // ����Ű �Է�
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
        /// ������ ���� �� ü�¿� ���� ó���ϴ� �Լ��Դϴ�.
        /// </summary>
        /// <param name="_damage">���� ������ ������</param>
        public void GetDamage(int _damage)
        {
            // ���� �޾��� �� ���� ���� ���
            if(_player.State == PlayerState.Guard)
            {
                if(_timer > 0)
                {
                    // Parry ����
                }
                else
                {
                    _player.Hp -= _damage * Defence;
                }
            }
            // �Ϲ����� �ǰ� ���� ����
            else
            {
                StopAllCoroutines();
                StartCoroutine(Hit(_damage));
            }
        }

        /// <summary>
        /// ������ ���� �� Hit���·� ��ȯ�ϴ� �ڷ�ƾ �Դϴ�.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Hit(int _damage)
        {
            _player.Hp -= _damage;
            // ü���� ��������
            if (_player.Hp > 0)
            {
                _player.State = PlayerState.Hit;
            }

            // ü���� �� ������
            else
            {
                _player.State = PlayerState.Die;
            }
            yield return new WaitForSeconds(HitAnimTime);
            _player.State = PlayerState.Idle;
        }
    }
}