using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Sprite muzzleFlash;
    public Sprite baseBullet;
    public SpriteRenderer rend;
    public GameObject impact;

    // Start is called before the first frame update
    void Start()
    {
        if (Manager.juiceLevel >= 12)
            GetComponent<Collider2D>().isTrigger = false;
        else
            GetComponent<Collider2D>().isTrigger = true;
        Destroy(gameObject, 5);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (Manager.juiceLevel >= 14)
        {
            GameObject ImpactEffect = Instantiate(impact, transform.position, transform.rotation);
            Destroy(ImpactEffect, 0.1f);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Manager.juiceLevel >= 14)
        {
            GameObject ImpactEffect = Instantiate(impact, collision.GetContact(0).point, transform.rotation);
            Destroy(ImpactEffect, 0.03f);
        }
        Destroy(gameObject);

    }

}
