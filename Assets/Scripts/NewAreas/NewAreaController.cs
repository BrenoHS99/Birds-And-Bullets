using UnityEngine;
using UnityEngine.SceneManagement;

public class NewAreaController : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject blockedArea;

    private bool touched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cameraObject.transform.position = this.gameObject.transform.position;
            if (!touched)
            {
                touched = true;
                foreach (Transform enemySpawn in this.gameObject.transform)
                {
                    if (enemySpawn.tag == "EnemySpawn")
                    {
                        SpawnEnemy spawnEnemyScript = enemySpawn.GetComponent<SpawnEnemy>();
                        spawnEnemyScript.EnemySpawn(blockedArea);
                    }
                }
            }
        }
    }
}
