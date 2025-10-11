using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointSystem : MonoBehaviour
{   
    public int HP { get; private set; }
    public int MaxHP { get; private set; }
    bool isInvincible;
    [SerializeField] float InvincibleTime;//無敵時間
    float cur_InvincibleTime;//残りの無敵時間
    [SerializeField] float BlinkingCycle;//ダメージを食らったときの点滅周期
    float cur_Blinking;//点滅時間
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {   
        sr = GetComponent<SpriteRenderer>();
        MaxHP = 3;
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInvincible)
        {   
            cur_Blinking -= Time.deltaTime;
            cur_InvincibleTime -= Time.deltaTime;

            if (cur_Blinking <= 0)
            {
                if (sr.enabled == true)
                    sr.enabled = false;
                else
                    sr.enabled = true;

                cur_Blinking = BlinkingCycle;
            }

            if (cur_InvincibleTime <= 0)
            {
                sr.enabled = true;
                isInvincible = false;
            }
        }
    }

    public void AddDamage(int damage)//ダメージを与える
    {   
        if(!isInvincible)
        {   
            HP -= damage;
            isInvincible = true;
            cur_InvincibleTime = InvincibleTime;
            cur_Blinking = BlinkingCycle;
            if (HP <= 0)
            {
                //残基を減らす
            }
            Debug.Log(HP);
        }
    }

    public void  RecoverHP(int amount)//体力を回復
    {
        HP += amount;
        if (HP > MaxHP)
        HP = MaxHP;
    }
}
