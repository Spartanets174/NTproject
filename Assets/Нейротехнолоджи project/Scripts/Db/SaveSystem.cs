using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem {
    public static void SavePlayer(int id)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/player.fun";
        FileStream stream = new FileStream(path,  FileMode.Create);
        int idToSave = id;
        formatter.Serialize(stream, idToSave);
        stream.Close();
    }

    public static int LoadPlayer()
    {
        string path = Application.dataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int idToLoad = (int)formatter.Deserialize(stream);
            stream.Close();
            return idToLoad;
        }
        else
        {        
            return -1;
        }

    }
}
