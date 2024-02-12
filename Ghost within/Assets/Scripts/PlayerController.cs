using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // 玩家移动速度
    public float jumpForce = 10.0f; // 玩家跳跃力量
    private Rigidbody2D myRigidbody;
    private bool isGrounded; // 判断玩家是否在地面上
    public Transform groundCheck; // 地面检测点
    public float checkRadius; // 检测半径
    public LayerMask whatIsGround; // 定义哪一层是地面

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // 处理移动
        Jump(); // 处理跳跃
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // 获取水平轴上的输入
        myRigidbody.velocity = new Vector2(moveInput * speed, myRigidbody.velocity.y);
    }

    void Jump()
    {
        // 检测是否在地面上
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidbody.velocity = Vector2.up * jumpForce;
        }
    }
}
