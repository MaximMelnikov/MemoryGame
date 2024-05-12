using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSaveMethod : ISaveMethod
{
    public void Load(List<ISavableData> list)
    {
        foreach (var item in list)
        {
            item.Deserialize(PlayerPrefs.GetString(item.Name));
        }
    }

    public void Save(List<ISavableData> list)
    {
        foreach (var item in list)
        {
            var serialized = item.Serialize();
            PlayerPrefs.SetString(serialized.Item1, serialized.Item2);
        }
    }
}