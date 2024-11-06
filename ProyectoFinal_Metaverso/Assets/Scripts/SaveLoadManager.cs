using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    private static string MissionFilePath => Path.Combine(Application.persistentDataPath, "missions.json");
    private static string EasterEggFilePath => Path.Combine(Application.persistentDataPath, "easterEggs.json");

    public static void SaveMissions(MisionesSO missionChecklist)
    {
        string json = JsonUtility.ToJson(missionChecklist);
        File.WriteAllText(MissionFilePath, json);
        Debug.Log("Missions saved to " + MissionFilePath);
    }

    public static void SaveEasterEggs(EasterEggsSO easterEggChecklist)
    {
        string json = JsonUtility.ToJson(easterEggChecklist);
        File.WriteAllText(EasterEggFilePath, json);
        Debug.Log("Easter eggs saved to " + EasterEggFilePath);
    }

    public static void LoadMissions(MisionesSO missionChecklist)
    {
        if (File.Exists(MissionFilePath))
        {
            string json = File.ReadAllText(MissionFilePath);
            JsonUtility.FromJsonOverwrite(json, missionChecklist);
            Debug.Log("Missions loaded from " + MissionFilePath);
        }
        else
        {
            Debug.LogWarning("Mission file not found.");
        }
    }

    public static void LoadEasterEggs(EasterEggsSO easterEggChecklist)
    {
        if (File.Exists(EasterEggFilePath))
        {
            string json = File.ReadAllText(EasterEggFilePath);
            JsonUtility.FromJsonOverwrite(json, easterEggChecklist);
            Debug.Log("Easter eggs loaded from " + EasterEggFilePath);
        }
        else
        {
            Debug.LogWarning("Easter egg file not found.");
        }
    }
}