using UnityEngine;

public class OnOffBlock : MonoBehaviour
{
    private bool isOn = true;
    private SpriteRenderer sr;
    private Collider2D col;

    [Header("ON時の色")]
    public Color onColor = Color.blue;

    [Header("OFF時の色")]
    public Color offColor = Color.red;

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
        sr.color = isOn ? onColor : offColor;
        col.enabled = isOn; // ONの時だけ当たり判定あり
    }
}
