using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{   
    [SerializeField] Sprite blackHeart;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlackHeart()
    {
        GetComponent<Image>().sprite = blackHeart;
    }
}
