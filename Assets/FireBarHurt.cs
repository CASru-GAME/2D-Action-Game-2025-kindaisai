using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FireBarHurt : MonoBehaviour
{
    public int minDamage = 1;
    public float knockbackImpulse = 6f;
    public float upwardBonus = 0.2f;

    void OnCollisionEnter2D(Collision2D c)
    {
        TryHurt(c.collider, -c.GetContact(0).normal);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 dir = ((Vector2)other.transform.position - (Vector2)transform.position).normalized;
        TryHurt(other, dir);
    }

    void TryHurt(Collider2D hitCol, Vector2 hitDir)
    {
        if (!hitCol) return;

        // 拿“玩家主体”（根/刚体），避免子碰撞器找不到组件
        var root = hitCol.attachedRigidbody ? hitCol.attachedRigidbody.gameObject
                                            : hitCol.transform.root.gameObject;

        // （可选）如果你坚持用Tag，就用 root.CompareTag("Player")
        var hp = root.GetComponentInParent<HitPointSystem>();
        if (hp == null || hp.isInvincible) return;

        int damage = Mathf.Max(minDamage, 1);   // 需要固定伤害就用 1；想按比例就用 Mathf.CeilToInt(hp.MaxHP*0.2f)
        hp.AddDamage(damage);                   // 触发：HP 减少 → 你的脚本会无敌 & 闪烁

        // 击退
        var rb = root.GetComponentInParent<Rigidbody2D>();
        if (rb != null)
        {
            if (hitDir.sqrMagnitude < 0.0001f) hitDir = Vector2.up;
            hitDir = (hitDir + Vector2.up * upwardBonus).normalized;

            if (rb.velocity.y < 0f) // 下落中先清掉下落速度，击退更明显
                rb.velocity = new Vector2(rb.velocity.x, 0f);

            rb.AddForce(hitDir * knockbackImpulse, ForceMode2D.Impulse);
        }
    }
}


