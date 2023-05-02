using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class EffectManager : Singleton<EffectManager>
    {
        public GameObject player;
        public GameObject playerEffect;
        public GameObject effect;
        [SerializeField] GameObject DashEffect;
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
            var newDash = Instantiate(DashEffect, playerEffect.transform.position, playerEffect.transform.rotation);
            newDash.transform.SetParent(playerEffect.transform);
            if (player.transform.GetComponent<SpriteRenderer>().flipX)
            {
                newDash.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}

