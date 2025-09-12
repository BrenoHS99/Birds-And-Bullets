using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class EnemyController : MonoBehaviour
{
    public float health = 10f;
    public float maxHealth = 10f;
    public float knockback = 1f;
    public float knockbackRecover = 0.5f;
    public float speed = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D hitbox;
    private Slider healthbarSlider;

    public GameObject healthbar;

    private Vector2 hitboxSize;

    public bool stunned = false;

    public GameObject gun;

    private float enemyAIActions = 0f;
    public float enemyAIwaitA = 0.1f;
    public float enemyAIwaitB = 0.5f;
    private Vector2 walkingDirection;

    private GameObject player;

    public LayerMask wallLayer;

    public GameObject blockedArea;
    private BlockedNewArea blockedAreaScript;

    private bool enemiesDefeatedRebound = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitbox = GetComponent<CapsuleCollider2D>();
        healthbarSlider = healthbar.GetComponent<Slider>();
        blockedAreaScript = blockedArea.GetComponent<BlockedNewArea>();
        health = maxHealth;
        StartCoroutine(EnemyAI());
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetFloat("Health", health);
        if (hitbox != null)
        {
            hitboxSize = hitbox.size * 0.7f;
        }
        if (rb != null)
        {
            if (rb.linearVelocityY > 0)
            {
                animator.SetBool("Facing", false);
            }
            else
            {
                animator.SetBool("Facing", true);
            }
        }

        if (health <= 0 && enemiesDefeatedRebound)
        {
            enemiesDefeatedRebound = false;
            blockedAreaScript.enemiesDefeated++;
        }
    }

    private void FixedUpdate()
    {
        if(rb != null)
        {
            RaycastHit2D hit = Physics2D.BoxCast(rb.position, hitboxSize, 0f, walkingDirection, 0.1f, wallLayer);

            if (hit.collider != null)
            {
                rb.linearVelocity *= -1;
            }
        }
    }

    void EnemyDestroyOnDeath()
    {
        Destroy(this.gameObject);
    }
    void EnemyDying()
    {
        Destroy(rb);
        Destroy(hitbox);
        Destroy(healthbar);
        Destroy(gun);
    }

    IEnumerator EnemyAI()
    {
        while (true)
        {
            enemyAIActions = Random.Range(0, 3);
            if(rb != null && !stunned)
            {
                if (enemyAIActions == 0)
                {
                    rb.linearVelocity = new Vector2(0f, 0f);
                    animator.SetBool("Walking", false);
                }
                else if (enemyAIActions == 1)
                {
                    Vector2 wanderingDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
                    walkingDirection = wanderingDirection;
                    rb.linearVelocity = wanderingDirection * speed;
                    if (rb.linearVelocity != new Vector2(0, 0))
                    {
                        animator.SetBool("Walking", true);
                    }
                }
                else if (enemyAIActions == 2)
                {
                    if (player != null)
                    {
                        Vector2 playerDirection = (player.transform.position - transform.position).normalized;
                        walkingDirection = playerDirection;
                        rb.linearVelocity = playerDirection * speed;
                        animator.SetBool("Walking", true);
                    }
                    else
                    {
                        Vector2 wanderingDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
                        walkingDirection = wanderingDirection;
                        rb.linearVelocity = wanderingDirection * speed;
                        if (rb.linearVelocity != new Vector2(0, 0))
                        {
                            animator.SetBool("Walking", true);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(Random.Range(enemyAIwaitA, enemyAIwaitB));
        }
    }

    private void TakeKB(GameObject damageObject)
    {
        stunned = true;
        animator.SetBool("Walking", false);
        Vector2 direction = transform.position - damageObject.transform.position;
        direction.Normalize();
        direction *= knockback;
        rb.linearVelocity = direction;
        StartCoroutine(StopKB());
        UpdateHealthBar();

        IEnumerator StopKB()
        {
            yield return new WaitForSeconds(knockbackRecover);
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(0f, 0f);
            }
            stunned = false;
        }
    }

    private void UpdateHealthBar()
    {
        if (healthbar != null)
        {
            healthbarSlider.value = health / maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "AOE")
        {
            TakeKB(collision.gameObject);
        }
    }
}
