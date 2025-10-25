using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;    // 通常移動速度
    [SerializeField] private float dashSpeed = 8f;    // ダッシュ時の速度

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 7f;           // ジャンプ初速
    [SerializeField] private float jumpHoldForce = 2f;       // 長押し中に加える力
    [SerializeField] private float maxJumpHoldTime = 0.2f;   // 長押しが有効な時間

    [Header("接地判定")]
    [SerializeField] private Transform groundCheck;   // 足元の判定位置
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;   // 地面レイヤー

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing;

    public bool isReverse;
    private bool isJumping;
    public bool DoubleJump;
    private bool isDoubleJumping;
    private float jumpTimeCounter;

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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.Find("Main Camera");
        BounceTime = MaxBounceTime;
        Width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0f,0f)).x - Camera.main.ScreenToWorldPoint(new Vector3(0f,0f,0f)).x;
        Height = Camera.main.ScreenToWorldPoint(new Vector3(0f,Screen.height,0f)).y - Camera.main.ScreenToWorldPoint(new Vector3(0f,0f,0f)).y;
    }

    void Update()
    {           
        //カメラ追従
        PlayerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        if(PlayerScreenPos.x / Screen.width > 2f/3f)
        mainCamera.transform.position = new Vector3(transform.position.x - Width /6f,mainCamera.transform.position.y,-10f);
        else if(PlayerScreenPos.x / Screen.width < 1f/3f)
        mainCamera.transform.position = new Vector3(transform.position.x + Width /6f,mainCamera.transform.position.y,-10f);
        
        if(PlayerScreenPos.y / Screen.height > 2f/3f)
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x,transform.position.y - Height /6f,-10f);
        else if(PlayerScreenPos.y / Screen.height < 1f/3f)
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x,transform.position.y + Height /6f,-10f);
        // 接地判定
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            isDoubleJumping = false;
        }

        // 左右移動
        float moveInput = Input.GetAxisRaw("Horizontal");
        float speed = (Input.GetKey(KeyCode.LeftShift)) ? dashSpeed : moveSpeed;
        rb.linearVelocity = new Vector2((isReverse ? -1 : 1) * moveInput * speed, rb.linearVelocity.y);

        // ジャンプ開始（ボタンを押した瞬間）
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isJumping = true;
                jumpTimeCounter = maxJumpHoldTime;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (DoubleJump && !isJumping && !isDoubleJumping)
            {
                isDoubleJumping = true;
                isJumping = true;
                jumpTimeCounter = maxJumpHoldTime;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }

        // ジャンプボタン長押し中
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    rb.linearVelocity.y + jumpHoldForce * Time.deltaTime * 60f
                );
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // ジャンプボタンを離したら終了
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        // キャラの向きを入力方向に合わせる（スプライト反転）
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        if(cur_ShotInterval > 0f)
        cur_ShotInterval -= Time.deltaTime;
        

        if(isShot && Input.GetKeyDown(KeyCode.R) && cur_ShotInterval <= 0f)
        Shot();
    }

    private void OnDrawGizmosSelected()
    {
        // 足元の判定をScene上に可視化
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}




    void OnTriggerStay2D(Collider2D collision)
    {
        MoveableBlock moveableBlock = collision.gameObject.GetComponent<MoveableBlock>();
        if (moveableBlock != null)
        {
            if (Input.GetKey(KeyCode.K))
            {
                if (rb.velocity.x * (collision.gameObject.transform.position.x - transform.position.x) > 0)
                    moveableBlock.Push(rb.velocity.x,transform.position,transform.localScale.x);
                else if (rb.velocity.x * (collision.gameObject.transform.position.x - transform.position.x) < 0)
                    moveableBlock.Pull(rb.velocity.x,transform.position,transform.localScale.x);
            }
        }
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

