using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WaterSplash : MonoBehaviour
{
    public float damage = 5f;

    private List<GameObject> enemiesHitted = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !enemiesHitted.Contains(collision.gameObject))
        {
            EnemyController enemyScript = collision.gameObject.GetComponent<EnemyController>();
            enemyScript.health -= damage;
        }
    }
    public void AnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
