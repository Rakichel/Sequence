using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class EffectManager : Singleton<EffectManager>
    {
        public GameObject player;
        public GameObject playerEffect;
        public GameObject[] chronosEffect;
        [SerializeField] GameObject dashEffect;
        // Start is called before the first frame update
        void Awake()
        {
            player = GameObject.Find("Player");
            playerEffect = GameObject.Find("EffectPoint");
        }
        // Update is called once per frame
        void Update()
        {
        }

        public void Dash()
        {
            var newDash = Instantiate(dashEffect, playerEffect.transform.position, playerEffect.transform.rotation);
            newDash.transform.SetParent(playerEffect.transform);
            if (player.transform.GetComponent<SpriteRenderer>().flipX)
            {
                newDash.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        public void Chronos(GameObject g)
        {
            var newChronos = Instantiate(chronosEffect[0], g.transform.position, g.transform.rotation);
        }

        public void BossChronos(GameObject g)
        {
            var newChronos = Instantiate(chronosEffect[1], g.transform.position + new Vector3(0, 0.7f, 0), g.transform.rotation);
        }
    }

}

