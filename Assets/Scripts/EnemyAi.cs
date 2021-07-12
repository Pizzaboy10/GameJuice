using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Transform target;
    Rigidbody2D rb;
    public float speed = 5;
    public int health = 10;
    Animator anim;
    public SpriteRenderer rend;
    Color startCol;
    float lerpRatio = 0;

    Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        manager = FindObjectOfType<Manager>();

        if (Manager.juiceLevel >= 5)
            health = 3;
        if (Manager.juiceLevel >= 15)
        {
            health = Mathf.RoundToInt(health * 2.5f);
            speed = speed*2;
        }
        startCol = rend.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target, Vector3.right);

        if(Manager.juiceLevel >= 2)
           anim.SetFloat("Speed", rb.velocity.magnitude);

        if (lerpRatio > 0)
            lerpRatio -= Time.deltaTime*10;
        rend.color = Color.Lerp(startCol, Color.white, lerpRatio);
    }

    private void FixedUpdate()
    {
        rb.AddForce((target.position- transform.position).normalized * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (Manager.juiceLevel >= 20)
            {
                lerpRatio = 1;
            }
        }


        if(collision.gameObject.tag == "Respawn")
        {
            health--;
            if(health <= 0)
                Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            health--;
            if (health <= 0)
                Die();
        }
    }


    IEnumerator Corpse()
    {
        yield return new WaitForSeconds(0.1f);
        rend.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        Collider2D[] cols = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in cols)
        {
            col.enabled = false;
        }
        anim.enabled = false;
        rb.angularDrag = 10;
        enabled = false;
    }

    void Die()
    {
        if (Manager.juiceLevel >= 21)
        {
            manager.CallSleep();
        }
        if (Manager.juiceLevel >= 22)
        {
            StartCoroutine("Corpse");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
