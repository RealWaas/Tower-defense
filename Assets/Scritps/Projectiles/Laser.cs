using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    TurretStats bulletStats;
    private Dictionary<IDamagable, float> damageTimers = new Dictionary<IDamagable, float>();


    public void InitBullet(EnemyBehaviour _enemy, TurretStats _bulletStats)
    {
        bulletStats = _bulletStats;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != this.tag)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                if (!damageTimers.ContainsKey(damagable))
                    damageTimers[damagable] = Time.time;

                if (Time.time - damageTimers[damagable] >= bulletStats.attackSpeed)
                {
                    damagable.TakeDamage(bulletStats.bulletDamage);
                    damageTimers[damagable] = Time.time;
                    
                    damagable.TakeSlow(bulletStats.bulletSpeed);
                }
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag != this.tag)
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
                damageTimers.Remove(damagable);
    }
}
