using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class HighScoreSaveSystem 
{
    public static string path = Application.persistentDataPath + "/highscore.espcoacod";
    public static void SaveScores(GameManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);


        ScoreData data = new ScoreData(manager);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void SaveScores(ScoreData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static ScoreData LoadHighScoreData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoreData data = formatter.Deserialize(stream) as ScoreData;
            stream.Close();
            return data;    
        } else
        {
            SaveEmptyScoreData();
            return new ScoreData();    
        }
    }

    private static void SaveEmptyScoreData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);


        ScoreData data = new ScoreData();

        formatter.Serialize(stream, data);

        stream.Close();
    }


}
