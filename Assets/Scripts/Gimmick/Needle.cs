using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Needle : MonoBehaviour
{   
    [SerializeField] bool isFall;
    [SerializeField] bool isFalling;
    [SerializeField] float time;
    [SerializeField] float speed;
    [SerializeField] Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GameObject.Find("player(Clone)").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFall && Mathf.Abs(tr.position.x - transform.position.x) < 1.2f)
        isFalling = true;

        if(isFalling)
        {   
            time -= Time.deltaTime;
            if(time < 0)
            transform.position -= new Vector3(0f,speed * Time.deltaTime,0f);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        HitPointSystem hitPointSystem = collision.GetComponent<HitPointSystem>();
        if(hitPointSystem != null)
        hitPointSystem.AddDamage(1);

        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        Destroy(gameObject);
    }
}
