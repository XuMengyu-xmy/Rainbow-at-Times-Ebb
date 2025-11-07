// PlayerStats.cs
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    
    [Header("基础属性")]
    public int maxHealth = 100;
    public int currentHealth;
    public int maxMana = 50;
    public int currentMana;
    
    [Header("装备加成")]
    public int attackBonus;
    public int defenseBonus;
    public float speedBonus;
    
    void Awake()
    {
        // 设置单例
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        // 初始化状态
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
    
    // 恢复生命值
    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("恢复了 " + amount + " 点生命值，当前生命值: " + currentHealth);
        
        // 可以添加UI更新
        // UpdateHealthUI();
    }
    
    // 受到伤害
    public void TakeDamage(int damage)
    {
        // 计算实际伤害（考虑防御力）
        int actualDamage = Mathf.Max(damage - defenseBonus, 1);
        currentHealth = Mathf.Max(currentHealth - actualDamage, 0);
        Debug.Log("受到 " + actualDamage + " 点伤害，当前生命值: " + currentHealth);
        
        // 可以添加UI更新
        // UpdateHealthUI();
        
        // 检查是否死亡
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    // 恢复法力值
    public void RestoreMana(int amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
        Debug.Log("恢复了 " + amount + " 点法力值，当前法力值: " + currentMana);
        
        // 可以添加UI更新
        // UpdateManaUI();
    }
    
    // 消耗法力值
    public bool UseMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            Debug.Log("消耗了 " + amount + " 点法力值，当前法力值: " + currentMana);
            
            // 可以添加UI更新
            // UpdateManaUI();
            return true;
        }
        
        Debug.Log("法力值不足");
        return false;
    }
    
    // 装备物品
    public void EquipItem(ItemData item)
    {
        if (item.itemType == ItemType.Equipment)
        {
            attackBonus += item.attackBonus;
            defenseBonus += item.defenseBonus;
            speedBonus += item.speedBonus;
            
            Debug.Log("装备了 " + item.itemName + "，攻击力+" + item.attackBonus + 
                      "，防御力+" + item.defenseBonus + "，速度+" + item.speedBonus);
        }
    }
    
    // 卸下装备
    public void UnequipItem(ItemData item)
    {
        if (item.itemType == ItemType.Equipment)
        {
            attackBonus -= item.attackBonus;
            defenseBonus -= item.defenseBonus;
            speedBonus -= item.speedBonus;
            
            Debug.Log("卸下了 " + item.itemName);
        }
    }
    
    // 死亡
    private void Die()
    {
        Debug.Log("玩家死亡");
        // 可以添加死亡动画、游戏结束逻辑等
    }
}