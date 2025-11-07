using UnityEngine;
using UnityEngine.UI;

// 这个脚本用于创建基本的背包UI结构
public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform slotsParent;
    public GameObject slotPrefab;
    
    void Start()
    {
        // 如果没有设置背包面板，创建一个基本的UI结构
        if (inventoryPanel == null)
        {
            CreateBasicInventoryUI();
        }
    }
    
    void CreateBasicInventoryUI()
    {
        // 查找或创建Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }
        
        // 创建背包面板
        inventoryPanel = new GameObject("InventoryPanel");
        inventoryPanel.transform.SetParent(canvas.transform, false);
        
        // 添加Image组件作为背景
        Image panelImage = inventoryPanel.AddComponent<Image>();
        panelImage.color = new Color(0.1f, 0.1f, 0.1f, 0.8f); // 半透明黑色背景
        
        // 设置RectTransform
        RectTransform panelRect = inventoryPanel.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.3f, 0.3f);
        panelRect.anchorMax = new Vector2(0.7f, 0.7f);
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;
        
        // 创建物品格子容器
        GameObject slotsContainer = new GameObject("SlotsContainer");
        slotsContainer.transform.SetParent(inventoryPanel.transform, false);
        
        // 添加GridLayoutGroup组件
        GridLayoutGroup gridLayout = slotsContainer.AddComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(60, 60);
        gridLayout.spacing = new Vector2(5, 5);
        gridLayout.padding = new RectOffset(10, 10, 10, 10);
        gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayout.childAlignment = TextAnchor.UpperLeft;
        
        // 设置RectTransform
        RectTransform containerRect = slotsContainer.GetComponent<RectTransform>();
        containerRect.anchorMin = Vector2.zero;
        containerRect.anchorMax = Vector2.one;
        containerRect.offsetMin = new Vector2(10, 10);
        containerRect.offsetMax = new Vector2(-10, -10);
        
        slotsParent = slotsContainer.transform;
        
        // 如果没有设置格子预制体，创建一个基本的
        if (slotPrefab == null)
        {
            CreateSlotPrefab();
        }
        
        // 初始状态设置为隐藏
        inventoryPanel.SetActive(false);
        
        Debug.Log("创建了基本的背包UI结构");
    }
    
    void CreateSlotPrefab()
    {
        // 创建格子预制体
        slotPrefab = new GameObject("Slot");
        
        // 添加Image组件
        Image slotImage = slotPrefab.AddComponent<Image>();
        slotImage.color = new Color(0.2f, 0.2f, 0.2f, 0.8f); // 深灰色背景
        
        // 设置RectTransform
        RectTransform slotRect = slotPrefab.GetComponent<RectTransform>();
        slotRect.sizeDelta = new Vector2(60, 60);
        
        // 添加InventorySlot组件
        slotPrefab.AddComponent<InventorySlot>();
        
        // 保存为预制体
        #if UNITY_EDITOR
        string localPath = "Assets/UI/Slot.prefab";
        if (!System.IO.Directory.Exists("Assets/UI"))
        {
            System.IO.Directory.CreateDirectory("Assets/UI");
        }
        
        // 如果已存在，先删除
        if (UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(localPath) != null)
        {
            UnityEditor.AssetDatabase.DeleteAsset(localPath);
        }
        
        UnityEditor.PrefabUtility.SaveAsPrefabAsset(slotPrefab, localPath);
        #endif
        
        // 销毁临时对象
        DestroyImmediate(slotPrefab);
        
        // 重新加载预制体
        #if UNITY_EDITOR
        slotPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(localPath);
        #endif
        
        Debug.Log("创建了物品格子预制体");
    }
}