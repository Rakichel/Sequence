using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageMaker : MonoBehaviour
{
    [SerializeField] private int BlockCount;
    [SerializeField] private Transform StartPos;
    [SerializeField] private Transform LastPos;
    [SerializeField] private float DistanceMin;
    [SerializeField] private float DistanceMax;
    public float[] BlockFloor;
    public GameObject[] Block;
    public GameObject[] CurrentBlock;
    private Transform Player;
    public UnityEvent StageMakeEvent;
    // Start is called before the first frame update
    void Start()
    {
        CurrentBlock = new GameObject[BlockCount];
        BlockFloor = new float[BlockCount];
        LastPos = StartPos;
        StageSpawn();
        Player = GameObject.Find("StageTest").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.position.x >= CurrentBlock[BlockCount / 2].transform.position.x)
        {
            StageSpawn();
        }
    }

    public void StageSpawn()
    {
        for (int i = 0; i < BlockCount; i++)
        {
            var newBlock = Instantiate(Block[Random.Range(0, Block.Length)], LastPos.position + new Vector3(Random.Range(DistanceMin, DistanceMax + 1), 0, 0), gameObject.transform.rotation);
            LastPos = newBlock.transform;
            CurrentBlock[i] = newBlock;
            switch(newBlock.name)
            {
                case "Floor1":
                    BlockFloor[i] = 1;
                    break;
                case "Floor2":
                    BlockFloor[i] = 2;
                    break;
                case "Florr3":
                    BlockFloor[i] = 3;
                    break;
                default:
                    BlockFloor[i] = 1;
                    break;
            }
        }
        StageMakeEvent.Invoke();
    }
}
