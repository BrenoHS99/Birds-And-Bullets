using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private float rotationZ = 0f;
    private float gunAngle = 0f;
    
    public float speed = 5f;
    public GameObject gun;
    public string gunTag;

    public GameObject SplashOnlyForBalloon;
    private GameObject instSplash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gun = GameObject.FindWithTag("Gun");

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        gunAngle = gun.transform.rotation.z;
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        SpecialBehavior(gunTag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController enemyScript = collision.gameObject.GetComponent<EnemyController>();

            enemyScript.health -= 5;
            Instantiate(SplashOnlyForBalloon, this.gameObject.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            Destroy(this.gameObject);
        }
    }

    private void SpecialBehavior(string gunName)
    {
        if (gunName == "Balloon")
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotationZ));
            if (gunAngle < 0.7 && gunAngle > -0.7)
            {
                rotationZ -= 2f;
            }
            else
            {
                rotationZ += 2f;
            }
        }
    }
}