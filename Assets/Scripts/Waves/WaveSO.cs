using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave/new Wave")]
public class WaveSO : ScriptableObject
{
    public List<EnemyWaveParametter> waveContent = new List<EnemyWaveParametter>();

    // The amount of enemies spawn each wave
    public int spawnAmount = 10;

    public float waveDuration = 60;
    public float spawnInterval = 10;
}

[Serializable]
public class EnemyWaveParametter
{
    public GameObject prefab;

    public float weight = 1;
}