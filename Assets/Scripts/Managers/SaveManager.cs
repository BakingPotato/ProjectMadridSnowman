using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SaveManager
{
    static string dataPath = Application.persistentDataPath + "/data.json";
    static string pepper = "Q:c8!r7pb2L)<6~A";

    public static GameData GameDataInstance = new GameData();

    [Serializable]
    public class GameData
    {
        public string hash = "";
        public LevelData[] levels;
    }

    [Serializable]
    public struct LevelData
    {
        public string levelName;
        public int record;
        public bool unlocked;
    }

    /// <summary>
    /// If new game has been started at any time, this file will exist
    /// </summary>
    /// <returns></returns>
    public static bool CheckData()
    {
        return File.Exists(dataPath);
    }

    public static void DeleteData()
    {
        File.Delete(dataPath);
    }

    public static void WriteData()
    {
        GameDataInstance.hash = "";

        //Generate the json without pepper
        string json = JsonUtility.ToJson(GameDataInstance);

        //Add the pepper
        json += pepper;

        //Generate the hash
        GameDataInstance.hash = HashFunction(json);

        //Generate the true json with the hash
        json = JsonUtility.ToJson(GameDataInstance);

        //Save the json
        StreamWriter file = new StreamWriter(dataPath);
        file.Write(json);
        file.Close();

    }

    public static void ReadData()
    {
        //Read the json
        StreamReader file = new StreamReader(dataPath);
        string json = file.ReadLine();
        file.Close();

        //Deserialize the json
        JsonUtility.FromJsonOverwrite(json, GameDataInstance);
        //Store the hash to check the legitimacy of the file and set our hash to empty
        string auxHash = GameDataInstance.hash;
        GameDataInstance.hash = "";
        //Serialize the object without the hash and add the pepper
        json = JsonUtility.ToJson(GameDataInstance);
        json += pepper;
        //If the hash we stored previously and the hash we just generated aren't equal, it means the user 
        //tried to cheat, so we delete the progress
        string currentHash = HashFunction(json);
        if (currentHash != auxHash)
        {
            Debug.LogError("DELETING SAVE FILE, INCORRECT HASH: " + currentHash);
            File.Delete(dataPath);
        }

    }

    static string HashFunction(string input)
    {
        SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Convert byte array to a string   
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
}