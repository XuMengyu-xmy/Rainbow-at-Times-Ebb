# 背包UI设置指南

## 方法一：自动创建（推荐）

1. 在场景中创建一个空的GameObject，命名为"InventoryUIManager"
2. 将新创建的InventoryUI脚本拖拽到这个GameObject上
3. 运行游戏，脚本会自动创建基本的背包UI结构
4. 停止游戏，检查创建的UI结构

## 方法二：手动创建

### 1. 创建Canvas
- 在Hierarchy窗口右键 -> UI -> Canvas
- 确保Canvas的Render Mode设置为"Screen Space - Overlay"

### 2. 创建背包面板
- 在Canvas下右键 -> UI -> Panel，命名为"InventoryPanel"
- 设置Panel的RectTransform：
  - Anchor: 拉伸并居中
  - Anchor Min: (0.3, 0.3)
  - Anchor Max: (0.7, 0.7)
- 设置Panel的Image颜色为半透明黑色（例如：RGBA(0.1, 0.1, 0.1, 0.8)）
- 取消勾选InventoryPanel的active复选框（初始状态为隐藏）

### 3. 创建物品格子容器
- 在InventoryPanel下右键 -> Create Empty，命名为"SlotsContainer"
- 添加GridLayoutGroup组件：
  - Cell Size: (60, 60)
  - Spacing: (5, 5)
  - Padding: (10, 10, 10, 10)
- 设置SlotsContainer的RectTransform：
  - Anchor: 拉伸
  - Left/Right/Top/Bottom: 10

### 4. 创建物品格子预制体
- 在Hierarchy中创建一个UI -> Image，命名为"Slot"
- 设置Image颜色为深灰色（例如：RGBA(0.2, 0.2, 0.2, 0.8)）
- 设置RectTransform大小为(60, 60)
- 添加InventorySlot脚本组件
- 将这个Slot拖拽到Project窗口创建预制体
- 删除Hierarchy中的Slot对象

## 设置Inventory组件

1. 在场景中创建一个空的GameObject，命名为"InventoryManager"
2. 将Inventory脚本拖拽到这个GameObject上
3. 在Inspector中设置以下引用：
   - Inventory Panel: 拖拽InventoryPanel到这个字段
   - Slots Parent: 拖拽SlotsContainer到这个字段
   - Slot Prefab: 拖拽Slot预制体到这个字段

## 测试

1. 运行游戏
2. 按I键，应该能看到背包UI显示/隐藏
3. 查看控制台输出，确认没有错误信息

## 常见问题

1. **"inventoryPanel为空"错误**：确保在Inventory组件的Inspector中设置了Inventory Panel字段
2. **按键无响应**：确保没有其他UI元素阻挡输入，检查控制台是否有"I键被按下"的日志
3. **UI显示异常**：检查Canvas设置和RectTransform属性

## 高级设置

如果需要更复杂的背包UI，可以考虑：
- 添加拖拽功能
- 添加物品详细信息显示
- 添加分类标签页
- 添加物品使用按钮