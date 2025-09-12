using System.Runtime.CompilerServices;
using UnityEngine;

public class BossAreaController : MonoBehaviour
{
    public GameObject cameraObject;
    public Transform bossSpawn;
    public GameObject boss;

    private bool touched = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cameraObject.transform.position = this.gameObject.transform.position;
            if (!touched)
            {
                touched = true;
                SpawnBoss();
            }
        }
    }

    private void SpawnBoss()
    {
        GameObject bossInst = Instantiate(boss, bossSpawn.position, bossSpawn.rotation);
    }
}
