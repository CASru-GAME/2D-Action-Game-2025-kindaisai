using UnityEngine;

[DisallowMultipleComponent]
public class FireBarSpinner : MonoBehaviour
{
    public Transform bar;
    public float radius = 2f;
    [Header("回転速度")]
    public float degreesPerSecond = 120f;

    void Reset()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; 
        if (transform.childCount > 0) bar = transform.GetChild(0);
    }

    void OnValidate()
    {
        if (bar == null && transform.childCount > 0) bar = transform.GetChild(0);

        if (bar != null)
        {
            bar.localPosition = new Vector3(radius, 0f, 0f);
            var rb2d = bar.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                #if UNITY_EDITOR
                Debug.LogWarning("[FireBarSpinner] 子物体 Bar ");
                #endif
                rb2d.simulated = false; 
            }
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, degreesPerSecond * Time.deltaTime, Space.Self);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        const int seg = 48;
        float r = (bar ? Vector3.Distance(bar.position, transform.position) : radius);
        Vector3 c = transform.position;
        Vector3 prev = c + new Vector3(r, 0, 0);
        for (int i = 1; i <= seg; i++)
        {
            float a = i * Mathf.PI * 2f / seg;
            Vector3 p = c + new Vector3(Mathf.Cos(a) * r, Mathf.Sin(a) * r, 0);
            Gizmos.DrawLine(prev, p);
            prev = p;
        }
        Gizmos.DrawSphere(c, 0.05f);
    }
}
