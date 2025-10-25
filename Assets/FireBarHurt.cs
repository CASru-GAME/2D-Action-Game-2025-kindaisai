using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FireBarHurt : MonoBehaviour
{
    public int minDamage = 1;
    public float knockbackImpulse = 6f;

    public float upwardBonus = 0.2f;
    private void OnCollisionEnter2D(Collision2D c)
    {
        TryHurt(c.collider, -c.GetContact(0).normal); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        TryHurt(other, ((Vector2)other.transform.position - (Vector2)transform.position).normalized);
    }

    private void TryHurt(Collider2D other, Vector2 hitDir)
    {
        if (!other || !other.CompareTag("Player")) return;

        var hp = other.GetComponent<HitPointSystem>();
        if (hp == null) return;
        if (hp.isInvincible) return; 
        int damage = Mathf.Max(minDamage, Mathf.CeilToInt(hp.MaxHP * 0.2f));
        hp.AddDamage(damage); 
        var rb = other.attachedRigidbody;
        if (rb != null)
        {
            if (hitDir.sqrMagnitude < 0.0001f) hitDir = Vector2.up;
            hitDir = (hitDir + Vector2.up * upwardBonus).normalized;
            if (rb.velocity.y < 0f)
                rb.velocity = new Vector2(rb.velocity.x, 0f);

            rb.AddForce(hitDir * knockbackImpulse, ForceMode2D.Impulse);
        }
    }
}
