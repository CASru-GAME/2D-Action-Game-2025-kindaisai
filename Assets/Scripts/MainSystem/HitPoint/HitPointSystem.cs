using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointSystem : MonoBehaviour
{   
    public int HP { get; private set; }
    public int MaxHP { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            //残基を減らす
        }
    }

    public void  RecoverHP(int amount)
    {
        HP += amount;
        if (HP > MaxHP)
        HP = MaxHP;
    }
}
