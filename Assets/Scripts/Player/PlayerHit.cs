using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInfo
{
    public class PlayerHit : MonoBehaviour
    {
        Player _player;
        private void Start()
        {
            _player = GetComponent<Player>();
        }
        public void GetDamage(int _damage)
        {
            _player.Hp = _player.Hp - _damage;
            if (_player.Hp > 0)
            {
                StopAllCoroutines();
                StartCoroutine(Hit());
            }
            else
            {
                StopAllCoroutines();
                _player.state = PlayerState.Die;
            }
        }
        IEnumerator Hit()
        {
            _player.state = PlayerState.Hit;
            yield return new WaitForSeconds(1f);
            _player.state = PlayerState.Idle;
        }
    }
}

