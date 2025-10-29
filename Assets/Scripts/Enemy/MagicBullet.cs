using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float x_Speed;
    public float x_Acceleration;
    public float y_Speed;
    public float y_Acceleration;
    public float Repulsion;//反発係数
    public int Bounce_num;//バウンド回数
    public bool isPlayer;
    
    public bool isLeft;
    Rigidbody2D rb;
    bool isInsideCamera;
    [SerializeField] float DeathTime;//画面に映っていないと弾が消える時間
    float cur_DeathTime;

    [SerializeField] private LayerMask groundLayer;// 地面レイヤー
    bool isBouncing;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cur_DeathTime = DeathTime;
        if (isLeft)
            rb.velocity = new Vector2(-1 * x_Speed, y_Speed);
        else
            rb.velocity = new Vector2(x_Speed, y_Speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInsideCamera)
        {
            cur_DeathTime -= Time.deltaTime;
            if (cur_DeathTime <= 0)
                Destroy(gameObject);
        }
        rb.velocity += new Vector2(x_Acceleration * Time.deltaTime,y_Acceleration * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDataStore playerDataStore = collision.GetComponent<PlayerDataStore>();
        Enemy enemy = collision.GetComponent<Enemy>();
        if (!isPlayer && playerDataStore != null && !playerDataStore.HitPointSystem.isInvincible)
        {
            playerDataStore.HitPointSystem.AddDamage(1);
            Destroy(gameObject);
        }
        else if(isPlayer && enemy != null && !enemy.isInvincible)
        {
            enemy.AddDamage(1);
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {   
            float y_bullet = transform.position.y - transform.localScale.y / 2f;
            float y_floor = collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y / 2f;
            if(Bounce_num > 0)
            {   
                if(!isBouncing)
                { 
                    rb.velocity = new Vector2(rb.velocity.x,-rb.velocity.y * Repulsion);
                    isBouncing = true;
                    Bounce_num--;
                }
            }
            else
            Destroy(gameObject);
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        isBouncing = false;
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
