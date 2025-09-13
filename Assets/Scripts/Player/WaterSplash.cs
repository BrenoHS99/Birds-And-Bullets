using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WaterSplash : MonoBehaviour
{
    public float damage = 5f;

    private GameObject sfx;
    public GameObject splashSound;
    
    private List<GameObject> enemiesHitted = new List<GameObject>();

    private void Start()
    {
        sfx = GameObject.FindWithTag("Sfx");
        TempSplashSound();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !enemiesHitted.Contains(collision.gameObject))
        {
            EnemyController enemyScript = collision.gameObject.GetComponent<EnemyController>();
            enemyScript.health -= damage;
        }
    }
    private void TempSplashSound()
    {
        GameObject splashSoundInst;
        splashSoundInst = Instantiate(splashSound, transform.position, transform.rotation);
        splashSoundInst.transform.parent = sfx.transform;
        AudioSource splashSoundSource = splashSoundInst.GetComponent<AudioSource>();
        splashSoundSource.Play();
        Destroy(splashSoundInst, splashSoundSource.clip.length);
    }
    public void AnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
