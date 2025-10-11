using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSelect : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("select");
    }
}
