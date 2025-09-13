using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class BossAreaController : MonoBehaviour
{
    public GameObject cameraObject;
    public Transform bossSpawn;
    public GameObject boss;

    public GameObject gameController;
    private GameController gameControllerScript;

    public GameObject battleTheme;
    private AudioSource battleThemeSource;

    private bool touched = false;

    private void Start()
    {
        gameControllerScript = gameController.GetComponent<GameController>();
        battleThemeSource = battleTheme.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cameraObject.transform.position = this.gameObject.transform.position;
            if (!touched)
            {
                touched = true;
                battleThemeSource.Stop();
                gameControllerScript.StartCutscene();
                GameObject player = collision.gameObject;
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.health = playerController.maxHealth;
                playerController.UpdateHealthBar();
                SpawnBoss();
            }
        }
    }

    private void SpawnBoss()
    {
        GameObject bossInst = Instantiate(boss, bossSpawn.position, bossSpawn.rotation);
    }
}
