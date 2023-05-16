using Manager;
using System.Collections;
using UnityEngine;

namespace PlayerInfo
{
    public class PlayerHit : MonoBehaviour
    {
        private Player _player;
        private Coroutine _guarding;
        private SpriteRenderer _spriteRenderer;
        private Color _myColor;
        private Color _lerpColor;
        private float _colorTimer = 1f;

        public float HitAnimTime;   // Hit �ִϸ��̼� ��� �ð�
        public float DieAnimTime;   // Die �ִϸ��̼� ��� �ð�  

        private void Start()
        {
            _player = GetComponent<Player>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _myColor = _spriteRenderer.color;
            _lerpColor = _myColor;
        }

        private void Update()
        {
            if (Time.timeScale == 0.1f)
                return;
            // ����Ű �Է�
            if (Input.GetKey(KeyCode.Z) && _player.PlayerFixedState())
            {
                _player.State = PlayerState.Guard;
            }
            else if (Input.GetKeyUp(KeyCode.Z) && _player.State == PlayerState.Guard)
            {
                _player.State = PlayerState.Idle;
            }
            if (_colorTimer <= 1f)
            {
                _colorTimer += 3f * Time.unscaledDeltaTime;
                _spriteRenderer.color = Color.Lerp(_lerpColor, _myColor, _colorTimer);
            }


            if (Input.GetKey(KeyCode.H))
            {
                GetDamage(100);
            }
        }

        /// <summary>
        /// ������ ���� �� ü�¿� ���� ó���ϴ� �Լ��Դϴ�.
        /// </summary>
        /// <param name="_damage">���� ������ ������</param>
        public void GetDamage(int _damage)
        {
            // ���� �޾��� �� ���� ���� ���
            if (_player.State == PlayerState.Guard || _player.State == PlayerState.Guarding)
            {
                if (_guarding != null)
                {
                    StopCoroutine(_guarding);
                }
                _guarding = StartCoroutine(Guarding());
                _lerpColor = Color.cyan;
                _colorTimer = 0f;
            }
            // �Ϲ����� �ǰ� ���� ����
            else if (_player.State != PlayerState.Hit && _player.State != PlayerState.Die)
            {
                StopAllCoroutines();
                StartCoroutine(Hit(_damage));
                _lerpColor = Color.red;
                _colorTimer = 0f;
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
        /// ������ ���� �� Hit���·� ��ȯ�ϴ� �ڷ�ƾ �Դϴ�.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Hit(int _damage)
        {
            _player.Hp -= _damage;

            // ü���� �� ������
            if (_player.Hp <= 0)
            {
                _player.State = PlayerState.Die;
                yield return new WaitForSecondsRealtime(HitAnimTime);
                StageManager.Instance.GotoTitle();
            }
        }
    }
}