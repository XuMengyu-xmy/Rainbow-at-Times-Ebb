// InventoryTest.cs
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public ItemData testItem; // 在Inspector中设置要测试的物品
    public int testAmount = 1; // 测试添加的物品数量
    
    void Update()
    {
        // 按T键测试添加物品
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestAddItem();
        }
        
        // 按C键测试清空背包
        if (Input.GetKeyDown(KeyCode.C))
        {
            TestClearInventory();
        }
        
        // 按I键切换背包显示（与Inventory脚本中的按键一致）
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("按下I键，切换背包显示");
        }
    }
    
    void TestAddItem()
    {
        if (testItem == null)
        {
            Debug.LogError("测试物品未设置！请在Inspector中设置testItem字段");
            return;
        }
        
        Inventory inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("当前对象没有Inventory组件！");
            return;
        }
        
        Debug.Log($"测试添加物品: {testItem.itemName} x {testAmount}");
        bool success = inventory.AddItem(testItem, testAmount);
        
        if (success)
        {
            Debug.Log($"成功添加 {testItem.itemName} x {testAmount} 到背包");
        }
        else
        {
            Debug.LogError($"添加 {testItem.itemName} 到背包失败");
        }
    }
    
    void TestClearInventory()
    {
        Inventory inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("当前对象没有Inventory组件！");
            return;
        }
        
        Debug.Log("清空背包");
        inventory.items.Clear();
        
        // 通知UI更新
        if (inventory.onItemChangedCallback != null)
        {
            inventory.onItemChangedCallback.Invoke();
        }
    }
    
    void Start()
    {
        Debug.Log("InventoryTest脚本已启动");
        Debug.Log("按T键测试添加物品");
        Debug.Log("按C键测试清空背包");
        Debug.Log("按I键切换背包显示");
        
        // 检查Inventory组件
        Inventory inventory = GetComponent<Inventory>();
        if (inventory != null)
        {
            Debug.Log("找到Inventory组件");
        }
        else
        {
            Debug.LogError("未找到Inventory组件！");
        }
    }
}