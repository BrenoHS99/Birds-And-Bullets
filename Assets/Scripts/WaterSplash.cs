using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WaterSplash : MonoBehaviour
{
    private List<GameObject> enemiesHitted = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !enemiesHitted.Contains(collision.gameObject))
        {
            EnemyController enemyScript = collision.gameObject.GetComponent<EnemyController>();
            enemyScript.health -= 5;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
