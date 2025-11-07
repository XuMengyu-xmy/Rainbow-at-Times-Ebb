// ItemPickup.cs
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item; // 在Inspector中关联对应的ItemData资产

    private void Start()
    {
        // 检查ItemData是否已设置
        if (item == null)
        {
            Debug.LogError($"物品拾取对象 {gameObject.name} 没有设置ItemData引用");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查进入触发器的是否是玩家
        if (other.CompareTag("Player"))
        {
            // 检查ItemData是否已设置
            if (item == null)
            {
                Debug.LogError("无法拾取物品：ItemData未设置");
                return;
            }
            
            // 获取玩家身上的背包脚本
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                // 尝试添加物品到背包
                bool wasPickedUp = inventory.AddItem(item);

                // 如果成功拾取，则销毁场景中的物品
                if (wasPickedUp)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogError("玩家对象上没有找到Inventory组件");
            }
        }
    }
}

