using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class ProximityGameOver : MonoBehaviour
{
    public float detectionRadius = 2f;
    public string playerTag = "Player";
    private bool gameOverTriggered = false;
    public TMP_Text gameOverText;

    void Start()
    {
        // 确保碰撞体是触发器
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
        
        // 调试信息：显示碰撞体状态
        Debug.Log($"碰撞体设置: isTrigger={col.isTrigger}, 大小={col.size}");
        
        if (gameOverText == null)
        {
            CreateGameOverText();
        }
        else
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 增加调试信息，查看触发情况
        Debug.Log($"检测到碰撞: {other.gameObject.name}, 标签: {other.tag}");
        
        if (other.CompareTag(playerTag) && !gameOverTriggered)
        {
            Debug.Log($"玩家 {playerTag} 进入触发区域");
            TriggerGameOver();
        }
        else if (!other.CompareTag(playerTag))
        {
            Debug.Log($"碰撞对象不是 {playerTag}，标签为: {other.tag}");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }

    private void TriggerGameOver()
    {
        gameOverTriggered = true;
        Debug.Log("触发游戏结束");
        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("游戏结束文本未找到！");
        }
        
        Time.timeScale = 0;
    }

    private void CreateGameOverText()
    {
        GameObject canvas = new GameObject("GameOverCanvas");
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        DontDestroyOnLoad(canvas);
        
        GameObject textObject = new GameObject("GameOverText");
        textObject.transform.SetParent(canvas.transform);
        gameOverText = textObject.AddComponent<TextMeshProUGUI>();
        
        gameOverText.text = "Game Over";
        gameOverText.fontSize = 72;
        gameOverText.color = Color.red;
        gameOverText.alignment = TextAlignmentOptions.Center;
        
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        
        gameOverText.gameObject.SetActive(false);
    }
}
    