using UnityEngine;

public class OnOffSwitch : MonoBehaviour
{
    [Header("制御するブロック")]
    public OnOffBlock[] targetBlocks;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.CompareTag("Player")) // プレイヤーが触れたら
        {
            foreach (var block in targetBlocks)
            {
                block.ToggleBlock();
            }
        }
    }
}
