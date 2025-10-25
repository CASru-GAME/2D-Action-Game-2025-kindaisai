using UnityEngine;

[DisallowMultipleComponent]
public class FireBarSpinner : MonoBehaviour
{
    public enum CenterMode
    {
        AroundSelf,        
        AroundWorldOrigin, 
        AroundBarLeftEdge   
    }

    [Header("要旋转的子物体（Bar）")]
    public Transform bar;

    [Header("半径（在 AroundSelf/AroundWorldOrigin 模式下使用）")]
    public float radius = 2f;

    [Header("旋转速度（度/秒，2D 用 Z 轴）")]
    public float degreesPerSecond = 120f;

    [Header("旋转中心模式")]
    public CenterMode centerMode = CenterMode.AroundSelf;

    // —— 内部状态 ——
    private float _angle;              
    private bool _leftPivotReady = false;
    private Vector3 _leftPivotWorld;   
    private float _barHalfWidthWorld = 0; 

    void Reset()
    {
        if (transform.childCount > 0) bar = transform.GetChild(0);
        var rb = GetComponent<Rigidbody2D>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnValidate()
    {
        if (bar == null && transform.childCount > 0) bar = transform.GetChild(0);
        if (bar != null && centerMode == CenterMode.AroundSelf)
        {
            bar.localPosition = new Vector3(radius, 0f, 0f);
            var rb2d = bar.GetComponent<Rigidbody2D>();
            if (rb2d != null) rb2d.simulated = false;
        }
        CacheBarHalfWidthForGizmo();
    }

    void Awake()
    {
        PrepareLeftEdgePivotIfNeeded();
    }

    void Start()
    {
        PrepareLeftEdgePivotIfNeeded();
    }

    void Update()
    {
        if (!bar) return;

        switch (centerMode)
        {
            case CenterMode.AroundWorldOrigin:
                _angle += degreesPerSecond * Time.deltaTime;
                float rad = _angle * Mathf.Deg2Rad;
                bar.position = new Vector3(Mathf.Cos(rad) * radius,
                                           Mathf.Sin(rad) * radius,
                                           bar.position.z);
                break;

            case CenterMode.AroundSelf:
                transform.Rotate(0f, 0f, degreesPerSecond * Time.deltaTime, Space.Self);
                break;

            case CenterMode.AroundBarLeftEdge:
                if (!_leftPivotReady) PrepareLeftEdgePivotIfNeeded();
                bar.RotateAround(_leftPivotWorld, Vector3.forward, degreesPerSecond * Time.deltaTime);
                break;
        }
    }

    
    void PrepareLeftEdgePivotIfNeeded()
    {
        if (bar == null || centerMode != CenterMode.AroundBarLeftEdge) return;

        var sr = bar.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning("[FireBarSpinner] AroundBarLeftEdge 模式需要 Bar 上有 SpriteRenderer。");
            return;
        }
        _barHalfWidthWorld = sr.bounds.size.x * 0.5f;
        _leftPivotWorld = bar.position - bar.right * (_barHalfWidthWorld * 2f * 0.5f);
        _leftPivotWorld.z = bar.position.z;

        _leftPivotReady = true;
    }

    void CacheBarHalfWidthForGizmo()
    {
        if (!bar) return;
        var sr = bar.GetComponent<SpriteRenderer>();
        _barHalfWidthWorld = sr ? sr.bounds.size.x * 0.5f : 0f;
    }

    void OnDrawGizmos()
    {
        if (!bar && transform.childCount > 0) bar = transform.GetChild(0);

        Gizmos.color = Color.yellow;

        if (centerMode == CenterMode.AroundWorldOrigin)
        {
            DrawCircle(Vector3.zero, radius);
        }
        else if (centerMode == CenterMode.AroundSelf)
        {
            DrawCircle(transform.position, radius);
        }
        else if (centerMode == CenterMode.AroundBarLeftEdge && bar != null)
        {
        
            Vector3 approxLeft = bar.position - bar.right * (_barHalfWidthWorld * 2f * 0.5f);
            approxLeft.z = bar.position.z;

            Gizmos.DrawSphere(approxLeft, 0.05f);
            DrawCircle(approxLeft, _barHalfWidthWorld > 0 ? _barHalfWidthWorld : 0.5f);
        }
    }


    void DrawCircle(Vector3 center, float r)
    {
        const int seg = 48;
        Vector3 prev = center + new Vector3(r, 0, 0);
        for (int i = 1; i <= seg; i++)
        {
            float a = i * Mathf.PI * 2f / seg;
            Vector3 p = center + new Vector3(Mathf.Cos(a) * r, Mathf.Sin(a) * r, 0);
            Gizmos.DrawLine(prev, p);
            prev = p;
        }
        Gizmos.DrawSphere(center, 0.05f);
    }
}

