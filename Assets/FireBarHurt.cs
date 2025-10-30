using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FireBarHurt : MonoBehaviour
{
    public int minDamage = 1;
    public float knockbackImpulse = 6f;
    public float upwardBonus = 0.2f;

    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log($"[FireBarHurt] Collision with {c.collider.name}");
        TryHurt(c.collider, -c.GetContact(0).normal);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[FireBarHurt] Trigger with {other.name}");
        Vector2 dir = ((Vector2)other.transform.position - (Vector2)transform.position).normalized;
        TryHurt(other, dir);
    }

    void TryHurt(Collider2D hitCol, Vector2 hitDir)
    {
        if (!hitCol) return;

        
        var host = hitCol.attachedRigidbody ? hitCol.attachedRigidbody.gameObject
                                            : hitCol.transform.root.gameObject;

        var hp = host.GetComponentInParent<HitPointSystem>();
        if (hp == null) { Debug.Log("[FireBarHurt] No HitPointSystem found on host/parents."); return; }

        if (hp.isInvincible) { Debug.Log("[FireBarHurt] Player invincible, skip."); return; }

        int damage = Mathf.Max(1, minDamage);
        hp.AddDamage(damage);
        Debug.Log($"[FireBarHurt] Damage {damage} applied. (HP 会在 HitPointSystem 的 Debug 里显示)");

        var rb = host.GetComponentInParent<Rigidbody2D>();
        if (rb)
        {
            if (hitDir.sqrMagnitude < 0.0001f) hitDir = Vector2.up;
            hitDir = (hitDir + Vector2.up * upwardBonus).normalized;

            if (rb.velocity.y < 0f) rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(hitDir * knockbackImpulse, ForceMode2D.Impulse);
        }
    }
}

