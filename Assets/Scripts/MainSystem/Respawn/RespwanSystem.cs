using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 RespawnPoint = new Vector3(0f, 0f, 0f);
    [SerializeField] GameObject PlayerPrefab;
    public GameObject Player;
    string SceneName;
    void Start()
    {
        Player = Instantiate(PlayerPrefab, RespawnPoint, Quaternion.identity);
        GetComponent<StageClearSystem>().playerDataStore = Player.GetComponent<PlayerDataStore>();
        SceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene(SceneName);
    }
    public void Retry()//リトライ(残基が残っていて死んだときに実行する)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ChangeRespawnPoint(Vector3 respawnPoint)
    {
        RespawnPoint = respawnPoint;
    }
}
