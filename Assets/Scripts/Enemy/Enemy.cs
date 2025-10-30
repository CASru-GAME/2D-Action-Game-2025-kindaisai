using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP { get; protected set; }
    [SerializeField] int initialHP;
    protected bool isInsideCamera;
    public Collider2D EnemyCollider;
    [SerializeField] float BounceForce;
    public bool isInvincible;
    float cur_InvincibleTime;//残りの無敵時間
    [SerializeField] float InvincibleTime;//無敵時間
    [SerializeField] bool isStepping;//踏めるか敵か
    // Start is called before the first frame update
    virtual protected void Start()
    {
        HP = initialHP;
    }

    // Update is called once per frame
    virtual protected void Update()
    {   
        if(isInvincible)
        {
            cur_InvincibleTime -= Time.deltaTime;
            
            if (cur_InvincibleTime <= 0)
            isInvincible = false;
        }
    }

    public void AddDamage(int damage)
    {   Debug.Log(HP);
        isInvincible = true;
        cur_InvincibleTime = InvincibleTime;
        HP -= damage;
        Debug.Log(HP);
        if (HP <= 0)//死亡処理
        {
            Destroy(gameObject);
        }
    }
    public void SteppedOn(Collider2D collision, int damage)
    {   
        if(!isInvincible)
        {   
            isInvincible = true;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, math.max(rb.velocity.y, BounceForce));
            collision.GetComponent<PlayerController2D>().isBounce = true;

            cur_InvincibleTime = InvincibleTime;
            PlayerController2D playerController2D = collision.GetComponent<PlayerController2D>();
            playerController2D.audioSource.PlayOneShot(playerController2D.steped_sound);
            AddDamage(damage);
        }
    }
    
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        collision.GetContacts(contacts);

        PlayerDataStore playerDataStore = collision.GetComponent<PlayerDataStore>();
        if(playerDataStore != null)
        {   
                float ypos = transform.position.y + 0.6f;
                
                if (collision.gameObject.transform.position.y < ypos)//踏めていなかったらプレイヤーがダメージを受ける
                {  
                    playerDataStore.HitPointSystem.AddDamage(1);
                    return;
                }
                else if (isStepping)
                SteppedOn(collision, 1);
                else
                playerDataStore.HitPointSystem.AddDamage(1);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>() != null)
            Physics2D.IgnoreCollision(EnemyCollider, collision, false);
    }
    
    void OnBecameVisible()
    {
        isInsideCamera = true;
    }

    void OnBecameInvisible()
    {
        isInsideCamera = false;
    }
}
