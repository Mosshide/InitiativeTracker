using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public SaveFile saveFile;

    void Awake()
    {
        saveFile = Load();
        if (saveFile == null)
        {
            saveFile = new SaveFile();
        }
    }

    public void Save(string fileName = "save.dat")
    {
        string destination = Application.persistentDataPath + "/" + fileName;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            bf.Serialize(file, saveFile);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            file.Close();
        }
    }

    public SaveFile Load()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        //Debug.Log(Application.persistentDataPath + "/save.dat");

        if (File.Exists(destination))
        {
            FileStream file = new FileStream(destination, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            SaveFile data = (SaveFile)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else return null;
    }
}
