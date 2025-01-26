using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform circleTransform; // 仓鼠球的Transform
    [SerializeField] private Rigidbody2D circleRigidbody; // 仓鼠球的Rigidbody
    [SerializeField] private float moveSpeed = 5f;      // 主角的移动速度
    [SerializeField] private float ballPushForce = 10f; // 主角推动仓鼠球的力
    [SerializeField] private float maxDistanceFromCenter = 1.5f; // 主角活动范围（球内半径）

    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 获取玩家输入
        float horizontalInput = Input.GetAxis("Horizontal");

        // 水平移动主角
        playerRigidbody.linearVelocity = new Vector2(horizontalInput * moveSpeed, playerRigidbody.linearVelocity.y);

        // 限制主角的活动范围（相对于仓鼠球中心）
        Vector2 ballToPlayer = transform.position - circleTransform.position; // 世界坐标差
        if (ballToPlayer.magnitude > maxDistanceFromCenter)
        {
            Vector2 limitedPosition = (Vector2)circleTransform.position + ballToPlayer.normalized * maxDistanceFromCenter;
            transform.position = limitedPosition;
        }

        // 推动仓鼠球
        PushCircle(horizontalInput);
    }

    private void PushCircle(float horizontalInput)
    {
        // 推动仓鼠球
        float torque = -horizontalInput * ballPushForce; // 负号确保方向正确
        circleRigidbody.AddTorque(torque);
    }
}
