using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 RespawnPoint = new Vector3(0f, 0f, 0f);
    [SerializeField] GameObject PlayerPrefab;
    GameObject Player;
    void Start()
    {   
        Player = Instantiate(PlayerPrefab, RespawnPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene("tomi");
    }

    public void ChangeRespawnPoint(Vector3 respawnPoint)
    {
        RespawnPoint = respawnPoint;
    }
}
