using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerControls : MonoBehaviour
{
    public int health = 100;
    public float speed;
    private float shakeValue = 0;
    private Vector2 moveDir, aimDir, mousePos;
    private Camera cam;
    private Rigidbody2D rb;
    public GameObject canvas;
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    [Header("Level 1")]
    public GameObject feetAnim;
    Animator anim;

    [Header("Level 11")]
    public float recoilForce = 0.1f;


    [Header("Level 22")]
    float iframes = 0.5f;
    public float timeSinceDamage = 0;
    public Color baseColor;
    public Color damageColor;
    SpriteRenderer rend;
    float colourLerp;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        anim = feetAnim.GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        if (Manager.juiceLevel >= 2)
            feetAnim.SetActive(true);
        else
            feetAnim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health / 100f;
        healthText.text = health.ToString();

        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        moveDir = new Vector2(xMove, yMove);
        aimDir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(aimDir.x, aimDir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        Debug.DrawLine(transform.position, (Vector2)transform.position - aimDir.normalized * recoilForce, Color.red);
        if (Manager.juiceLevel >= 2)
        {
            Vector3 feetDir = rb.velocity;
            float footangle = Mathf.Atan2(feetDir.x, feetDir.y) * Mathf.Rad2Deg;
            feetAnim.transform.rotation = Quaternion.AngleAxis(footangle, Vector3.back);
            anim.SetFloat("Speed", rb.velocity.magnitude);
        }

        if (shakeValue > 0)
            shakeValue -= 2*Time.deltaTime;
        if (shakeValue > 1)
            shakeValue -= 2*Time.deltaTime; //2 times faster when big explosion.

        if (Input.GetButtonDown("Jump"))
            shakeValue = 5;


        if (colourLerp > 0)
            colourLerp -= Time.deltaTime * 5;
        rend.color = Color.Lerp(baseColor, damageColor, colourLerp);

    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * speed;

        Vector3 desiredPosition = transform.position + Vector3.back * 10;
        if (Manager.juiceLevel >= 18)
        {
            desiredPosition += (Vector3)aimDir.normalized * 1.5f;
            if (Manager.juiceLevel >= 19)
                desiredPosition += Random.onUnitSphere * shakeValue;

        }


        //Apparantly Vector3.SmoothDamp works better but I wanna do this quick.
        Vector3 smoothedPosition = Vector3.Lerp(cam.transform.position, desiredPosition, 0.05f);    

        if (Manager.juiceLevel >= 17)
        {
            cam.transform.position = smoothedPosition;
        }
        else
        {
            cam.transform.position = desiredPosition;
        }

        desiredPosition = cam.transform.position + Vector3.forward;
        smoothedPosition = Vector3.Lerp(canvas.transform.position, desiredPosition, 0.5f);

        if (Manager.juiceLevel >= 19)
        {
            canvas.transform.position = smoothedPosition;
        }
        else
        {
            canvas.transform.position = cam.transform.position + Vector3.forward;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(Manager.juiceLevel >= 23)
            {
                if(timeSinceDamage + iframes < Time.time)
                {
                    timeSinceDamage = Time.time;
                    health -= 10;
                    colourLerp = 2.5f;
                }
            }
            else
            {
                health -= 10;
            }

            if (health <= 0)
               StartCoroutine("Die");
        }
    }

    public void Recoil()
    {
        Vector2 recoilSpot = new Vector2();
        recoilSpot = (Vector2)transform.position - aimDir.normalized * recoilForce;
        
        transform.position = recoilSpot;
        if(shakeValue < 1)
            shakeValue += 1;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.black;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }
}
