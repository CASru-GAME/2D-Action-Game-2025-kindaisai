using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WalkEnemy : Enemy
{
    // Start is called before the first frame update
    bool isInsideCamera;
    bool isLeft = true;
    private float moveSpeed = 2.5f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HP = 10;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        if (isInsideCamera)
        {
            if (isLeft)
            {
                rb.velocity = new Vector2(-1 * moveSpeed, 0f);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, 0f);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        
        if (collision.GetComponent<PlayerDataStore>() == null)
        if (isLeft) isLeft = false;
        else isLeft = true;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        if (LayerMask.LayerToName(layer) == "Ground")
        {
            if (isLeft)
            {
                if (transform.position.x + transform.localScale.x / 2f <= collision.transform.position.x - collision.transform.localScale.x / 2f)
                    isLeft = false;
            }
            else
            {
                if (transform.position.x + transform.localScale.x / 2f >= collision.transform.position.x + collision.transform.localScale.x / 2f)
                    isLeft = true; 
            }
        }
    }   
    void OnBecameVisible()
    {
        isInsideCamera = true;
    }

    void OnBecameInvisible()
    {
        isInsideCamera = false;
    }
}
