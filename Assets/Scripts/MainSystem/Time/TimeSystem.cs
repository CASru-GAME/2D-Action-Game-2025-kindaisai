using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float MaxTime;
    [SerializeField] Text TimeText;
    float Cur_Time;
    void Start()
    {
        Cur_Time = MaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        int time = (int)Cur_Time;
        if (time >= 100) TimeText.text = "Time\n" + time.ToString();
        else if (time >= 10) TimeText.text = "Time\n0" + time.ToString();
        else TimeText.text = "Time\n00" + time.ToString();
        

        Cur_Time -= Time.deltaTime;

        if(Cur_Time <= 0f)
        {
            //残基を減らす
        }
    }
}
