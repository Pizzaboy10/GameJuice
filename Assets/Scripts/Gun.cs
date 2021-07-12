using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet, bigBullet, casing;
    float speed = 3f;
    public float fireRate = 0.5f;
    float lastBulletFired;
    SpriteRenderer rend;
    AudioSource audioSource;
    public AudioClip bulletSound, laserSound;

    public PlayerControls player;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
        player = GetComponentInParent<PlayerControls>();

        if (Manager.juiceLevel >= 4)
            audioSource.pitch = 0.5f;
        if (Manager.juiceLevel >= 6)
            fireRate = 0.1f;
        if (Manager.juiceLevel >= 7)
            bulletSound = laserSound;
        if (Manager.juiceLevel >= 8)
            speed = 20;
        if (Manager.juiceLevel >= 9)
            audioSource.pitch = 0.35f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time > lastBulletFired + fireRate)
            {

                SpawnBullet();
                if (Manager.juiceLevel >= 15)
                {
                    Multibullet();
                    Multibullet();
                }
            }
        }
    }

    void SpawnBullet()
    {

        GameObject newBullet;

        if (Manager.juiceLevel >= 7)
            newBullet = Instantiate(bigBullet, transform.position, transform.rotation);
        else
            newBullet = Instantiate(bullet, transform.position, transform.rotation);

        if (Manager.juiceLevel >= 10)
            newBullet.transform.Rotate(Vector3.forward, Random.Range(-5, 5));

        newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.up * speed;
        lastBulletFired = Time.time;
        if (Manager.juiceLevel >= 3)
            audioSource.PlayOneShot(bulletSound);
        if (Manager.juiceLevel >= 9)
            StartCoroutine("Flash");
        if (Manager.juiceLevel >= 11)
            player.Recoil();
        if (Manager.juiceLevel >= 13)
        {
            GameObject newCase = Instantiate(casing, transform.position, transform.rotation);
        }
    }
    void Multibullet()
    {
        GameObject newBullet;

        if (Manager.juiceLevel >= 7)
            newBullet = Instantiate(bigBullet, transform.position, transform.rotation);
        else
            newBullet = Instantiate(bullet, transform.position, transform.rotation);

        if (Manager.juiceLevel >= 10)
            newBullet.transform.Rotate(Vector3.forward, Random.Range(-15, 15));

        newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.up * speed;
        lastBulletFired = Time.time;
    }

    IEnumerator Flash()
    {
        rend.enabled = true;
        yield return new WaitForSeconds(0.03f);
        rend.enabled = false;
    }
}
