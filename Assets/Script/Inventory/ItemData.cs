// ItemData.cs
using UnityEngine;

// 物品类型枚举
public enum ItemType
{
    Consumable, // 消耗品
    Equipment,  // 装备
    Material,   // 材料
    Quest       // 任务物品
}

// 创建一个菜单项，方便我们创建物品资产
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;         // 物品名称
    public string description;      // 物品描述
    public Sprite icon;             // 物品图标
    public GameObject prefab;       // 物品在游戏世界中的预制体（用于丢弃）
    public bool isStackable = true; // 是否可堆叠
    public int maxStackSize = 99;   // 最大堆叠数量
    public ItemType itemType;       // 物品类型
    
    [Header("消耗品属性")]
    public int healthRestore;       // 恢复的生命值
    public int manaRestore;         // 恢复的法力值
    public float effectDuration;    // 效果持续时间
    
    [Header("装备属性")]
    public int attackBonus;         // 攻击力加成
    public int defenseBonus;        // 防御力加成
    public float speedBonus;        // 速度加成
}
