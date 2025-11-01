using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;    // 通常移動速度
    [SerializeField] private float dashSpeed = 8f;    // ダッシュ時の速度
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float dashAcceleration;
    public bool isOnIce;
    [SerializeField] float iceForward;//氷の進行方向へのすべりやすさ
    [SerializeField] float iceBackward;//氷の逆方向へのすべりやすさ

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 7f;           // ジャンプ初速
    [SerializeField] private float jumpHoldForce = 2f;       // 長押し中に加える力
    [SerializeField] private float maxJumpHoldTime = 0.2f;   // 長押しが有効な時間

    [Header("接地判定")]
    [SerializeField] private Transform groundCheck;   // 足元の判定位置
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;   // 地面レイヤー

    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    private bool isDashing;

    public bool isReverse;
    private bool isJumping;
    public bool DoubleJump;
    private bool isDoubleJumping;
    private float jumpTimeCounter;
    public bool isBounce;
    private bool isBouncing;
    [SerializeField] float MaxBounceTime;//跳ねてからジャンプできる時間
    float BounceTime;//跳ねてからジャンプできる残りの時間

    GameObject mainCamera;
    Vector3 PlayerScreenPos;
    float Width,Height;

    public float x_Speed;
    public float x_Acceleration;
    public float y_Speed;
    public float y_Acceleration;
    public float Repulsion;//反発係数
    public int Bounce_num;//バウンド回数
    public bool isShot;
    public float ShotInterval;
    float cur_ShotInterval;
    [SerializeField] GameObject magicBulletPrefab;
    bool isJumped;

    Animator animator;
    public AudioSource audioSource;
    [SerializeField] AudioClip jump_sound;
    public AudioClip steped_sound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.Find("Main Camera");
        BounceTime = MaxBounceTime;
        Width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0f,0f)).x - Camera.main.ScreenToWorldPoint(new Vector3(0f,0f,0f)).x;
        Height = Camera.main.ScreenToWorldPoint(new Vector3(0f,Screen.height,0f)).y - Camera.main.ScreenToWorldPoint(new Vector3(0f,0f,0f)).y;
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x,transform.position.y + Height / 4f,-10f);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {           
        //カメラ追従
        PlayerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        if(PlayerScreenPos.x / Screen.width > 1f/2f)
        mainCamera.transform.position = new Vector3(transform.position.x,mainCamera.transform.position.y,-10f);
        else if(PlayerScreenPos.x / Screen.width < 1f/3f)
        mainCamera.transform.position = new Vector3(transform.position.x + Width /6f,mainCamera.transform.position.y,-10f);
        
        if(PlayerScreenPos.y / Screen.height > 2f/ 3f)
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x,transform.position.y - Height / 6f,-10f);
        else if(PlayerScreenPos.y / Screen.height < 1f/3f && transform.position.y > 1.5f)
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x,transform.position.y + Height / 6f,-10f);
        
    
        // 接地判定
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            isDoubleJumping = false;
        }
        // 左右移動
        float moveInput = Input.GetAxisRaw("Horizontal");
        float accelerationSpeed;

        if(transform.position.x < -3f)
        {
            rb.velocity = new Vector2(0f,rb.velocity.y);
            transform.position = new Vector3(-3f,transform.position.y,0f);
        }

        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            accelerationSpeed = SetAcceleration(moveInput, dashAcceleration);

            if (Mathf.Abs(rb.velocity.x + (isReverse ? -1 : 1) * accelerationSpeed) <= dashSpeed)
                rb.velocity += new Vector2((isReverse ? -1 : 1) * accelerationSpeed, 0);
            else if (Mathf.Abs(rb.velocity.x) > dashSpeed)
            Stop();
        }*/
  
        accelerationSpeed = SetAcceleration(moveInput, moveAcceleration);

            if (Mathf.Abs(rb.velocity.x + (isReverse ? -1 : 1) * accelerationSpeed) <= moveSpeed)
                rb.velocity += new Vector2((isReverse ? -1 : 1) * accelerationSpeed, 0);
            else if (rb.velocity.x * moveInput < 0)
                rb.velocity += new Vector2((isReverse ? -1 : 1) * accelerationSpeed, 0);
            else if (Mathf.Abs(rb.velocity.x) > moveSpeed)
                Stop();

        
        if (moveInput == 0)
            Stop();
        
        // ジャンプ開始（ボタンを押した瞬間）
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {   
                isJumped = true;
                isJumping = true;
                jumpTimeCounter = maxJumpHoldTime;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetTrigger("JumpTrigger");
                audioSource.PlayOneShot(jump_sound);
            }
            else if (DoubleJump && !isJumping && !isDoubleJumping)
            {
                isDoubleJumping = true;
                isJumping = true;
                jumpTimeCounter = maxJumpHoldTime;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (isBounce && !isBouncing)
            {
                isBounce = false;
                isBouncing = true;
                isJumping = true;
                jumpTimeCounter = maxJumpHoldTime;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        // ジャンプボタン長押し中
        if(Input.GetButton("Jump") && isJumping)
        {   
            if(jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpHoldForce * Time.deltaTime * 60f);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                isBouncing = false;
            }
        }

        // ジャンプボタンを離したら終了
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            isBouncing = false;
        }

        // キャラの向きを速度に合わせる（スプライト反転）
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        else if(rb.velocity.x < 0)
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        //時間がたったらジャンプボタンをしても跳ねなくなる
        if(isBounce)
        {
            BounceTime -= Time.deltaTime;
            if(BounceTime <= 0)
            {
                BounceTime = MaxBounceTime;
                isBounce = false;
            }
        }

        if(cur_ShotInterval > 0f)
        cur_ShotInterval -= Time.deltaTime;
        

        if(isShot && Input.GetKeyDown(KeyCode.R) && cur_ShotInterval <= 0f)
        Shot();

        if(Mathf.Abs(rb.velocity.x) > 0f)
        animator.speed = 1f;
        else if(Mathf.Abs(rb.velocity.y) > 0.1f)
        animator.speed = 1f;
        else
        animator.speed = 0f;

        if(rb.velocity.x == 0f || !isGrounded)
        {
            animator.SetBool("Idle",true);
            animator.speed = 1f;
        }
        else animator.SetBool("Idle",false);
    }

    void Stop()
    {
        if (rb.velocity.x > 0)
        {
            rb.velocity += new Vector2(isOnIce ? -moveAcceleration / 3f : -moveAcceleration, 0);
            if (rb.velocity.x < 0)
                rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (rb.velocity.x < 0)
        {
            rb.velocity += new Vector2(isOnIce ? moveAcceleration / 3f : moveAcceleration, 0);
            if (rb.velocity.x > 0)
                rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    float SetAcceleration(float moveInput, float acceleration)
    {
        float accelerationSpeed;
        if (isOnIce)
        {
            if (moveInput * rb.velocity.x > 0)
                accelerationSpeed = moveInput * acceleration * iceForward;
            else
                accelerationSpeed = moveInput * acceleration * iceBackward;
        }
        else
            accelerationSpeed = moveInput * acceleration;

        return accelerationSpeed;
    }

    void OnTriggerStay2D(Collider2D collision)
    {   
        int layer = collision.gameObject.layer;
        if (LayerMask.LayerToName(layer) == "Ground")
        isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (LayerMask.LayerToName(layer) == "Ground")
        isGrounded = false;;
    }

    

    void Shot()//魔法弾を発射する
    {   
        cur_ShotInterval = ShotInterval;

        MagicBullet magicBullet = Instantiate(magicBulletPrefab).GetComponent<MagicBullet>();
        magicBullet.x_Speed = x_Speed;
        magicBullet.y_Speed = y_Speed;
        magicBullet.x_Acceleration = x_Acceleration * transform.localScale.x;
        magicBullet.y_Acceleration = y_Acceleration;
        magicBullet.Repulsion = Repulsion;
        magicBullet.Bounce_num = Bounce_num;
        magicBullet.isPlayer = true;

        if (transform.localScale.x == 1)
            magicBullet.isLeft = false;
        else
            magicBullet.isLeft = true;

        magicBullet.transform.position = new Vector3(transform.position.x + transform.localScale.x, transform.position.y + transform.localScale.y / 2f);
    }
}
