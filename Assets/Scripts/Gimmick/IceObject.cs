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
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDataStore playerDataStore = collision.gameObject.GetComponent<PlayerDataStore>();
        if (playerDataStore != null)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity += new Vector2(Mathf.Max(0.4f,rb.velocity.x/4f),0);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        PlayerDataStore playerDataStore = collision.gameObject.GetComponent<PlayerDataStore>();
        if (playerDataStore != null)
            playerDataStore.PlayerController2D.isOnIce = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        PlayerDataStore playerDataStore = collision.gameObject.GetComponent<PlayerDataStore>();
        if (playerDataStore != null)
            playerDataStore.PlayerController2D.isOnIce = false;
    }
}
