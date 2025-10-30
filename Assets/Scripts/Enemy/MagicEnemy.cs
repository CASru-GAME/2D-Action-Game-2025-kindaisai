using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEnemy : Enemy
{
    // Start is called before the first frame update
    [SerializeField] float FiringInterval;//発射間隔
    float cur_FiringInterval = 0;
    [SerializeField] float x_Speed;//x軸の発射速度
    [SerializeField] float y_Speed;//y軸の発射速度
    [SerializeField] float x_Acceleration;//x軸の加速度
    [SerializeField] float y_Acceleration;//y軸の加速度
    [SerializeField] float Repulsion;//反発係数
    [SerializeField] int Bounce_num;//バウンド回数
    [SerializeField] GameObject MagicBulletPrefab;
    MagicBullet magicBullet;
    Animator animator;
    [SerializeField] float ypos;
    override protected void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

            cur_FiringInterval += Time.deltaTime;
            if (cur_FiringInterval >= FiringInterval)
            {
                Shot();
                cur_FiringInterval = 0;
            }
        
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void Shot()//魔法弾を発射する
    {
        magicBullet = Instantiate(MagicBulletPrefab).GetComponent<MagicBullet>();
        magicBullet.x_Speed = -x_Speed;
        magicBullet.y_Speed = y_Speed;
        magicBullet.x_Acceleration = -x_Acceleration;
        magicBullet.y_Acceleration = y_Acceleration;
        magicBullet.Repulsion = Repulsion;
        magicBullet.Bounce_num = Bounce_num;
        magicBullet.isPlayer = false;

        if (transform.localScale.x > 0)
            magicBullet.isLeft = false;
        else
            magicBullet.isLeft = true;

        magicBullet.transform.position = new Vector3(transform.position.x + -transform.localScale.x, transform.position.y + transform.localScale.y / 2f + ypos);
    }

}
