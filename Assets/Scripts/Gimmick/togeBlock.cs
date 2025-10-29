using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togeBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        HitPointSystem hitPointSystem = collision.gameObject.GetComponent<HitPointSystem>();
        if(hitPointSystem != null)
        hitPointSystem.AddDamage(1);
    }
}
