using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingObject : MonoBehaviour
{
    [Header("消えるまでの時間（秒）")]
    public float visibleTime = 2f;  // 表示される時間
    [Header("消えている時間（秒）")]
    public float invisibleTime = 2f; // 消えている時間

    private Renderer rend;
    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();
        StartCoroutine(TogglePlatform());
    }

    System.Collections.IEnumerator TogglePlatform()
    {
        while (true)
        {
            // 表示状態
            rend.enabled = true;
            col.enabled = true;
            yield return new WaitForSeconds(visibleTime);

            // 非表示状態
            rend.enabled = false;
            col.enabled = false;
            yield return new WaitForSeconds(invisibleTime);
        }
    }
}
