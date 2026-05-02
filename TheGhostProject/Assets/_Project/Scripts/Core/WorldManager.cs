using UnityEngine;
using GhostProject.Core;
using System.Collections.Generic;

using UnityObject = UnityEngine.GameObject;
using CoreObject = GhostProject.Core.GameObject;

public class WorldManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private LevelLoader loader;

    [Header("Prefabs")]
    [SerializeField] private UnityObject playerPrefab;
    [SerializeField] private UnityObject dronePrefab;
    [SerializeField] private UnityObject hazardPrefab;

    private Game gameInstance;

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        gameInstance = new Game();
        gameInstance.world = loader.LoadWorldFromFile();

        if (gameInstance.world != null)
        {
            SpawnSector(0);
            gameInstance.StartGame();
        }
    }

    private void SpawnSector(int index)
    {
        var sector = gameInstance.world.GetSector(index);
        Debug.Log($"[WorldManager] Spawning Sector: {sector.name}");

        // Player
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            Debug.Log("[WorldManager] -> Player succefully placed.");
        }
        else
        {
            Debug.LogError("[WorldManager] Missing playerPrefab!");
        }

        // JSON raw data
        var lastLoaded = loader.GetLastLoadedData();
        if (lastLoaded == null || lastLoaded.world == null || lastLoaded.world.sectors == null)
        {
            Debug.LogError("[WorldManager] Couuldn't find raw data in levelLoader.");
            return;
        }

        var rawData = lastLoaded.world.sectors[index];
        Debug.Log($"[WorldManager] Sector data have been read. Number of hazards: {rawData.hazards?.Count}, Number of drones: {rawData.drones?.Count}");

        // 3. Hazards
        if (rawData.hazards != null)
        {
            foreach (var h in rawData.hazards)
            {
                if (hazardPrefab != null)
                {
                    Instantiate(hazardPrefab, new Vector3(h.x, h.y, 0), Quaternion.identity);
                    Debug.Log($"[WorldManager] -> Hazard placed at: x = {h.x}, y = {h.y}");
                }
                else
                {
                    Debug.LogError("[WorldManager] Missing hazardPrefab.");
                }
            }
        }

        // 4. Drones
        if (rawData.drones != null)
        {
            foreach (var d in rawData.drones)
            {
                if (dronePrefab != null)
                {
                    Instantiate(dronePrefab, new Vector3(d.x, d.y, 0), Quaternion.identity);
                    Debug.Log($"[WorldManager] -> Drone placed at: x = {d.x}, y = {d.y}");
                }
                else
                {
                    Debug.LogError("[WorldManager] Missing dronePrefab.");
                }
            }
        }
    }
}
