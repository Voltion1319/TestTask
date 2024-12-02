using System;
using UnityEngine;

public static class ResourceLoader
{
    public static string LevelPath = "Levels/Level{0}";
    public static string PlayerPrefabPath = "Units/Player";
    public static string EnemyPrefabPath = "Units/Enemy";

    public static Level LoadLevel(int level)
    {
        return Resources.Load<Level>(String.Format(LevelPath, level));
    }
    
    public static void UnLoadResource(GameObject asset)
    {
        Resources.UnloadAsset(asset);
    }
}
