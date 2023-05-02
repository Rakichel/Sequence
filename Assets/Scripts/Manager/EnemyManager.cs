using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int EnemyLevel;
    [SerializeField] private int EnemyLvUp;
    [SerializeField] private GameObject[] EnemyArr;
    [SerializeField] private StageMaker Stage;
    public UnityEvent StageMakeEvent;

    // Start is called before the first frame update
    void Awake()
    {
        Stage = gameObject.GetComponent<StageMaker>();
    }

    // Update is called once per frame

    public void EnemySpawn()
    {
        for (int i = 0; i < Stage.CurrentBlock.Length; i++)
        {
            var newEnemy = Instantiate(EnemyArr[0], new Vector3((Stage.CurrentBlock[i].transform.localPosition.x + Random.Range(-4, 5)), Stage.BlockFloor[i], 0), Stage.CurrentBlock[i].transform.rotation);
        }
    }   
}
