// ItemStack.cs
using UnityEngine;

// 表示背包中的物品堆叠
[System.Serializable]
public class ItemStack
{
    public ItemData item;    // 物品数据
    public int amount;       // 物品数量
    
    public ItemStack(ItemData item, int amount = 1)
    {
        this.item = item;
        this.amount = amount;
    }
    
    // 尝试添加物品到堆叠中
    public bool TryAdd(int amountToAdd)
    {
        if (!item.isStackable)
            return false;
            
        int newAmount = amount + amountToAdd;
        if (newAmount <= item.maxStackSize)
        {
            amount = newAmount;
            return true;
        }
        return false;
    }
    
    // 尝试添加物品到堆叠中，返回剩余数量
    public int AddWithRemainder(int amountToAdd)
    {
        if (!item.isStackable)
            return amountToAdd;
            
        int newAmount = amount + amountToAdd;
        if (newAmount <= item.maxStackSize)
        {
            amount = newAmount;
            return 0;
        }
        else
        {
            int remainder = newAmount - item.maxStackSize;
            amount = item.maxStackSize;
            return remainder;
        }
    }
    
    // 从堆叠中移除物品
    public bool Remove(int amountToRemove)
    {
        if (amountToRemove <= 0)
            return false;
            
        if (amount >= amountToRemove)
        {
            amount -= amountToRemove;
            return true;
        }
        return false;
    }
    
    // 检查堆叠是否已满
    public bool IsFull()
    {
        if (!item.isStackable)
            return amount > 0;
        return amount >= item.maxStackSize;
    }
    
    // 检查是否可以添加指定数量的物品
    public bool CanAdd(int amountToAdd)
    {
        if (!item.isStackable)
            return false;
        return amount + amountToAdd <= item.maxStackSize;
    }
    
    // 获取可以添加的物品数量
    public int GetSpaceLeft()
    {
        if (!item.isStackable)
            return 0;
        return item.maxStackSize - amount;
    }
}