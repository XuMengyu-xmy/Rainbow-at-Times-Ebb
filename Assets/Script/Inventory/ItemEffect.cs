// ItemEffect.cs
using UnityEngine;

// 处理物品使用效果的类
public class ItemEffect : MonoBehaviour
{
    // 使用物品
    public static bool UseItem(ItemData item)
    {
        switch (item.itemType)
        {
            case ItemType.Consumable:
                return UseConsumable(item);
                
            case ItemType.Equipment:
                return EquipItem(item);
                
            case ItemType.Material:
            case ItemType.Quest:
                Debug.Log(item.itemName + " 不能直接使用");
                return false;
                
            default:
                Debug.Log("未知物品类型: " + item.itemType);
                return false;
        }
    }
    
    // 使用消耗品
    private static bool UseConsumable(ItemData item)
    {
        // 获取玩家状态组件
        if (PlayerStats.instance == null)
        {
            Debug.LogWarning("找不到PlayerStats实例");
            return false;
        }
        
        // 应用效果
        bool effectApplied = false;
        
        if (item.healthRestore > 0)
        {
            PlayerStats.instance.RestoreHealth(item.healthRestore);
            effectApplied = true;
        }
        
        if (item.manaRestore > 0)
        {
            PlayerStats.instance.RestoreMana(item.manaRestore);
            effectApplied = true;
        }
        
        if (effectApplied)
        {
            Debug.Log("使用了 " + item.itemName);
            
            // 可以添加特效或声音
            PlayUseEffect(item);
            
            return true;
        }
        
        return false;
    }
    
    // 装备物品
    private static bool EquipItem(ItemData item)
    {
        // 获取玩家状态组件
        if (PlayerStats.instance == null)
        {
            Debug.LogWarning("找不到PlayerStats实例");
            return false;
        }
        
        // 装备物品
        PlayerStats.instance.EquipItem(item);
        
        // 可以添加装备特效或声音
        PlayEquipEffect(item);
        
        return true;
    }
    
    // 播放使用物品的特效
    private static void PlayUseEffect(ItemData item)
    {
        // 这里可以添加粒子效果、动画或声音
        // 例如：Instantiate(item.useEffect, player.position, Quaternion.identity);
    }
    
    // 播放装备物品的特效
    private static void PlayEquipEffect(ItemData item)
    {
        // 这里可以添加装备特效或声音
        // 例如：Instantiate(item.equipEffect, player.position, Quaternion.identity);
    }
}