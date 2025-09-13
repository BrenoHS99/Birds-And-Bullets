using UnityEngine;

public class BlockedNewArea : MonoBehaviour
{
    public GameObject newArea;

    public float enemiesAmount;
    public float enemiesDefeated;

    private void Start()
    {
        foreach (Transform enemy in newArea.transform)
        {
            if (enemy.CompareTag("Enemy") || enemy.CompareTag("EnemySpawn"))
            {
                enemiesAmount++;
            }
        }
    }

    private void Update()
    {
        if (enemiesDefeated >= enemiesAmount)
        {
            Destroy(this.gameObject);
        }
    }
}
