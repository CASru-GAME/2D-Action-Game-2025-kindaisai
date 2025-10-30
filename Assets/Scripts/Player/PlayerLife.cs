using System.Collections;           
using UnityEngine;                  

public class PlayerLife : MonoBehaviour
{
    public int Lives { get; private set; } = 5;
    public RespawnSystem respawnSystem;

    bool isDying;                   
    Animator anim;
    Rigidbody2D rb;
    PlayerController2D controller;
    HitPointSystem hp;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController2D>();
        hp = GetComponent<HitPointSystem>();
    }

    
    public void LoseLife()
    {
        if (!isDying) StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        isDying = true;
        Lives = Mathf.Max(0, Lives - 1);

        
        if (controller) controller.enabled = false;
        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;   
        }

        
        if (anim) anim.SetBool("player_death", true);

        
        float waitAnim = 0.5f;
        if (anim)
        {
            yield return null; 
            var st = anim.GetCurrentAnimatorStateInfo(0);
            if (st.length > 0.05f) waitAnim = st.length;
        }
        yield return new WaitForSeconds(waitAnim + 2f);

        
        if (anim) anim.SetBool("Dead", false);
        if (rb) rb.simulated = true;
        if (hp) hp.RecoverHP(hp.MaxHP);    
        if (respawnSystem) respawnSystem.Retry();

        if (controller) controller.enabled = true;
        isDying = false;
    }

    public void GameOver() { }
}
