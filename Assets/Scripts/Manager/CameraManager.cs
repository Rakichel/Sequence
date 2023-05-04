using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Manager
{
    public class CameraManager : Singleton<CameraManager>
    {
        // Start is called before the first frame update
        CinemachineImpulseSource imp = new CinemachineImpulseSource();
        Camera MCam;
        private ChromaticAberration c;
        private Volume v;
        bool CDelay = true;
        bool ChronosAble = true;
        bool InputChronos = false;
        public float ChronosBGA = 0;
        [SerializeField] private float ChronosI = 0.1f;
        [SerializeField] private SpriteRenderer ChronosBG;
        private void Awake()
        {
            imp = gameObject.GetComponent<CinemachineImpulseSource>();
            MCam = Camera.main;
            v = MCam.GetComponent<Volume>();
            v.profile.TryGet(out c);
            ChronosBG = GameObject.Find("Chronos").GetComponent<SpriteRenderer>();
            Screen.SetResolution(1920, 1080, true);
            Debug.Log(CDelay);
        }

        // Update is called once per frame
        private void Update()
        {
            ChronosStart();
            ChronosBG.color = new Color(0, 0, 0, ChronosBGA);
        }
        public void Impulse()
        {
            imp.GenerateImpulse(Vector3.right);
        }

        private IEnumerator CD()
        {
            yield return new WaitForSecondsRealtime(0.025f);
            CDelay = true;
        }

        private void ChronosStart()
        {
            if(InputChronos)
            {
                if (c.intensity.value == 1)
                {
                    ChronosI *= -1f;
                }
                else if (c.intensity.value == 0)
                {
                    ChronosI = Mathf.Abs(ChronosI);
                }
                if (ChronosAble)
                {
                    if (CDelay)
                    {
                        CDelay = false;
                        c.intensity.value += ChronosI;
                        StartCoroutine(CD());
                        ChronosBGA += 0.02f;
                        Debug.Log(c.intensity.value);
                        if (c.intensity.value == 0)
                        {
                            ChronosAble = false;
                        }
                    }
                    
                }
                else
                {
                    ChronosAble = true;
                    InputChronos = false;
                }
            }
        }

        public void Chronos()
        {
            InputChronos = true;
        }
    }
}
