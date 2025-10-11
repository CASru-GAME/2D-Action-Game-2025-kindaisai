using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int Lives { get; private set; }
    public RespawnSystem respawnSystem;

    void Start()
    {
        Lives = 5;
    }

    void Update()
    {
        if (transform.position.y < -10)
        LoseLife();
    }
    // 残機を減らす
    public void LoseLife()
    {
        if (Lives > 0)
        {
            Lives--;
            respawnSystem.Retry();
        }
        else
        {
            GameOver();
        }
    }

    // ゲームオーバー処理
    public void GameOver()
    {
        
    }
}