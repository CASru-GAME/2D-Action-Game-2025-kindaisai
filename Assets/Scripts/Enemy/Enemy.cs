using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP { get; protected set; }
    public Collider2D EnemyCollider;
    [SerializeField] float BounceForce;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)//死亡処理
        {
            Destroy(gameObject);
        }
    }
    public void SteppedOn(Collider2D collision, int damage)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, math.max(rb.velocity.y, BounceForce));
        collision.GetComponent<PlayerController2D>().isBounce = true;

        AddDamage(damage);
    }
    
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        collision.GetContacts(contacts);

        PlayerDataStore playerDataStore = collision.GetComponent<PlayerDataStore>();
        if(playerDataStore != null)
        {
            foreach (ContactPoint2D p in contacts)
            {
                if (p.point.y < transform.position.y + transform.localScale.y/2.0f + 0.1f)//踏めていなかったらプレイヤーがダメージを受ける
                {
                    collision.GetComponent<HitPointSystem>().AddDamage(1);
                    Physics2D.IgnoreCollision(EnemyCollider, collision, true);
                    playerDataStore.HitPointSystem.AddDamage(1);
                    return;
                }
            }
            SteppedOn(collision, 1);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController2D>() != null)
        Physics2D.IgnoreCollision(EnemyCollider, collision, false);
    }
}
