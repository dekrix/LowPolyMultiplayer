using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // Data file name is "cornpopT.dat"
    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = File.Open(Application.persistentDataPath + "/LMG.dat", FileMode.Create);
        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/LMG.dat";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = new PlayerData();

            if (stream.Length == 0)
            {
                formatter.Serialize(stream, data);
            }
            else
            {
                data = formatter.Deserialize(stream) as PlayerData;
            }

            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = File.Open(Application.persistentDataPath + "/LMG.dat", FileMode.Create);

            PlayerData data = new PlayerData();

            formatter.Serialize(stream, data);
            stream.Close();

            return data;
        }
    }
}

