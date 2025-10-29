using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerDataStore playerDataStore = collision.gameObject.GetComponent<PlayerDataStore>();
        if (playerDataStore != null)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity += new Vector2(Mathf.Sign(collision.transform.localScale.x) * Mathf.Max(4f,rb.velocity.x),0);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        PlayerDataStore playerDataStore = collision.gameObject.GetComponent<PlayerDataStore>();
        if (playerDataStore != null)
            playerDataStore.PlayerController2D.isOnIce = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        PlayerDataStore playerDataStore = collision.gameObject.GetComponent<PlayerDataStore>();
        if (playerDataStore != null)
        {   
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerDataStore.PlayerController2D.isOnIce = false;
            rb.velocity += new Vector2(Mathf.Sign(collision.transform.localScale.x) * Mathf.Max(4f,rb.velocity.x),0);
        }

            
    }
}
