using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManger
{

    public static void SaveData<T>(T playerData, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        fileName = Application.persistentDataPath + "/" + fileName;
        FileStream stream = new FileStream(fileName, FileMode.Create);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static T LoadData<T>(string fileName) where T : class
    {
        fileName = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(fileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var stream = File.OpenRead(fileName))
                return formatter.Deserialize(stream) as T;
        }
        else
        {
            Debug.LogError("File not found...");
            return null;
        }
    }
}