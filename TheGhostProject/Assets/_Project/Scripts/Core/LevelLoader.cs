using UnityEngine;
using System.IO;
using GhostProject.Core;
using GhostProject.Data;

public class LevelLoader : MonoBehaviour
{
    public string levelFileName = "level_01.json";

    public World LoadWorldFromFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Levels", levelFileName);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"Level file not found at: {filePath}");
            return null;
        }

        string jsonContent = File.ReadAllText(filePath);

        WorldWrapper wrapper = JsonUtility.FromJson<WorldWrapper>(jsonContent);


        World gameWorld = new World { name = wrapper.world.title };

        foreach (var sData in wrapper.world.sectors)
        {
            Sector sector = new Sector { name = sData.name };


            foreach (var h in sData.hazards)
            {

                sector.sectorObjects.Add(new Hazard());
            }


            gameWorld.sectors.Add(sector);
        }

        Debug.Log($"World '{gameWorld.name}' successfully loaded from JSON!");
        return gameWorld;
    }
}
