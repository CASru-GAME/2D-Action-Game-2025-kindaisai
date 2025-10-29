using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebar_ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        HitPointSystem hitPointSystem = collision.GetComponent<HitPointSystem>();
        if(hitPointSystem != null)
        hitPointSystem.AddDamage(1);
    }
}
