using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MapConfig", menuName = "Data/MapConfig")]
public class MapConfig : ScriptableObject
{
    public MapSetting[] settings;

}

[Serializable]
public class MapSetting
{
    public GameObject prefabAsteroid;
    [Range(1,50)]
    public int countWin;
    public float waveSpawn;
    public int waveAsteroids;
}
