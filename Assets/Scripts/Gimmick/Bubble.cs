using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{   
    [SerializeField] float speed;
    [SerializeField] float Max_y,Min_y;
    [SerializeField] float wait_time;
    float cur_time;
    // Start is called before the first frame update
    void Start()
    {
        cur_time = wait_time;
    }

    // Update is called once per frame
    void Update()
    {   
        if(cur_time < 0f)
        transform.position += new Vector3(0f,speed * transform.localScale.y * Time.deltaTime,0f); 
        else
        transform.position += new Vector3(0f,speed * transform.localScale.y * Time.deltaTime / 5f,0f); 

        if(transform.position.y > Max_y && transform.localScale.y == 1)
        {
            transform.localScale = new Vector3(1,-1,1);
            cur_time = wait_time;
        }

        if(transform.position.y < Min_y && transform.localScale.y == -1)
        transform.localScale = new Vector3(1,1,1);

        cur_time -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDataStore playerDataStore = collision.GetComponent<PlayerDataStore>();
        if(playerDataStore != null)
        playerDataStore.HitPointSystem.AddDamage(1);
    }
}
