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
    [SerializeField] int EnemyId;
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
            if (Manager.GameManager.Instance.EnemyLevel >= 3)
            {
                EnemyId = Random.Range(0, 3);
            }
            else
            {
                EnemyId = Random.Range(0, 2);
            }
            int floor = Random.Range(1, Stage.BlockFloor[i] + 1);
            float EnemySpawn = -3f;
            switch (floor)
            {
                case 1:
                    EnemySpawn = -3f;
                    break;
                case 2:
                    EnemySpawn = 0f;
                    break;
                case 3:
                    EnemySpawn = 6f;
                    break;
                case 4:
                    EnemySpawn = 6f;
                    break;
            }
            Instantiate(EnemyArr[EnemyId], new Vector3((Stage.CurrentBlock[i].transform.position.x + Random.Range(-3, 4)), EnemySpawn, 0), Stage.CurrentBlock[i].transform.rotation);
        }
    }   
}
