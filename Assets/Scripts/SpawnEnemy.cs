using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    private GameObject enemyInst;

    public float enemyHealth;
    public float enemySpeed;
    public float enemyMaxHealth;
    public float enemyKnockback;
    public float enemyKnockbackRecover;

    public void EnemySpawn(GameObject blockedArea)
    {
        enemyInst = Instantiate(enemy, this.gameObject.transform.position, this.gameObject.transform.rotation);

        EnemyController enemyControllerScript = enemyInst.GetComponent<EnemyController>();

        enemyControllerScript.blockedArea = blockedArea;

        enemyControllerScript.health = enemyHealth;
        enemyControllerScript.speed = enemySpeed;
        enemyControllerScript.maxHealth = enemyMaxHealth;
        enemyControllerScript.knockback = enemyKnockback;
        enemyControllerScript.knockbackRecover = enemyKnockbackRecover;
    }
}
