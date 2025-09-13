using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class BossGun : MonoBehaviour
{
    public GameObject gun;
    public GameObject bullet;
    public GameObject superBullet;
    public Transform bulletSpawnpoint;

    private Vector2 direction;
    private float angle;

    private float gunLocalScaleY;

    public GameObject gameController;
    private GameController gameControllerScript;

    private Animator animator;

    private GameObject player;

    public float randomShootingA;
    public float randomShootingB;

    public float bulletsSkillOne = 5f;
    public float skillOneCD = 0.1f;

    public GameObject superShootSound;
    private GameObject sfx;

    public float waitAfterSuperShoot = 4f;

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
        sfx = GameObject.FindWithTag("Sfx");

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
    private void TempSuperShootSound()
    {
        GameObject shootSoundInst;
        shootSoundInst = Instantiate(superShootSound, transform.position, transform.rotation);
        shootSoundInst.transform.parent = sfx.transform;
        AudioSource shootSoundSource = shootSoundInst.GetComponent<AudioSource>();
        shootSoundSource.Play();
        Destroy(shootSoundInst, shootSoundSource.clip.length);
    }
    IEnumerator SkillOne()
    {
        for (int i = 0; i <= bulletsSkillOne; i++)
        {
            GameObject bulletInst = Instantiate(bullet, bulletSpawnpoint.transform.position, gun.transform.rotation);
            Destroy(bulletInst, 3f);
            yield return new WaitForSeconds(skillOneCD);
        }
    }

    public void SkillTwo()
    {
        GameObject bulletInst = Instantiate(superBullet, bulletSpawnpoint.transform.position, gun.transform.rotation);
        Destroy(bulletInst, 3f);
    }
    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomShootingA, randomShootingB));
            if (Random.Range(1, 5) == 1)
            {
                animator.SetTrigger("Shoot");
                TempSuperShootSound();
                yield return new WaitForSeconds(waitAfterSuperShoot);
            }
            else
            {
                StartCoroutine(SkillOne());
            }
        }
    }
}
