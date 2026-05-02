using UnityEngine;
using System.IO;
using GhostProject.Core;
using GhostProject.Data;

public class LevelLoader : MonoBehaviour
{
    public string levelFileName = "level_01.json";

    private WorldWrapper lastLoadedData;

    public World LoadWorldFromFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Levels", levelFileName);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"[LevelLoader] Level file not found at: {filePath}");
            return null;
        }

        string jsonContent = File.ReadAllText(filePath);
        Debug.Log($"[LevelLoader] Successfully read level JSON:\n{jsonContent}");

        if (string.IsNullOrEmpty(jsonContent))
        {
            Debug.LogError("[LevelLoader] Level file is empty!");
            return null;
        }
        try
        {
            lastLoadedData = JsonUtility.FromJson<WorldWrapper>(jsonContent);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[LevelLoader] Grave error during the use of JsonUtility: {ex.Message}");
            return null;
        }

        if (lastLoadedData == null)
        {
            Debug.LogError("[LevelLoader] JsonUtility.FromJson returned with null. Check WorldWrapper structure!");
            return null;
        }
        if (lastLoadedData.world == null)
        {
            Debug.LogError("[LevelLoader] 'lastLoadedData.world' object is null! JSON file root key doesn't match variable name in WorldWrapper.");
            return null;
        }

        World gameWorld = new World { name = lastLoadedData.world.title };

        if (lastLoadedData.world.sectors != null)
        {
            foreach (var sData in lastLoadedData.world.sectors)
            {
                Sector sector = new Sector { name = sData.name };

                if (sData.hazards != null)
                {
                    foreach (var h in sData.hazards)
                    {

                        sector.sectorObjects.Add(new Hazard());
                    }
                }

                gameWorld.sectors.Add(sector);
            }
        }

        Debug.Log($"World '{gameWorld.name}' successfully loaded from JSON!");
        return gameWorld;
    }

    public WorldWrapper GetLastLoadedData()
    {
        return lastLoadedData;
    }
}
