using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float knockbackRecover = 0.5f;
    public float knockback = 1f;

    private Vector2 plrPosition;
    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D hitbox;

    public Transform gunSpawnpoint;

    public GameObject balloon;
    private GameObject instBalloon;

    private float HMov;
    private float VMov;

    public float health = 3f;
    public bool stunned = false;
    public bool dying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitbox = GetComponent<CapsuleCollider2D>();
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


        if (Input.GetKeyDown(KeyCode.R))
        {
            instBalloon = Instantiate(balloon, gunSpawnpoint.transform.position, gunSpawnpoint.transform.rotation);
            instBalloon.transform.parent = gunSpawnpoint.parent;
            instBalloon.transform.name = "Gun";
        }
    }
    void FixedUpdate()
    {
        if (!stunned && rb != null)
        {
            rb.MovePosition(rb.position + plrPosition * speed * Time.fixedDeltaTime);
        }
    }

    void PlayerDestroyOnDeath()
    {
        Destroy(this.gameObject);
    }
    void PlayerDying()
    {
        dying = true;
        Destroy(rb);
        Destroy(hitbox);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !stunned)
        {
            stunned = true;
            health -= 1;
            Vector2 direction = transform.position - collision.gameObject.transform.position;
            direction.Normalize();
            direction *= knockback;
            rb.linearVelocity = direction;
            StartCoroutine(StopKB());

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
    }
}
