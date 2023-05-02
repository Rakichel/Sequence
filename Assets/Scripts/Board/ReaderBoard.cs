using System.IO;
using UnityEngine;

public static class ReaderBoard
{
    private static string _path = Application.persistentDataPath;

    public static void Save(UserData _userData, string _fileName)
    {
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
        string _saveJson = JsonUtility.ToJson(_userData);
        string _filePath = _path + "/" + _fileName + ".json";

        File.WriteAllText(_filePath, _saveJson);
        Debug.Log("Save : " + _filePath);
    }

    public static UserData Load(string _fileName)
    {
        string _filePath = _path + "/" + _fileName + ".json";

        if (!File.Exists(_filePath))
        {
            Debug.LogError("No such saveFile exists");
            return null;
        }

        string saveFile = File.ReadAllText(_filePath);
        UserData _userData = JsonUtility.FromJson<UserData>(saveFile);
        return _userData;
    }
}
