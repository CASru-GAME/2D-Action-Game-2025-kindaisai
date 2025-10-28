using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{   
    [Header("移動経路")]public Vector2[] movePoint;
    Vector3 moveVector;
    [Header("速さ")]public float speed = 1.0f;
    int cur_point = 0,last_point;
    // Start is called before the first frame update
    void Start()
    {
        last_point = movePoint.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = movePoint[(cur_point+1 > last_point) ? 0 : cur_point+1] - movePoint[cur_point];
        Debug.Log(moveVector.normalized);
        transform.position += moveVector.normalized * speed *Time.deltaTime;
        if(Mathf.Abs(transform.position.y-movePoint[(cur_point+1 > last_point) ? 0 : cur_point+1].y) < 0.1f && Mathf.Abs(transform.position.x-movePoint[(cur_point+1 > last_point) ? 0 : cur_point+1].x) < 0.1f)
        {
            cur_point++;
            if(cur_point > last_point)
            cur_point = 0;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        PlayerDataStore playerDataStore= collision.gameObject.GetComponent<PlayerDataStore>();
        if(playerDataStore != null)
        collision.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D collision)
    {   
        PlayerDataStore playerDataStore= collision.gameObject.GetComponent<PlayerDataStore>();
        if(playerDataStore != null)
        collision.transform.SetParent(null);
    }
}
