using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public GameObject enemy;

    private EnemyController enemyScript;

    private void Start()
    {
        enemyScript = enemy.GetComponent<EnemyController>();
    }
}
