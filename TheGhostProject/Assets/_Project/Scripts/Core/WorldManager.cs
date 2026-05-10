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
    [SerializeField] private UnityObject obstacleStaticPrefab;

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

        // JSON raw data
        var lastLoaded = loader.GetLastLoadedData();
        if (lastLoaded == null || lastLoaded.world == null || lastLoaded.world.sectors == null)
        {
            Debug.LogError("[WorldManager] Couuldn't find raw data in levelLoader.");
            return;
        }

        var rawData = lastLoaded.world.sectors[index];
        Debug.Log($"[WorldManager] Sector data have been read. Number of hazards: {rawData.hazards?.Count}, Number of drones: {rawData.drones?.Count}");

        // 2. Player
        if (playerPrefab != null && rawData.playerStart != null)
        {
            Vector3 startPos = new Vector3(rawData.playerStart.x, rawData.playerStart.y, 0);
            UnityObject p = Instantiate(playerPrefab, startPos, Quaternion.identity);

            // Méret beállítása 0.4-re
            p.transform.localScale = new Vector3(0.4f, 0.4f, 1f);

            // A kamera célpontjának beállítása (ha a CameraController-t használod)
            if (Camera.main.TryGetComponent<CameraController>(out var camCtrl))
            {
                camCtrl.SetTarget(p.transform);
            }

            Debug.Log($"[WorldManager] Player spawned at {startPos} with scale 0.4");
        }

        // 3. Hazards
        if (rawData.hazards != null)
        {
            foreach (var h in rawData.hazards)
            {
                UnityObject prefabToUse = (h.type == "static_wall") ? obstacleStaticPrefab : hazardPrefab;
                if (hazardPrefab != null)
                {
                    Vector3 pos = new Vector3(h.x, h.y, -1);
                    UnityObject hazardObj = Instantiate(prefabToUse, pos, Quaternion.identity);
                    hazardObj.transform.localScale = new Vector3(h.width, h.height, 1f);
                    Debug.Log($"[WorldManager] -> Hazard placed at: x = {h.x}, y = {h.y}");
                }
                else
                {
                    Debug.LogError("[WorldManager] Missing hazardPrefab.");
                }
            }
        }

        // 3. Hazards
        if (rawData.terminals != null)
        {
            foreach (var t in rawData.terminals)
            {
                UnityObject prefabToUse = obstacleStaticPrefab;
                if (obstacleStaticPrefab != null)
                {
                    Vector3 pos = new Vector3(t.x, t.y, -1);
                    UnityObject terminalObj = Instantiate(prefabToUse, pos, Quaternion.identity);
                    terminalObj.transform.localScale = new Vector3(t.width, t.height, 1f);
                    Debug.Log($"[WorldManager] -> Terminal placed at: x = {t.x}, y = {t.y}");
                }
                else
                {
                    Debug.LogError("[WorldManager] Missing TerminalPrefab.");
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
                    Vector3 dronePos = new Vector3(d.x, d.y, -1);
                    UnityObject droneObj = Instantiate(dronePrefab, dronePos, Quaternion.identity);
                    droneObj.transform.localScale = new Vector3(0.46f, 0.46f, 1f); 
                    Debug.Log($"[WorldManager] -> Drone placed at: x = {d.x}, y = {d.y}");

                    if (droneObj.TryGetComponent<DronePatrol>(out var patrolScript))
                    {
                        patrolScript.patrolType = d.patrolType;
                    }
                }
                else
                {
                    Debug.LogError("[WorldManager] Missing dronePrefab.");
                }
            }
        }
    }
}
