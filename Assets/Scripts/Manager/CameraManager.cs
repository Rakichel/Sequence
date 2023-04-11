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
        float ChronosI = 0.1f;
        public int ChronosCount = 0;
        bool InputChronos = false;
        public SpriteRenderer ChronosBG;
        public float ChronosBGA = 0;
        private void Awake()
        {
            imp = gameObject.GetComponent<CinemachineImpulseSource>();
            MCam = Camera.main;
            v = MCam.GetComponent<Volume>();
            v.profile.TryGet(out c);
            ChronosBG = MCam.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            Screen.SetResolution(1920, 1080, true);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKey(KeyCode.R))
                ChronosBGA = 0;
            ChronosStart();
            ChronosBG.color = new Color(0, 0, 0, ChronosBGA);
            if (c.intensity.value >= 1 || c.intensity.value == 0)
            {
                ChronosI *= -1f;
            }
                
        }
        public void Impulse()
        {
            imp.GenerateImpulse(Vector3.right);
        }

        private IEnumerator CD()
        {
            yield return new WaitForSecondsRealtime(0.02f);
            CDelay = true;
        }

        private void ChronosStart()
        {
            if(InputChronos)
            {
                if (ChronosCount <= 22)
                {
                    if (CDelay)
                    {
                        CDelay = false;
                        c.intensity.value += ChronosI;
                        StartCoroutine(CD());
                        ChronosCount++;
                        ChronosBGA += 0.015f;
                    }
                }
                else
                {
                    c.intensity.value = 0;
                    ChronosCount = 0;
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
