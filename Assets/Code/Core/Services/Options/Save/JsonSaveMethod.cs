using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;

public class JsonSaveMethod : ISaveMethod
{
    public void Load(List<ISavableData> list)
    {
        ReadFromFile(list);
    }

    public void Save(List<ISavableData> list)
    {
        Dictionary<string, string> options = new Dictionary<string, string>(list.Count);
        foreach (var item in list)
        {
            var serializedOption = item.Serialize();
            options.Add(serializedOption.Item1, serializedOption.Item2);
        }

        WriteToFile(options);
    }

    private void ReadFromFile(List<ISavableData> list)
    {
        string path = Application.persistentDataPath + "/save.json";

        try
        {
            if (!File.Exists(path))
            {
                Debug.Log($"File doesn't exist: {path}");
                return;
            }

            string json = File.ReadAllText(path);

            var dictionary = JsonUtility.FromJson<Dictionary<string, string>>(json);
            foreach (var item in list)
            {
                item.Deserialize(dictionary[item.Name]);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void WriteToFile(Dictionary<string, string> options)
    {
        string path = Application.persistentDataPath + "/save.json";

        try
        {
            var jsonString = JsonUtility.ToJson(options, true);

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            File.WriteAllText(path, jsonString);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
