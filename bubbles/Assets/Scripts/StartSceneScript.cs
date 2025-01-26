using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // 加载游戏场景，替换 "GameScene" 为你的实际场景名称
        SceneManager.LoadScene("SampleScene");
    }
}
