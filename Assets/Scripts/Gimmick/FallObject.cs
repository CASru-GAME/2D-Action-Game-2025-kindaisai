using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{   
    [SerializeField] float speed;
    bool isfall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isfall)
        transform.position -= new Vector3(0f,speed * Time.deltaTime,0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        PlayerDataStore playerDataStore= collision.gameObject.GetComponent<PlayerDataStore>();
        if(playerDataStore != null)
        isfall = true;
    
    }
}
