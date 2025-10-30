using UnityEngine;
using UnityEngine.Tilemaps;

public class OnOffSwitch : MonoBehaviour
{
    [Header("制御するブロック")]
    public OnOffBlock[] targetBlocks;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileBase On,Off;
    [SerializeField] Vector3Int[] Position;
    [SerializeField] bool isOn = true;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.CompareTag("above") && collision.transform.parent.GetComponent<Rigidbody2D>().velocity.y > 0f) // プレイヤーが触れたら
        {
            foreach (var block in targetBlocks)
            {
                block.ToggleBlock();
            }

            isOn = !isOn;
            foreach(var pos in Position)
            if(isOn) tilemap.SetTile(pos,On);
            else tilemap.SetTile(pos,Off);
        }
    }
}
