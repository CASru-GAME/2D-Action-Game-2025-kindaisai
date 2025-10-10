using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseSystem : MonoBehaviour
{
    bool isPosing = false;
    [SerializeField] GameObject Pose;
    [SerializeField] GameObject PoseDisplay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPosing)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        isPosing = true;
        Time.timeScale = 0;
        Pose.SetActive(true);
        PoseDisplay.SetActive(true);
    }
    public void Unpause()
    {
        isPosing = false;
        Time.timeScale = 1;
        Pose.SetActive(false);
        PoseDisplay.SetActive(false);
    }
}
