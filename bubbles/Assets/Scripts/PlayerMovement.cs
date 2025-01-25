// 2025/1/25 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Rigidbody2D circleRigidbody;
    public float speed;
    public float pushForce = 10f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (Input.GetKey(KeyCode.Space))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        }

        // 检查 Player 是否靠近 Circle 的边界，并施加推力
        CheckPlayerPositionAndPushCircle(horizontalInput);
    }

    private void CheckPlayerPositionAndPushCircle(float horizontalInput)
    {
        float playerToCircleEdgeDistance = circleRigidbody.GetComponent<CircleCollider2D>().radius - Mathf.Abs(transform.localPosition.x);
        if (playerToCircleEdgeDistance <= 0.1f)
        {
            Vector2 pushDirection = new Vector2(horizontalInput, 0).normalized;
            circleRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Force);
        }
    }
}