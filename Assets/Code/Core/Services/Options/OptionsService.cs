using System.Collections.Generic;
using Zenject;

public class OptionsService
{
    public BoolOptionEntity sound;
    public IntOptionEntity difficulty;
    private ISaveMethod _saveMethod;
    private List<ISavableData> _savableEntities = new List<ISavableData>();

    public OptionsService(ISaveMethod saveMethod)
    {
        _saveMethod = saveMethod;

        sound = new BoolOptionEntity("sound");
        difficulty = new IntOptionEntity("difficulty");

        _savableEntities.Add(sound);
        _savableEntities.Add(difficulty);

        Load();
    }

    public void Save()
    {
        _saveMethod.Save(_savableEntities);
    }

    public void Load()
    {
        _saveMethod.Load(_savableEntities);
    }
}
