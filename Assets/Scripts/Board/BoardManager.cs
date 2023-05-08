using Manager;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class User
{
    public string _name;
    public int _time;
    public User(string _name, int _time)
    {
        this._name = _name;
        this._time = _time;
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

    public GameObject InputField;
    public Text[] Initials;

    public GameObject Board;
    public GameObject[] Ranker;

    public GameObject Now;
    int time;
    float timer = 1f;

    private void Start()
    {
        time = Convert.ToInt32(GameManager.GameTimer);
    }

    void Update()
    {
        // 저장 시기는 보스 처치 후 이름 입력 시
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (InputField.activeSelf)
            {
                if (DataInput())
                {
                    InputField.SetActive(false);
                    DataOutput();
                }
            }
            else if (Board.activeSelf)
            {
                // 타이틀 씬 이동
                SceneManager.LoadScene("Title");
            }
        }

        ColorChange();
    }

    void ColorChange()
    {
        Initials[0].color = Color.Lerp(Color.red, Color.white, timer);
        Initials[1].color = Color.Lerp(Color.red, Color.white, timer);
        Initials[2].color = Color.Lerp(Color.red, Color.white, timer);
        timer += Time.deltaTime;
    }

    public bool DataInput()
    {
        UserData character = JsonManager<UserData>.Load("UserDB");
        if (character == null)
            character = new UserData();

        string name = Initials[0].text + Initials[1].text + Initials[2].text;
        if (name == "SEX")
        {
            timer = 0f;
            return false;
        }
        // 데이터 입력
        Now.transform.GetChild(1).GetComponent<Text>().text = name;
        Now.transform.GetChild(2).GetComponent<Text>().text = time.ToString();
        character.users.Add(new User(name, time));
        JsonManager<UserData>.Save(character, "UserDB");
        return true;
    }

    public void DataOutput()
    {
        UserData loadData = JsonManager<UserData>.Load("UserDB");
        loadData.users.Sort((a, b) => a._time.CompareTo(b._time));

        // 데이터를 UI에 보낼 구문
        for (int i = 0; i < Ranker.Length; i++)
        {
            if (i >= loadData.users.Count)
                break;

            Ranker[i].transform.GetChild(1).GetComponent<Text>().text = loadData.users[i]._name;
            Ranker[i].transform.GetChild(2).GetComponent<Text>().text = loadData.users[i]._time.ToString();
            Ranker[i].SetActive(true);
        }
        Board.SetActive(true);
    }
}
