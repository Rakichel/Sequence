using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tuto : MonoBehaviour
{
    public GameObject Move;
    public GameObject Jump;
    public GameObject Landing;
    public GameObject ADG;
    public GameObject GuardAttack;
    public GameObject Chronos;
    public GameObject Pause;
    public GameObject KillEnemy;
    // Start is called before the first frame update

    private void Update()
    {
        if(Manager.GameManager.Instance.KillCount == 5)
        {
            SceneManager.LoadScene("Japanese landscape");
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log(c.name);
        switch(c.name)
        {
            case "Move":
                Move.SetActive(true);
                Jump.SetActive(false);
                Landing.SetActive(false);
                ADG.SetActive(false);
                GuardAttack.SetActive(false);
                Chronos.SetActive(false);
                Pause.SetActive(false);
                KillEnemy.SetActive(false);
                break;
            case "Jump":
                Move.SetActive(false);
                Jump.SetActive(true);
                Landing.SetActive(false);
                break;
            case "Landing":
                Jump.SetActive(false);
                Landing.SetActive(true);
                ADG.SetActive(false);
                break;
            case "ADG":
                Landing.SetActive(false);
                ADG.SetActive(true);
                GuardAttack.SetActive(false);
                break;
            case "GuardAttack":
                ADG.SetActive(false);
                GuardAttack.SetActive(true);
                Chronos.SetActive(false);
                break;
            case "ChronosT":
                GuardAttack.SetActive(false);
                Chronos.SetActive(true);
                Pause.SetActive(false);
                break;
            case "Pause":
                Chronos.SetActive(false);
                Pause.SetActive(true);
                KillEnemy.SetActive(false);
                break;
            case "KillEnemy":
                Pause.SetActive(false);
                KillEnemy.SetActive(true);
                break;
        }
    }
}
