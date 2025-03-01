using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 targetDir = Vector3.down;
    TurretStats bulletStats;

    private void Update()
    {
        if(targetDir == Vector3.down)
            return;

        transform.position += targetDir.normalized * Time.deltaTime * bulletStats.bulletSpeed;
    }

    public void InitBullet(EnemyBehaviour _enemy, TurretStats _bulletStats)
    {
        targetDir = _enemy.transform.position - transform.position;
        bulletStats = _bulletStats;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != this.tag)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(bulletStats.bulletDamage);
                gameObject.SetActive(false);
            }
        }
    }
}
