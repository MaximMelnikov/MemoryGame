using System.Collections.Generic;

public interface ISaveMethod
{
    public void Save(List<ISavableData> list);
    public void Load(List<ISavableData> list);
}