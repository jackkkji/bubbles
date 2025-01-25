// 2025/1/25 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 玩家对象的 Transform
    public Vector3 offset; // 相机与玩家之间的偏移量

    void Start()
    {
        // 可以初始化偏移量为相机当前的位置与玩家的位置之差
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 将相机的位置设置为玩家的位置加上偏移量
        transform.position = player.position + offset;
    }
}