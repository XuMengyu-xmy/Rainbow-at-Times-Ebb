using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // 要跟随的目标
    public float distance = 5.0f; // 与目标的距离
    public float height = 2.0f; // 在目标上方的高度
    public float smoothDampTime = 0.2f; // 平滑移动的阻尼时间
    private Vector3 velocity = Vector3.zero; // 平滑移动的速度

    void Start()
    {
        // 如果没有设置目标，尝试查找玩家对象
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                Debug.Log("自动找到玩家对象并设置为相机目标");
            }
            else
            {
                Debug.LogWarning("未找到带有Player标签的对象！");
            }
        }
    }

    void Update() 
    {
        if (target == null) return; // 增加空值判断，防止报错
        Vector3 targetPosition = target.TransformPoint(0, height, -distance);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothDampTime);
        transform.LookAt(target);
    }
}
