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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 接地判定
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            isDoubleJumping = false;
        }
        // 左右移動
        float moveInput = Input.GetAxisRaw("Horizontal");
        float speed = (Input.GetKey(KeyCode.LeftShift)) ? dashSpeed : moveSpeed;
        rb.velocity = new Vector2((isReverse ? -1 : 1) * moveInput * speed, rb.velocity.y);

        
        // ジャンプ開始（ボタンを押した瞬間）
        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                isJumping = true;
                jumpTimeCounter = maxJumpHoldTime;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if(DoubleJump && !isJumping && !isDoubleJumping)
            {   
                isDoubleJumping = true;
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
