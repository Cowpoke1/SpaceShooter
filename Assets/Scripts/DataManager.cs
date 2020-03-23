using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour
{
    private string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
        //DeleteSaves();
    }

    public void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(filePath, FileMode.Create);

        Data data = new Data();

        data.level = GameController.Instance.UnlockedLevels;

        formatter.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

         BinaryFormatter formatter = new BinaryFormatter();
         FileStream file = new FileStream(filePath, FileMode.Open);

         Data save = (Data)formatter.Deserialize(file);
         file.Close();

         GameController.Instance.UnlockedLevels = save.level;

    }

    void DeleteSaves()
    {
        if (!File.Exists(filePath))
        {
            return;
        }
         File.Delete(filePath);
    }

}

[System.Serializable]
public class Data
{
    public int level;
}
