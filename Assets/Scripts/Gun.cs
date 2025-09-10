using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    public GameObject gun;
    public GameObject bullet;
    public Transform bulletSpawnpoint;
    public float cooldown = 0.5f;

    public string gunName;

    private GameObject bulletInst;

    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;

    private float gunLocalScaleY;

    private bool notCD = true;

    private SpriteRenderer spriteRenderer;

    // Update is called once per frame
    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
        SpecialBehavior();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunLocalScaleY = gun.transform.localScale.y;
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(gun.transform.localScale.x, gunLocalScaleY, gun.transform.localScale.z);
        if (angle > 90 || angle < -90)
        {
            localScale.y = gunLocalScaleY * -1;
        }
        else
        {
            localScale.y = gunLocalScaleY;
        }

        gun.transform.localScale = localScale;
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && notCD)
        {
            notCD = false;
            StartCoroutine(CD());
            bulletInst = Instantiate(bullet, bulletSpawnpoint.transform.position, gun.transform.rotation);
        }
    }

    private void SpecialBehavior()
    {
        if (gunName == "Balloon")
        {
            if (notCD)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
    }

    IEnumerator CD()
    {
        yield return new WaitForSeconds(cooldown);
        notCD = true;
    }
}
