using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerDataStore playerDataStore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
            StageClear();
    }

    public void StageClear()
    {
        playerDataStore.PlayerController2D.enabled = false;
    }
}
