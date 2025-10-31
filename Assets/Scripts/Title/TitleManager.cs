using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
