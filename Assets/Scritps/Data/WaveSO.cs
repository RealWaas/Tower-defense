using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave/new Wave")]
public class WaveSO : ScriptableObject
{
    public List<EnemyWaveParametter> waveContent = new List<EnemyWaveParametter>();

    public int spawnAmount = 10;

    public float spawnInterval = 10;
}

[Serializable]
public class EnemyWaveParametter
{
    public GameObject prefab;

    public float weight = 1;
}