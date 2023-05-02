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
        // ���� �ñ�� ���� óġ �� �̸� �Է� ��
        if (Input.GetKeyDown("s"))
        {
            DataInput();
        }

        // �ε� �ñ�� �������� ������ ��
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

        // ������ �Է��� ���Ŀ� ����
        character.users.Add(new User("YDS", UnityEngine.Random.Range(0f, 100f), n++));
        ReaderBoard.Save(character, "UserDB");
    }

    public void DataOutput()
    {
        UserData loadData = ReaderBoard.Load("UserDB");
        loadData.users.Sort((a, b) => a._time.CompareTo(b._time));

        // �����͸� UI�� ���� ����
        foreach (User user in loadData.users)
            Debug.Log(user._name + ' ' + user._time + ' ' + user._resurrect);
    }
}
