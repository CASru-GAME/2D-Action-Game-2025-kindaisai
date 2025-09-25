using UnityEngine;

public class FollowCamera2D : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothTime = 0.15f;

    private Vector3 _vel;

    void OnEnable() { TryBindImmediate(); }
    void Start()    { if (!target) StartCoroutine(TryBindNextFrames()); }

    void LateUpdate()
    {
        if (!target) return;
        var desired = target.position + offset;
        desired.z = offset.z;        
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref _vel, smoothTime);
    }

    void TryBindImmediate()
    {
        try {
            if (!target)
            {
                var go = GameObject.FindGameObjectWithTag("Player");
                if (go) target = go.transform;
            }
        } catch
        {
            
        }

#if UNITY_2023_1_OR_NEWER
        if (!target) target = FindFirstObjectByType<PlayerController2D>()?.transform;
#else
        if (!target) target = FindObjectOfType<PlayerController2D>()?.transform;
#endif
    }

    System.Collections.IEnumerator TryBindNextFrames()
    {
        // 连续尝试若干帧，适配运行时生成 Player 的情况
        for (int i = 0; i < 60 && !target; i++)
        {
            yield return null;
            TryBindImmediate();
        }
    }
}

