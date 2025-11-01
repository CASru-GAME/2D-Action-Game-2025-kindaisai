using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StageClear : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject GameClear_Text;
    [SerializeField] GameObject player;
    PlayerController2D playerController2D;
    Rigidbody2D rb; 
    Animator animator;
    bool isGoal;
    void Start()
    {
        player = GameObject.Find("player(Clone)");
        playerController2D = player.GetComponent<PlayerController2D>();
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGoal) GameClear();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDataStore playerDataStore = collision.GetComponent<PlayerDataStore>();
        if(playerDataStore != null) isGoal = true;
    }

    void GameClear()
    {
        GameClear_Text.SetActive(true);
        playerController2D.enabled = false;
        rb.velocity = new Vector2(0f,rb.velocity.y);
        animator.speed = 0f;
;       StartCoroutine(DelayedSceneLoad());
    }

    IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("StageSelect");

    }   
}
