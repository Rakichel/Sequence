using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string _name;
    public float _time;
    public int _resurrect;
    public User(string _name, float _time, int _resurrect)
    {
        this._name = _name;
        this._time = _time;
        this._resurrect = _resurrect;
    }
}

[Serializable]
public class UserData
{
    public List<User> users = new List<User>();
}

public class BoardManager : MonoBehaviour
{
    int n = 0;

    void Update()
    {
        // 저장 시기는 보스 처치 후 이름 입력 시
        if (Input.GetKeyDown("s"))
        {
            DataInput();
        }

        // 로드 시기는 리더보드 보여줄 때
        if (Input.GetKeyDown("l"))
        {
            DataOutput();
        }
    }

    public void DataInput()
    {
        UserData character = ReaderBoard.Load("UserDB");
        if (character == null)
            character = new UserData();

        // 데이터 입력은 추후에 수정
        character.users.Add(new User("YDS", UnityEngine.Random.Range(0f, 100f), n++));
        ReaderBoard.Save(character, "UserDB");
    }

    public void DataOutput()
    {
        UserData loadData = ReaderBoard.Load("UserDB");
        loadData.users.Sort((a, b) => a._time.CompareTo(b._time));

        // 데이터를 UI에 보낼 구문
        foreach (User user in loadData.users)
            Debug.Log(user._name + ' ' + user._time + ' ' + user._resurrect);
    }
}
