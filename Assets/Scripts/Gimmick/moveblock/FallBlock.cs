using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
    [Header("落下までの待ち時間")]
    public float fallDelay = 0.5f;

    [Header("床が戻るまでの時間")]
    public float resetDelay = 2f;

    private Rigidbody2D rb;
    private Vector3 startPos; 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        startPos = transform.position;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }

        IEnumerator Fall()
        {
            yield return new WaitForSeconds(fallDelay);
            rb.bodyType = RigidbodyType2D.Dynamic; // 落下開始   
            yield return new WaitForSeconds(resetDelay);
            rb.bodyType = RigidbodyType2D.Kinematic; // 固定に戻す
            rb.velocity = Vector2.zero; // 落下速度をリセット
            transform.position = startPos; // 初期位置に戻す
        }
    }

}
