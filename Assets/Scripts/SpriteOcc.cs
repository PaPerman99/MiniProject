using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteOcc : MonoBehaviour
{
    public SpriteRenderer[] blockers;  // 可能遮挡自己的 SpriteRenderer 列表
    private SpriteRenderer selfRenderer;

    private static readonly int GrayFactorID = Shader.PropertyToID("_GrayFactor");

    void Awake()
    {
        selfRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        bool occluded = false;
        foreach (var blocker in blockers)
        {
            if (blocker == null) continue;
            // 判断是否在同一 Sorting Layer 且遮挡排序更高
            if (blocker.sortingLayerID == selfRenderer.sortingLayerID &&
                blocker.sortingOrder > selfRenderer.sortingOrder)
            {
                occluded = true;
                break;
            }
        }
    
        // 设置灰度参数
        if (occluded)
        {
            selfRenderer.material.SetFloat(GrayFactorID, 1f); // 变灰
        }
        else
        {
            selfRenderer.material.SetFloat(GrayFactorID, 0f); // 正常颜色
        }
    }
}