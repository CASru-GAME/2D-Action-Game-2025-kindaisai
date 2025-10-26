using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pull(float velocity,Vector3 position,float size)
    {   rb.velocity = new Vector2(velocity,rb.velocity.y);
        //transform.position = new Vector3(position.x + (-size - (velocity > 0 ? 1 : -1) * transform.localScale.x) / 2.08f / 2f + (velocity > 0 ? 0.02f : -0.02f), transform.position.y, 0);
    }
    
    public void Push(float velocity,Vector3 position,float size)
    {   Debug.Log("push");
        rb.velocity = new Vector2(velocity,rb.velocity.y);
        //transform.position = transform.position + new Vector3(Mathf.Sign(velocity) * 0.01f,0f,0f);
        //transform.position = new Vector3(position.x + (size + (velocity > 0 ? 1 : -1) * transform.localScale.x) / 2.08f / 2f + (velocity > 0 ? 0.02f : -0.02f), transform.position.y, 0);
    }
    
}
