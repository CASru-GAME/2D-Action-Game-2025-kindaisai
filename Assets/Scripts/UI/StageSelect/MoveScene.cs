using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField] private string _loadScene; //シーン名を記述

    public void change_button()
    {
        SceneManager.LoadScene(_loadScene);
    }
}
