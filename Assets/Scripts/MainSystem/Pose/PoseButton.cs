using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseButton : MonoBehaviour
{   
    [SerializeField] GameObject Pose;
    [SerializeField] GameObject PoseDisplay;
    [SerializeField] PoseSystem poseSystem;
    [SerializeField] RespawnSystem respawnSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResumeButton()
    {
        poseSystem.Unpause();
    }
    public void RestartButton()
    {
        Time.timeScale = 1;
        respawnSystem.Retry();
    }
    public void BackButton()
    {
        //ステージ選択画面に戻る
    }
}
