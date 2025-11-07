using UnityEngine;

// 添加命名空间（如果项目要求命名空间为Script，按提示补充）
namespace Script // 根据之前的命名空间提示添加，若不需要可删除
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerStats))]
    public class PrefabControl : MonoBehaviour
    {
        public static PrefabControl instance; // 单例模式
        public Transform dropPosition; // 丢弃物品的位置
        
        public float speed = 5f;
        public float rotationSpeed = 10f; // 新增：角色转向速度（让角色面朝移动方向）
        private CharacterController controller;
        private PlayerStats playerStats; // 玩家状态组件

        // 新增：缓存主相机，避免每帧调用Camera.main（提升性能）
        private Camera mainCamera;

        void Start()
        {
            // 设置单例实例
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            // 如果没有设置dropPosition，创建一个默认的
            if (dropPosition == null)
            {
                GameObject dropPoint = new GameObject("DropPosition");
                dropPoint.transform.SetParent(transform);
                dropPoint.transform.localPosition = new Vector3(0, 0.5f, 1); // 在玩家前方
                dropPosition = dropPoint.transform;
            }
            
            controller = GetComponent<CharacterController>();
            playerStats = GetComponent<PlayerStats>();
            gameObject.tag = "Player";

            // 缓存主相机，若找不到则提示
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("场景中没有主相机（Main Camera）！");
            }
            else
            {
                // 确保主相机有ThirdPersonCamera脚本并设置目标为当前玩家
                ThirdPersonCamera cameraScript = mainCamera.GetComponent<ThirdPersonCamera>();
                if (cameraScript != null)
                {
                    cameraScript.target = transform;
                }
                else
                {
                    Debug.LogWarning("主相机没有ThirdPersonCamera脚本！");
                }
            }

            if (controller == null)
            {
                Debug.LogError("角色控制器组件缺失！请添加CharacterController组件到玩家对象");
            }
        }

        void Update()
        {
            if (controller == null || mainCamera == null) return;

            // 获取输入（增加死区处理，避免微小输入导致的抖动）
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 input = new Vector2(horizontal, vertical).normalized;

            // 添加调试信息
            if (Input.anyKey)
            {
                Debug.Log($"输入检测: Horizontal={horizontal}, Vertical={vertical}, 输入向量={input}");
            }

            // 若没有输入，直接返回（避免角色在无输入时仍尝试移动）
            if (input.sqrMagnitude < 0.1f) return;

            // 计算移动方向（基于相机朝向）
            Vector3 forward = mainCamera.transform.forward;
            Vector3 right = mainCamera.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 movementDir = (forward * input.y + right * input.x).normalized;

            // 移动角色
            Vector3 movement = movementDir * speed * Time.deltaTime;
            controller.Move(movement);

            // 新增：让角色面朝移动方向（更自然的第三人称体验）
            Quaternion targetRotation = Quaternion.LookRotation(movementDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // 可选：碰撞检测逻辑
            // Debug.Log("碰撞到: " + hit.collider.gameObject.name);
        }
    }
}