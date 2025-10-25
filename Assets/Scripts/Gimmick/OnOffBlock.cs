using UnityEngine;

public class OnOffBlock : MonoBehaviour
{
    private bool isOn = true;
    private SpriteRenderer sr;
    private Collider2D col;

    [Header("ON時のスプライト")]
    public Sprite on;

    [Header("OFF時のスプライト")]
    public Sprite off;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        UpdateBlock();
    }

    public void ToggleBlock()
    {
        isOn = !isOn;
        UpdateBlock();
    }

    private void UpdateBlock()
    {
        sr.sprite = isOn ? on :off;
        col.enabled = isOn; // ONの時だけ当たり判定あり
    }
}
