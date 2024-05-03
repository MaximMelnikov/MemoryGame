using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//not safe save method, better convert List<ISavableData> to binary data and save it. But i think task was to write all options values separately... maybe...

public class BinaryFileSaveMethod : ISaveMethod
{
    public void Load(List<ISavableData> list)
    {
        ReadFromFile(list);
    }

    public void Save(List<ISavableData> list)
    {
        WriteToFile(list);
    }

    private void ReadFromFile(List<ISavableData> list)
    {
        string path = Application.persistentDataPath + "/save.bin";

        try
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                foreach (var item in list)
                {
                    if (item is IOptionEntity<int> integer)
                    {
                        integer.Value = reader.ReadInt32();
                    }
                    else if (item is IOptionEntity<bool> boolean)
                    {
                        boolean.Value = reader.ReadBoolean();
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void WriteToFile(List<ISavableData> list)
    {
        string path = Application.persistentDataPath + "/save.bin";

        try
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                foreach (var item in list)
                {
                    if (item is IOptionEntity<int> integer)
                    {
                        writer.Write(integer.Value);
                    }
                    else if (item is IOptionEntity<bool> boolean)
                    {
                        writer.Write(boolean.Value);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
