using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI")]
    public Image fill;                            

    [Header("Follow")]
    public Transform followTarget;                
    public Vector3 worldOffset = new Vector3(0f, 0.3f, 0f);
    public bool keepFacingCamera = true;          
    public bool cancelParentFlip = true;          

    HitPointSystem hp;
    Camera cam;

    void Awake()
    {
        cam = Camera.main;
        
        hp = GetComponentInParent<HitPointSystem>();
    }

    void LateUpdate()
    {
        
        if (followTarget)
        {
            transform.position = followTarget.position + worldOffset;

            if (keepFacingCamera && cam)
                transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }

        
        if (cancelParentFlip)
        {
            Transform basis = followTarget ? followTarget : transform.parent;
            if (basis)
            {
                float sign = Mathf.Sign(basis.lossyScale.x); 
                Vector3 ls = transform.localScale;
                ls.x = sign < 0 ? -Mathf.Abs(ls.x) : Mathf.Abs(ls.x); 
                transform.localScale = ls;
            }
        }

        
        if (hp && fill)
        {
            float ratio = (float)Mathf.Max(0, hp.HP) / Mathf.Max(1, hp.MaxHP);
            fill.fillAmount = ratio;
        }
    }
}



