using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public bool isLeft;
    Rigidbody2D rb;
    bool isInsideCamera;
    [SerializeField] float DeathTime;//画面に映っていないと弾が消える時間
    float cur_DeathTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cur_DeathTime = DeathTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeft)
            rb.velocity = new Vector2(-1 * Speed, 0);
        else
            rb.velocity = new Vector2(Speed, 0);

        if(!isInsideCamera)
        {
            cur_DeathTime -= Time.deltaTime;
            if (cur_DeathTime <= 0)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDataStore playerDataStore = collision.GetComponent<PlayerDataStore>();
        if (playerDataStore != null)
        {
            playerDataStore.HitPointSystem.AddDamage(1);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);

    }
    
    void OnBecameVisible()
    {
        isInsideCamera = true;
        cur_DeathTime = DeathTime;
    }

    void OnBecameInvisible()
    {
        isInsideCamera = false;
    }
}
