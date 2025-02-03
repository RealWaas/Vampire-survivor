using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave/new Wave")]
public class WaveSO : ScriptableObject
{
    public List<EnemyWaveParametter> waveContent = new List<EnemyWaveParametter>();
    public int spawnInterval = 10;
    public int spawnCount = 10;
}

[Serializable]
public class EnemyWaveParametter
{
    public GameObject enemy;
    public int spawnAmount = 3;
}