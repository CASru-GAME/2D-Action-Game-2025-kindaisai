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
    static bool isRetry = false;
    [SerializeField] GameObject LoadDisplay;
    void Start()
    {
        SceneName = SceneManager.GetActiveScene().name;
        
        if(!isRetry)
        {
            Player = Instantiate(PlayerPrefab, RespawnPoint, Quaternion.identity);
        }
        else
        {
            LoadDisplay.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<TimeSystem>().enabled = false;
            Invoke(new Action(() => {
                LoadDisplay.GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<TimeSystem>().enabled = true;
                Player = Instantiate(PlayerPrefab, RespawnPoint, Quaternion.identity);
            }).Method.Name, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Retry();
    }
    public void Retry()//リトライ(残基が残っていて死んだときに実行する)
    {
        isRetry = true;
        SceneManager.LoadScene(SceneName);
    }

    public void ChangeRespawnPoint(Vector3 respawnPoint)
    {
        RespawnPoint = respawnPoint;
    }
}
