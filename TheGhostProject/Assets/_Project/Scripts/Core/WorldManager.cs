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
    [SerializeField] private UnityObject terminalPrefab;

    private Game gameInstance;
    private int currentSectorIndex = 0;

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

    public void LoadSector(int index)
    {
        var lastLoaded = loader.GetLastLoadedData();
        if (lastLoaded == null || lastLoaded.world == null || lastLoaded.world.sectors == null)
        {
            Debug.LogError("[WorldManager] Couldn't find raw data in levelLoader.");
            return;
        }

        // Ellenőrizzük, hogy van-e még szektor a JSON fájlban
        if (index >= lastLoaded.world.sectors.Count)
        {
            Debug.Log("<color=gold>[VICTORY]: Nincs több szektor a JSON-ben! Megnyerted a játékot!</color>");
            // Ide jöhet majd a stáblista, főmenübe való visszatérés, vagy egy győzelmi képernyő
            return;
        }

        // 1. Megjegyezzük az aktuális indexet
        currentSectorIndex = index;

        // 2. Kitakarítjuk az előző pálya összes elemét a jelenetből
        ClearCurrentSector();

        // 3. Legyártjuk az új szektort
        SpawnSector(index);
    }

    // SZENIOR BŐVÍTÉS: Ezt a metódust fogja meghívni a GameManager, amikor a puzzle sikeresen bezárul
    public void NextSector()
    {
        LoadSector(currentSectorIndex + 1);
    }

    // SZENIOR BŐVÍTÉS: A "Takarítóbrigád"
    private void ClearCurrentSector()
    {
        // Összegyűjtjük és letöröljük az összes olyan Unity objektumot, amit mi spawnoltunk.
        // Hogy ez működjön, győződj meg róla, hogy a SpawnSector-ban létrehozott klónok megkapják 
        // a megfelelő "GeneratedElement" vagy "Player" taget!

        UnityObject[] oldElements = UnityObject.FindGameObjectsWithTag("GeneratedElement");
        foreach (UnityObject element in oldElements)
        {
            Destroy(element);
        }

        UnityObject oldPlayer = UnityObject.FindWithTag("Player");
        if (oldPlayer != null)
        {
            Destroy(oldPlayer);
        }

        Debug.Log("[WorldManager] Az előző szektor sikeresen kitakarítva.");
    }

    private void SpawnSector(int index)
    {
        var sector = gameInstance.world.GetSector(index);
        Debug.Log($"[WorldManager] Spawning Sector: {sector.name}");

        // JSON raw data
        var lastLoaded = loader.GetLastLoadedData();
        var rawData = lastLoaded.world.sectors[index];
        Debug.Log($"[WorldManager] Sector data have been read. Number of hazards: {rawData.hazards?.Count}, Number of drones: {rawData.drones?.Count}");


        // Player
        if (playerPrefab != null && rawData.playerStart != null)
        {
            Vector3 startPos = new Vector3(rawData.playerStart.x, rawData.playerStart.y, -0.01f);
            UnityObject p = Instantiate(playerPrefab, startPos, Quaternion.identity);

            p.transform.localScale = new Vector3(3.48f, 3.8f, 9.5f);

            if (Camera.main.TryGetComponent<CameraController>(out var camCtrl))
            {
                camCtrl.SetTarget(p.transform);
            }

            Debug.Log($"[WorldManager] Player spawned at {startPos} with scale 0.4");
        }

        // Hazards
        if (rawData.hazards != null)
        {
            foreach (var h in rawData.hazards)
            {
                UnityObject prefabToUse = (h.type == "static_wall") ? obstacleStaticPrefab : hazardPrefab;
                if (hazardPrefab != null)
                {
                    Vector3 pos = new Vector3(h.x, h.y, -1);
                    UnityObject hazardObj = Instantiate(prefabToUse, pos, Quaternion.identity);
                    hazardObj.tag = "GeneratedElement";
                    hazardObj.transform.localScale = new Vector3(h.width, h.height, 1f);
                    Debug.Log($"[WorldManager] -> Hazard placed at: x = {h.x}, y = {h.y}");
                }
                else
                {
                    Debug.LogError("[WorldManager] Missing hazardPrefab.");
                }
            }
        }

        // Terminals
        if (rawData.terminals != null)
        {
            foreach (var t in rawData.terminals)
            {
                UnityObject prefabToUse = terminalPrefab;
                if (obstacleStaticPrefab != null)
                {
                    Vector3 pos = new Vector3(t.x, t.y, -1);
                    UnityObject terminalObj = Instantiate(prefabToUse, pos, Quaternion.identity);
                    terminalObj.tag = "GeneratedElement";
                    terminalObj.transform.localScale = new Vector3(t.width, t.height, 1f);
                    Debug.Log($"[WorldManager] -> Terminal placed at: x = {t.x}, y = {t.y}");
                }
                else
                {
                    Debug.LogError("[WorldManager] Missing TerminalPrefab.");
                }
            }
        }

        // Drones
        if (rawData.drones != null)
        {
            foreach (var d in rawData.drones)
            {
                if (dronePrefab != null)
                {
                    Vector3 dronePos = new Vector3(d.x, d.y, -1);
                    UnityObject droneObj = Instantiate(dronePrefab, dronePos, Quaternion.identity);
                    droneObj.tag = "GeneratedElement";
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

        //history
        if (!string.IsNullOrEmpty(rawData.story))
        {
            Debug.Log($"<color=cyan>[STORY LOADED]:</color> <color=purple>{rawData.story}</color>");
        }
    }
}
