using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEnemy : Enemy
{
    // Start is called before the first frame update
    [SerializeField] float FiringInterval;//発射間隔
    float cur_FiringInterval = 0;
    [SerializeField] float FiringSpeed;//発射速度
    [SerializeField] GameObject MagicBulletPrefab;
    MagicBullet magicBullet;
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (isInsideCamera)
        {
            cur_FiringInterval += Time.deltaTime;
            if (cur_FiringInterval >= FiringInterval)
            {
                Shot();
                cur_FiringInterval = 0;
            }
        }
    }

    void Shot()//魔法弾を発射する
    {
        magicBullet = Instantiate(MagicBulletPrefab).GetComponent<MagicBullet>();
        magicBullet.Speed = FiringSpeed;

        if (transform.localScale.x == 1)
            magicBullet.isLeft = false;
        else
            magicBullet.isLeft = true;

        magicBullet.transform.position = new Vector3(transform.position.x + transform.localScale.x, transform.position.y + transform.localScale.y / 2f);
    }

}
