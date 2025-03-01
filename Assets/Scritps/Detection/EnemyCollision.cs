using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.tag != this.tag)
        //{
        //    if(other.gameObject.TryGetComponent(out HealthSystem healthSystem))
        //    {
        //        healthSystem.TakeDamage(10);
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != this.tag)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(10);
            }
        }
    }
}
