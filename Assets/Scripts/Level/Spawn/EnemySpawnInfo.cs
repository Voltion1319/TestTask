using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnInfo
{
    public Transform SpawnPointsParent;
    public List<Transform> EnemiesSpawnPoints;
}