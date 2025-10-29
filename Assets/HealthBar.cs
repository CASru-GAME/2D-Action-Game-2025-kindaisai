using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;                 
    public Transform followTarget;     
    public Vector3 worldOffset = new Vector3(0f, 0.7f, 0f);

    private HitPointSystem hp;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
        if (!followTarget) followTarget = transform.parent;        
        hp = GetComponentInParent<HitPointSystem>();               
    }

    void LateUpdate()
    {
        
        if (followTarget)
        {
            transform.position = followTarget.position + worldOffset;
            if (cam) transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }

        
        if (hp && fill)
        {
            fill.fillAmount = (float)Mathf.Max(0, hp.HP) / Mathf.Max(1, hp.MaxHP);
        }
        
        if (followTarget)
        {
            float parentSign = Mathf.Sign(followTarget.lossyScale.x); 
            var ls = transform.localScale;
            
            ls.x = parentSign < 0 ? -Mathf.Abs(ls.x) : Mathf.Abs(ls.x);
            transform.localScale = ls;
        }

    }
}


