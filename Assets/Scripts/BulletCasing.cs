using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCasing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector3 rand = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 0.3f;

        rb.velocity = (rb.transform.right+rand) *Random.Range(5, 15);
        rb.AddTorque(Random.Range(-10, 10));
        if(Manager.juiceLevel < 22)
            Destroy(gameObject, 5);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
