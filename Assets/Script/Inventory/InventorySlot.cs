// InventorySlot.cs
using UnityEngine;
using UnityEngine.UI;
using Script; // 添加对Script命名空间的引用

public class InventorySlot : MonoBehaviour
{
    public Image icon;          // 物品图标
    public Button removeButton; // 丢弃按钮
    public Text amountText;     // 物品数量文本

    ItemStack itemStack; // 当前格子里的物品堆叠

    // 添加物品到格子
    public void AddItem(ItemData newItem, int amount = 1)
    {
        // 检查空引用
        if (newItem == null)
        {
            Debug.LogError("尝试添加空物品数据");
            return;
        }
        
        itemStack = new ItemStack(newItem, amount);
        
        // 检查icon是否为空
        if (icon == null)
        {
            Debug.LogError("InventorySlot的icon组件未设置");
            return;
        }
        
        // 检查物品图标是否为空
        if (itemStack.item.icon == null)
        {
            Debug.LogWarning($"物品 '{itemStack.item.itemName}' 没有设置图标，使用默认显示");
            icon.enabled = true;
            icon.color = Color.white; // 保持白色显示
        }
        else
        {
            icon.sprite = itemStack.item.icon;
            icon.enabled = true;
        }
        
        if (removeButton != null)
        {
            removeButton.interactable = true;
        }
        else
        {
            Debug.LogWarning("InventorySlot的removeButton组件未设置");
        }
        
        // 更新数量文本
        if (amountText != null)
        {
            if (amount > 1 && newItem.isStackable)
            {
                amountText.text = amount.ToString();
                amountText.enabled = true;
            }
            else
            {
                amountText.enabled = false;
            }
        }
        else
        {
            Debug.LogWarning("InventorySlot的amountText组件未设置");
        }
    }
    
    // 添加物品堆叠到格子
    public void AddItem(ItemStack stack)
    {
        // 检查空引用
        if (stack == null || stack.item == null)
        {
            Debug.LogError("尝试添加空物品堆叠或空物品数据");
            return;
        }
        
        itemStack = stack;
        
        // 检查icon是否为空
        if (icon == null)
        {
            Debug.LogError("InventorySlot的icon组件未设置");
            return;
        }
        
        // 检查物品图标是否为空
        if (stack.item.icon == null)
        {
            Debug.LogWarning($"物品 '{stack.item.itemName}' 没有设置图标，使用默认显示");
            icon.enabled = true;
            icon.color = Color.white; // 保持白色显示
        }
        else
        {
            icon.sprite = stack.item.icon;
            icon.enabled = true;
        }
        
        if (removeButton != null)
        {
            removeButton.interactable = true;
        }
        else
        {
            Debug.LogWarning("InventorySlot的removeButton组件未设置");
        }
        
        // 更新数量文本
        if (amountText != null)
        {
            if (itemStack.amount > 1 && itemStack.item.isStackable)
            {
                amountText.text = itemStack.amount.ToString();
                amountText.enabled = true;
            }
            else
            {
                amountText.enabled = false;
            }
        }
        else
        {
            Debug.LogWarning("InventorySlot的amountText组件未设置");
        }
    }

    // 清空格子
    public void ClearSlot()
    {
        itemStack = null;
        
        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
        
        if (removeButton != null)
        {
            removeButton.interactable = false;
        }
        
        // 隐藏数量文本
        if (amountText != null)
        {
            amountText.enabled = false;
        }
    }
    
    // 获取当前格子中的物品
    public ItemData GetCurrentItem()
    {
        return itemStack?.item;
    }
    
    // 获取当前格子中的物品数量
    public int GetCurrentAmount()
    {
        return itemStack?.amount ?? 0;
    }

    // 丢弃按钮点击事件
    public void OnRemoveButton()
    {
        if (itemStack == null) return;
        
        // 在玩家位置前方一点生成物品预制体
        GameObject droppedItem = Instantiate(itemStack.item.prefab, PrefabControl.instance.dropPosition.position, PrefabControl.instance.dropPosition.rotation);
        
        // 如果物品可堆叠且数量大于1，只丢弃一个
        if (itemStack.item.isStackable && itemStack.amount > 1)
        {
            // 从背包中移除一个物品
            Inventory.instance.RemoveItem(itemStack.item, 1);
        }
        else
        {
            // 从背包中移除整个堆叠
            Inventory.instance.RemoveStack(itemStack);
        }
    }


    // 用于拖拽系统
    public void UseItem()
    {
        if (itemStack == null) return;
        
        // 尝试使用物品
        if (ItemEffect.UseItem(itemStack.item))
        {
            // 使用成功，从背包中移除一个物品
            Inventory.instance.RemoveItem(itemStack.item, 1);
        }
        else
        {
            // 使用失败，显示提示信息
            Debug.Log("无法使用 " + itemStack.item.itemName);
        }
    }
}