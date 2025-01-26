using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform circleTransform; // 仓鼠球的Transform
    [SerializeField] private Rigidbody2D circleRigidbody; // 仓鼠球的Rigidbody
    [SerializeField] private float moveSpeed = 5f;      // 主角的移动速度
    [SerializeField] private float ballPushForce = 10f; // 主角推动仓鼠球的力
    [SerializeField] private float maxDistanceFromCenter = 1.5f; // 主角活动范围（球内半径）

    // 体力系统
    [SerializeField] private float maxStamina = 100f;     // 最大体力值
    [SerializeField] private float stamina;              // 当前体力值
    [SerializeField] private float staminaDrainRate = 5f; // 每秒体力消耗速率
    [SerializeField] private float slowRecoveryRate = 10f; // 前3秒每秒恢复体力
    [SerializeField] private float fastRecoveryRate = 45f; // 后2秒每秒恢复体力

    private Rigidbody2D playerRigidbody;
    private float recoveryTimer = 0f; // 恢复计时器

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        stamina = maxStamina; // 初始化体力值
    }

    private void Update()
    {
        // 获取玩家输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical"); // 获取垂直方向的输入

        // 如果有体力，允许玩家移动
        if (stamina > 0 && (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0))
        {
            // 移动主角
            Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * moveSpeed;
            playerRigidbody.linearVelocity = new Vector2(movement.x, movement.y);

            // 消耗体力
            DrainStamina(Time.deltaTime * staminaDrainRate);

            // 重置恢复计时器
            recoveryTimer = 0f;
        }
        else
        {
            // 停止主角移动
            playerRigidbody.linearVelocity = Vector2.zero;

            // 恢复体力
            RecoverStamina(Time.deltaTime);
        }

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
        // 始终可以推动仓鼠球，无需依赖地面检测
        if (stamina > 0) // 推动需要体力
        {
            float torque = -horizontalInput * ballPushForce; // 负号确保方向正确
            circleRigidbody.AddTorque(torque);
        }
    }

    private void DrainStamina(float amount)
    {
        // 消耗体力
        float oldStamina = stamina;
        stamina = Mathf.Max(0, stamina - amount);

        // 如果体力有变化，打印信息
        if (stamina != oldStamina)
        {
            Debug.Log($"Stamina Decreased: {stamina}");
        }
    }

    private void RecoverStamina(float deltaTime)
    {
        // 更新恢复计时器
        recoveryTimer += deltaTime;

        // 计算恢复速率（分段逻辑）
        float recoveryRate = recoveryTimer <= 3f ? slowRecoveryRate : fastRecoveryRate;

        // 恢复体力
        float oldStamina = stamina;
        stamina = Mathf.Min(maxStamina, stamina + recoveryRate * deltaTime);

        // 如果体力有变化，打印信息
        if (stamina != oldStamina)
        {
            Debug.Log($"Stamina Recovered: {stamina}");
        }
    }
}
