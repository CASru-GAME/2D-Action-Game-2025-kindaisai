using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlinkingObject : MonoBehaviour
{
    [Header("消えるまでの時間（秒）")]
    public float visibleTime = 2f;  // 表示される時間
    [Header("消えている時間（秒）")]
    public float invisibleTime = 2f; // 消えている時間

    private SpriteRenderer rend;
    private TilemapCollider2D col;
    [SerializeField] Tilemap tilemap;

    [SerializeField] TileBase VisibleTile,InvisibleTile;
    
    [SerializeField] bool isVisible;
    [SerializeField] Vector3Int[] Position;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<TilemapCollider2D>();
        //StartCoroutine(TogglePlatform());
        time = visibleTime;

        col.enabled = isVisible;
        foreach(var pos in Position)
        if(isVisible) tilemap.SetTile(pos,VisibleTile);
        else tilemap.SetTile(pos,InvisibleTile);
    }

    void Update()
    {   
        time -= Time.deltaTime;
        if(time <= 0f)
        {
            isVisible = !isVisible;
            time = visibleTime;

            col.enabled = isVisible;

            foreach(var pos in Position)
            if(isVisible) tilemap.SetTile(pos,VisibleTile);
            else tilemap.SetTile(pos,InvisibleTile);
        }
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
