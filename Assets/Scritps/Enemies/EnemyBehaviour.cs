using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int indexOnPath { get; private set; } = 0;
    [SerializeField] private float moveSpeed = 2;

    [ContextMenu("Init")]
    private void InitializeEnemy()
    {
        indexOnPath = 0;
    }

    private void Update()
    {
        Vector3 targetDir = GridManager.mainPath[indexOnPath] - transform.position;

        if(targetDir.magnitude <= 0.1f) 
            OnTargetReached();

        transform.position += targetDir.normalized * Time.deltaTime * moveSpeed;
    }

    private void OnTargetReached()
    {
        if(indexOnPath +1 >=  GridManager.mainPath.Count)
        {
            SpawnManager.instance.OnEnemyDeath();
            Destroy(gameObject);
        }
        else
            indexOnPath ++;
    }
}
