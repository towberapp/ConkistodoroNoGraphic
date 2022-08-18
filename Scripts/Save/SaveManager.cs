using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static Save Load(string saveName)
    {
        string filePath;
        if (!Application.isEditor)
        {
            filePath = Application.persistentDataPath + $"/{saveName}.save";
        }
        else
        {
            filePath = saveName + ".save";
        }
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(filePath))
        {
            FileStream fs = File.Open(filePath, FileMode.Open);
            fs.Position = 0;
            Save save = (Save)bf.Deserialize(fs);
            fs.Close();
            Debug.Log($"Save: {saveName} loaded!");
            return save;
        }
        else
        {
            Debug.Log($"Save: {filePath} not Found");
            return null;
        }
    }

    public static void Save(Save newSave, string fileName)
    {
        string filePath;
        if (!Application.isEditor)
        {
            filePath = Application.persistentDataPath + $"/{fileName}.save";
        }
        else
        {
            filePath = fileName + ".save";
        }
        
        if (File.Exists(filePath))
            File.Delete(filePath);
        FileStream fs = File.Create(filePath);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, newSave);
        fs.Close();
        Debug.Log($"Save: {fileName}.save writed");
    }
}
