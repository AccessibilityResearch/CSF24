using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleInfo
{
    public String Name;
    public Vector3 Scale;
    public Vector3 Position;
    public Vector3 StartPoint;
    public Vector3 EndPoint;
    public float RespawnDelay;
    public float Speed;
    public bool BackNForth;
    public String ObstacleMaterialPath = "";
    public string soundEvent;
}

[System.Serializable]
public class SceneInfo
{
    // To display the Scene Number in the JSON File
    public int SceneNumber = 0;

    #region Hallway Data
    public int HallwayWidth = 0;
    public int HallwayLength = 0;
    public int HallwayHeight = 0;
    public float HallwayScale = 0;

    public String HallwayFloorMaterialPath = "";
    public String HallwayLeftMaterialPath = "";
    public String HallwayFarMaterialPath = "";
    public String HallwayCeilingMaterialPath = "";
    public String HallwayRightMaterialPath = "";
    public String HallwayCloseMaterialPath = "";
    #endregion

    #region Obstacles
    public List<ObstacleInfo> Obstacles = new List<ObstacleInfo>();
    public List<ObstacleInfo> MovingObstacles = new List<ObstacleInfo>();
    #endregion


}

[System.Serializable]
public class AllScenesInfo
{
    public List<SceneInfo> Scenes = new List<SceneInfo>();
}