using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    public Button[] stageButtons;

    void Start()
    {
        // 各ボタンにクリックイベントを登録
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageIndex = i + 1; // ステージ番号（1始まり）
            stageButtons[i].onClick.AddListener(() => OnStageButtonClicked(stageIndex));
        }
    }

    void OnStageButtonClicked(int stageNumber)
    {
        Debug.Log($"ステージ{stageNumber}を選択しました！");
        SceneManager.LoadScene("Stage" + stageNumber);
    }
}