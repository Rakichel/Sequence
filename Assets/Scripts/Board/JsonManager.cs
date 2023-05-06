using System.IO;
using UnityEngine;

public static class JsonManager<T>
{
    private static string _path = Application.persistentDataPath;

    public static void Save(T _userData, string _fileName)
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

    public static T Load(string _fileName)
    {
        string _filePath = _path + "/" + _fileName + ".json";

        if (!File.Exists(_filePath))
        {
            Debug.LogError("No such saveFile exists");
            return default(T);
        }

        string saveFile = File.ReadAllText(_filePath);
        T _userData = JsonUtility.FromJson<T>(saveFile);
        return _userData;
    }

    public static void Delete(string _fileName)
    {
        string _filePath = _path + "/" + _fileName + ".json";

        if (!File.Exists(_filePath))
        {
            Debug.LogError("No such saveFile exists");
            return;
        }
        File.Delete(_filePath);
    }
}
