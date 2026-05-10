using System;
using System.Collections.Generic;

namespace GhostProject.Data
{
    [Serializable]
    public class WorldWrapper
    {
        public WorldData world;
    }

    [Serializable]
    public class WorldData
    {
        public string title;
        public List<SectorData> sectors;
    }

    [Serializable]
    public class PlayerData
    {
        public float x;
        public float y;
    }

    [Serializable]
    public class SectorData
    {
        public string id;
        public string name;
        public string description;
        public PlayerData playerStart;
        public List<HazardData> hazards;
        public List<DroneData> drones;
        public List<TerminalData> terminals;
        public List<TransitionData> transitions;
    }

    [Serializable]
    public class HazardData
    {
        public string type; 
        public float x; 
        public float y; 
        public float width; 
        public float height; 
    }

    [Serializable]
    public class DroneData
    {
        public float x; 
        public float y; 
        public string patrolType; 
    }

    [Serializable]
    public class TerminalData
    {
        public string id; 
        public float x; 
        public float y;
        public float width;
        public float height;
        public string message; 
    }

    [Serializable]
    public class TransitionData
    {
        public string targetSector; 
        public float x; 
        public float y; 
        public bool requiredComplete; 
    }
}