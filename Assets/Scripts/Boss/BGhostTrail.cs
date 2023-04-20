using System.Collections.Generic;
using UnityEngine;

namespace BossInfo
{
    public class BGhostTrail : MonoBehaviour
    {
        private BossController _controller;
        private SpriteRenderer _spriteRenderer;     // ���� ������ ghost�� SpriteRenderer�� �����ϱ� ���� ����
        private float _timer = 0;                   // �ֱ������� �����ϱ� ���� Ÿ�̸�
        private Stack<GameObject> _ghostPool = new Stack<GameObject>();
        private bool isOne = false;

        public GameObject GhostGroup;
        public GameObject GhostPrefab;          // ������ Prefab
        public float Delay;                     // �����ϴµ� �ɸ��� �ð�
        public Color StartColor;
        public Color EndColor;
        public float Timer;

        private void Start()
        {
            _controller = GetComponent<BossController>();
            if (GhostGroup == null)
                GhostGroup = new GameObject("BGhostGroup");
            GhostInit();
        }

        private void Update()
        {
            if (Time.timeScale < 1f && _controller.IsUnscaled)
            {
                isOne = true;
                if (_timer > 0)
                {
                    _timer -= Time.unscaledDeltaTime;
                }
                else
                {
                    if (_controller.State != BossState.Idle)
                    {
                        Timer += Delay;
                        _timer = Delay;
                        DrawGhost();
                    }
                }
            }
            else if (isOne)
            {
                for (int i = 0; i < GhostGroup.transform.childCount; i++)
                {
                    GhostGroup.transform.GetChild(i).gameObject.SetActive(false);
                    _ghostPool.Push(GhostGroup.transform.GetChild(i).gameObject);
                }
                isOne = false;
            }
        }

        private void GhostInit()
        {
            for (int i = 0; i < 5 / Delay; i++)
                _ghostPool.Push(CreateGhost());
        }

        /// <summary>
        /// �ܻ��� �������ִ� �Լ��Դϴ�.
        /// </summary>
        /// 
        private GameObject CreateGhost()
        {
            // ����, ũ������
            GameObject ghostObj = Instantiate(GhostPrefab, Vector2.zero, Quaternion.identity, GhostGroup.transform);
            ghostObj.transform.localScale = _controller.transform.localScale;
            ghostObj.SetActive(false);
            return ghostObj;
        }
        private void DrawGhost()
        {
            if (_ghostPool.Count > 0)
            {
                GameObject g = _ghostPool.Pop();
                g.transform.position = transform.position;
                g.transform.rotation = transform.rotation;

                _spriteRenderer = g.GetComponent<SpriteRenderer>();
                _spriteRenderer.sprite = _controller.GetComponent<SpriteRenderer>().sprite;
                _spriteRenderer.flipX = _controller.GetComponent<SpriteRenderer>().flipX;
                Color newColor = new Color(
                    Mathf.Lerp(StartColor.r, EndColor.r, Mathf.Abs(Mathf.Sin(Timer))),
                    Mathf.Lerp(StartColor.g, EndColor.g, Mathf.Abs(Mathf.Sin(Timer))),
                    Mathf.Lerp(StartColor.b, EndColor.b, Mathf.Abs(Mathf.Sin(Timer))),
                    0.5f);
                _spriteRenderer.color = newColor;
                g.SetActive(true);
            }
            else
            {
                _ghostPool.Push(CreateGhost());
                DrawGhost();
            }
        }
    }
}