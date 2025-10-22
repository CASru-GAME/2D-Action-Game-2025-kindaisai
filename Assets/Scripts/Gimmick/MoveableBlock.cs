using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBlock : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pull(float velocity,Vector3 position,float size)
    {
        transform.position = new Vector3(position.x + (-size + (velocity > 0 ? -1 : 1) * transform.localScale.x) / 2f + (velocity > 0 ? -0.01f : 0.01f), transform.position.y, 0);
    }
    
    public void Push(float velocity,Vector3 position,float size)
    {
        transform.position = new Vector3(position.x + (size + (velocity > 0 ? 1 : -1) * transform.localScale.x) / 2f + (velocity > 0 ? 0.02f : -0.02f), transform.position.y, 0);
    }
    
}
