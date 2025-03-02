using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/new Enemy")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public float enemyHealth;
    public float enemyDamages;
    public float enemySpeed;
    public int enemyMoney;
}