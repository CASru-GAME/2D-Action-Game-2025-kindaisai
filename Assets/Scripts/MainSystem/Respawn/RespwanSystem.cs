using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 RespawnPoint = new Vector3(0f, 0f, 0f);
    [SerializeField] GameObject PlayerPrefab;
    GameObject Player;
    string SceneName;
    [SerializeField] GameObject LoadDisplay;
    void Start()
    {
        SceneName = SceneManager.GetActiveScene().name;
        Player = Instantiate(PlayerPrefab, RespawnPoint, Quaternion.identity);
        Player.GetComponent<PlayerLife>().respawnSystem = GetComponent<RespawnSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        Retry();
    }
    public void Retry()//リトライ(残基が残っていて死んだときに実行する)
    {   
        LoadDisplay.GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<TimeSystem>().enabled = false;
        Invoke(new Action(() =>
        {
            SceneManager.LoadScene(SceneName);
        }).Method.Name, 2f);
    }

    public void ChangeRespawnPoint(Vector3 respawnPoint)
    {
        RespawnPoint = respawnPoint;
    }
}
