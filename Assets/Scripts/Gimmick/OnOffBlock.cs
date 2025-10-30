using UnityEngine;
using UnityEngine.Tilemaps;

public class OnOffBlock : MonoBehaviour
{
    [SerializeField] private bool isOn;
    private TilemapCollider2D col;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase On,Off;
    [SerializeField] Vector3Int[] Position;

    void Awake()
    {
        col = GetComponent<TilemapCollider2D>();
        UpdateBlock();

        foreach(var pos in Position)
        if(isOn) tilemap.SetTile(pos,On);
        else tilemap.SetTile(pos,Off);
    }

    public void ToggleBlock()
    {
        isOn = !isOn;
        UpdateBlock();
    }

    private void UpdateBlock()
    {
        col.enabled = isOn; // ONの時だけ当たり判定あり
        foreach(var pos in Position)
        if(isOn) tilemap.SetTile(pos,On);
        else tilemap.SetTile(pos,Off);
    }
}
