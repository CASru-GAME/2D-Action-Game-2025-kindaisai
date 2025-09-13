using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HazardDamage2D_PDS : MonoBehaviour
{
    [Header("伤害设置")]
    public float damage = 10f;
    public float hitCooldown = 0.4f;
    public float knockbackImpulse = 6f;

    [Header("只对这些层生效（勾 Player）")]
    public LayerMask targetLayers;

    static readonly string[] DamageMethods = 
        { "TakeDamage", "ApplyDamage", "Damage", "ReduceHP", "LoseHP" };

    private readonly Dictionary<Collider2D, float> _lastHit = new();

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true; 
    }

    void OnTriggerEnter2D(Collider2D other) => TryHit(other);
    void OnTriggerStay2D(Collider2D other)  => TryHit(other);

    void TryHit(Collider2D other)
    {
        if ((targetLayers.value & (1 << other.gameObject.layer)) == 0) return;

        if (_lastHit.TryGetValue(other, out float t) && (Time.time - t) < hitCooldown) return;

        var pds = other.GetComponentInParent<PlayerDataStore>();
        var hps = pds != null ? pds.HitPointSystem : null;

        if (hps != null)
        {     
            MethodInfo mi = null;
            var type = hps.GetType();
            foreach (string name in DamageMethods)
            {
                mi = type.GetMethod(name, BindingFlags.Public | BindingFlags.Instance);
                if (mi == null) continue;
                var ps = mi.GetParameters();
                if (ps.Length == 1 && (ps[0].ParameterType == typeof(float) || ps[0].ParameterType == typeof(int)))
                {
                    object arg = ps[0].ParameterType == typeof(int)
                        ? (object)Mathf.RoundToInt(damage)
                        : damage as object;
                    mi.Invoke(hps, new object[] { arg });
                    break;
                }
                mi = null; 
            }

            if (mi == null)
            {
                Debug.LogWarning("[HazardDamage2D_PDS] 没找到可用的扣血方法。请在 HitPointSystem 里提供 public 单参数方法（float 或 int），"
                                 + $"方法名最好是：{string.Join("/", DamageMethods)}。");
            }
        }

        var rb = other.attachedRigidbody;
        if (rb != null)
        {
            Vector2 dir = (other.transform.position - transform.position).normalized;
            rb.AddForce(dir * knockbackImpulse, ForceMode2D.Impulse);
        }

        _lastHit[other] = Time.time;
    }
}
