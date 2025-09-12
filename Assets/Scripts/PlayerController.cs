using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 plrPosition;
    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D hitbox;
    private SpriteRenderer sprite;

    public Transform gunSpawnpoint;

    public GameObject balloon;
    private GameObject instBalloon;

    public GameObject healthbar;
    private Slider healthbarSlider;
    public GameObject healthbarFill;
    private Image healthbarFillImage;

    private float HMov;
    private float VMov;

    public float health = 10f;
    public float maxHealth = 10f;
    public bool stunned = false;
    public bool dying = false;

    public GameObject gameController;
    private GameController gameControllerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitbox = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        gameControllerScript = gameController.GetComponent<GameController>();
        healthbarSlider = healthbar.GetComponent<Slider>();
        healthbarFillImage = healthbarFill.GetComponent<Image>();
        health = maxHealth;
        GiveBalloon();
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        HMov = Input.GetAxisRaw("Horizontal");
        VMov = Input.GetAxisRaw("Vertical");

        if (!stunned || !dying)
        {
            plrPosition = new Vector2(HMov, VMov);
        }

        if (HMov == 0 && VMov == 0)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
            if (VMov == 1)
            {
                animator.SetBool("Facing", false);
            }
            else
            {
                animator.SetBool("Facing", true);
            }
        }

        animator.SetFloat("Health", health);
    }
    void FixedUpdate()
    {
        if (!stunned && rb != null)
        {
            rb.MovePosition(rb.position + plrPosition * speed * Time.fixedDeltaTime);
        }
    }

    void RestartSceneOnDeath()
    {
        sprite.enabled = false;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    void PlayerDying()
    {
        dying = true;
        Destroy(instBalloon);
        Destroy(rb);
        Destroy(hitbox);
    }

    private void GiveBalloon()
    {
        instBalloon = Instantiate(balloon, gunSpawnpoint.transform.position, gunSpawnpoint.transform.rotation);
        instBalloon.transform.parent = this.gameObject.transform;
        instBalloon.transform.name = "Gun";
    }

    public void TakeDamage(GameObject damageObject, float damage, float knockback, float knockbackRecover)
    {
        stunned = true;
        health -= damage;
        Vector2 direction = transform.position - damageObject.gameObject.transform.position;
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
        if (health <= 0)
        {
            healthbarFillImage.color = Color.black;
        }
        healthbarSlider.value = health / maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !stunned)
        {
            TakeDamage(collision.gameObject, 3f, 10f, 0.2f);
        }
        if (collision.gameObject.tag == "Boss" && !stunned)
        {
            TakeDamage(collision.gameObject, 3f, 15f, 0.3f);
        }
    }
}
