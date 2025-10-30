using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;                 // 拖入 Fill 图片（Image Type=Filled, Horizontal）
    public Transform followTarget;     // 留空则默认跟随父物体(玩家)
    public Vector3 worldOffset = new Vector3(0f, 0.7f, 0f);

    private HitPointSystem hp;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
        if (!followTarget) followTarget = transform.parent;        // 让血条跟着玩家
        hp = GetComponentInParent<HitPointSystem>();               // 从父链找到你的HP脚本
    }

    void LateUpdate()
    {
        // 跟随到头顶并面向相机
        if (followTarget)
        {
            transform.position = followTarget.position + worldOffset;
            if (cam) transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }

        // ★ 就是你问的这段：把填充比例设为 HP/MaxHP
        if (hp && fill)
        {
            fill.fillAmount = (float)Mathf.Max(0, hp.HP) / Mathf.Max(1, hp.MaxHP);
        }
    }
}


