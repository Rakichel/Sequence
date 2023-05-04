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
    [SerializeField] private GameObject Ground;
    private Transform Player;
    public int[] BlockFloor;
    public GameObject[] Block;
    public GameObject[] CurrentBlock;
    public UnityEvent StageMakeEvent;
    // Start is called before the first frame update
    void Start()
    {
        CurrentBlock = new GameObject[BlockCount];
        BlockFloor = new int[BlockCount];
        LastPos = StartPos;
        StageSpawn();
        Player = GameObject.Find("Player").transform;
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
            newBlock.transform.SetParent(gameObject.transform);
            CurrentBlock[i] = newBlock;
            switch(newBlock.tag)
            {
                case "Floor1":
                    BlockFloor[i] = 2;
                    break;
                case "Floor2":
                    BlockFloor[i] = 3;
                    break;
                case "Floor3":
                    BlockFloor[i] = 4;
                    break;
            }
        }
        Instantiate(Ground, new Vector3(CurrentBlock[BlockCount / 2 - 1].transform.position.x, -8.67f, 0), gameObject.transform.rotation);
        StageMakeEvent.Invoke();
    }
}
