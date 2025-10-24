using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togeblock : MonoBehaviour
{


    void OntriggerEnter2D(Collider2D collision)
    {
        PlayerLife player = collision.gameObject.GetComponent<PlayerLife>();

        if (player != null)
        {
            player.LoseLife();  // プレイヤーにダメージを与える
        }

    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
