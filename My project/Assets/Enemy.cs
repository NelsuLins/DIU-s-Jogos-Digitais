using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public float speed;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;

    private bool colliding;

    public LayerMask layer;

    public CircleCollider2D circleCollider2D;
    public BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position,leftCol.position,layer);

        if(colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            float height = other.transform.position.y - headPoint.position.y;
            bool playerDie = false;

            if (height > 0 && !playerDie)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                speed = 0;
                anim.SetTrigger("die");
                rig.bodyType = RigidbodyType2D.Kinematic;
                circleCollider2D.enabled = false;
                boxCollider2D.enabled = false;
                Destroy(gameObject, 0.5f);
            }
            else
            {
                GameController.instance.ShowGameOver();
                playerDie = true;
                speed = 0;
                other.gameObject.GetComponent<Animator>().SetTrigger("Die");
                other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Destroy(other.gameObject, 0.5f);
            }
        }
        
    }
}
