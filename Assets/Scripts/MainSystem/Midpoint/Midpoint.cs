using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Midpoint : MonoBehaviour
{
    static bool isGet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {   
        PlayerDataStore playerDataStore = collision.gameObject.GetComponent<PlayerDataStore>();
        if (playerDataStore != null && !isGet)
        {
            isGet = true;
            RespawnSystem.RespawnPoint = transform.position;
        }
    }
}
