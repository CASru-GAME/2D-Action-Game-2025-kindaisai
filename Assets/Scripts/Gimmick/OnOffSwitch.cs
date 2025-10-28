using UnityEngine;

public class OnOffSwitch : MonoBehaviour
{
    [Header("制御するブロック")]
    public OnOffBlock[] targetBlocks;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.CompareTag("above") && transform.position.y - 0.5f >= collision.transform.position.y + 0.4f) // プレイヤーが触れたら
        {
            foreach (var block in targetBlocks)
            {
                block.ToggleBlock();
            }
        }
    }
}
