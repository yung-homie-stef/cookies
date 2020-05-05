using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Save_Load_Test : MonoBehaviour
{
    public static string fileName = "cookies";
    public static string fileExtension = "save";
    public static string data2Save;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Load()
    {

        Debug.Log("loading");

        if (System.IO.File.Exists(Application.persistentDataPath + "\\" + fileName + "." + fileExtension))
        {
            string finalOutput = "";
            StreamReader loadReader = null;
            loadReader = new StreamReader(Application.persistentDataPath + "\\" + fileName + "." + fileExtension);

            while (!loadReader.EndOfStream)
            {
                string readLine = loadReader.ReadLine();
                finalOutput += readLine;
            }
            loadReader.Close();
            Debug.Log(finalOutput);
        }
        else
        {
            Debug.Log("no file to load");
        }
    }

    public static ProgressInformation LoadProgress()
    {
        Debug.Log("loading");
        if (System.IO.File.Exists(Application.persistentDataPath + "\\" + fileName + "." + fileExtension))
        {
            byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "\\" + fileName + "." + fileExtension);

            ProgressInformation info = BytesToInfo(bytes);
            return info;
        }
        else
        {
            
            return new ProgressInformation();
        }
    }

    static ProgressInformation BytesToInfo(byte[] bytes)
    {
        ProgressInformation info = null;
        MemoryStream ms = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();

        ms.Write(bytes, 0, bytes.Length);
        ms.Seek(0, SeekOrigin.Begin);
        info = (ProgressInformation)bf.Deserialize(ms);

        return info;
    }

    void Save()
    {
        Debug.Log("saving");
        byte[] benis = System.Text.Encoding.UTF8.GetBytes(data2Save);
        System.IO.FileStream saveStream = null;
        saveStream = new System.IO.FileStream(Application.persistentDataPath + "\\" + fileName + "." + fileExtension, System.IO.FileMode.Create);
        saveStream.Write(benis, 0, benis.Length);
        saveStream.Close();
    }

    public static void SaveProgress(ProgressInformation info)
    {
        byte[] bytes = null;

        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, info);
            bytes = ms.ToArray();
        }

        Debug.Log("saving");
        System.IO.FileStream saveStream = null;
        saveStream = new System.IO.FileStream(Application.persistentDataPath + "\\" + fileName + "." + fileExtension, System.IO.FileMode.Create);
        saveStream.Write(bytes, 0, bytes.Length);
        saveStream.Close();
    }

    public static void Delete()
    {
        Debug.Log("deleting");
        File.Delete(Application.persistentDataPath + "\\" + fileName + "." + fileExtension);
    }

}
