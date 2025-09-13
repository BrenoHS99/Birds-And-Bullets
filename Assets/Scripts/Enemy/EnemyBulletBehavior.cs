using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 5f;
    public float damage = 5f;

    public GameObject shootSound;
    public GameObject sfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfx = GameObject.FindWithTag("Sfx");
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Vector3 localScale = new Vector3(transform.localScale.x, transform.localScale.y,transform.localScale.z);
        localScale.y = transform.localScale.y * -1;

        TempSplashSound();
        transform.localScale = localScale;
        Destroy(this.gameObject, 3f);
    }

    private void TempSplashSound()
    {
        GameObject shootSoundInst;
        shootSoundInst = Instantiate(shootSound, transform.position, transform.rotation);
        shootSoundInst.transform.parent = sfx.transform;
        AudioSource shootSoundSource = shootSoundInst.GetComponent<AudioSource>();
        shootSoundSource.Play();
        Destroy(shootSoundInst, shootSoundSource.clip.length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();

            playerScript.TakeDamage(this.gameObject, damage, 10f, 0.1f);
            Destroy(this.gameObject);
        }
    }
}
