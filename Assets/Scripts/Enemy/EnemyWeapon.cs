using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject gun;
    public GameObject bullet;
    public Transform bulletSpawnpoint;

    public string gunName;

    private GameObject bulletInst;

    private Vector2 direction;
    private float angle;

    private float gunLocalScaleY;

    public GameObject gameController;
    private GameController gameControllerScript;

    private Animator animator;

    private GameObject player;

    public float randomShootingA;
    public float randomShootingB;

    // Update is called once per frame
    private void Update()
    {
        if (!gameControllerScript.isPaused)
        {
            HandleGunRotation();
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        gunLocalScaleY = gun.transform.localScale.y;

        gameController = GameObject.FindWithTag("GameController");
        gameControllerScript = gameController.GetComponent<GameController>();

        animator = GetComponent<Animator>();

        StartCoroutine(EnemyShooting());
    }

    private void HandleGunRotation()
    {
        direction = (player.transform.position - gun.transform.position).normalized;
        gun.transform.up = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(gun.transform.localScale.x, gunLocalScaleY, gun.transform.localScale.z);
        localScale.y = gunLocalScaleY * -1;

        gun.transform.localScale = localScale;
    }

    public void GunShooting()
    {
        bulletInst = Instantiate(bullet, bulletSpawnpoint.transform.position, gun.transform.rotation);
    }

    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomShootingA, randomShootingB));
            animator.SetTrigger("StartShooting");
        }
    }
}
