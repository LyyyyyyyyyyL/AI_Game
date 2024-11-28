using UnityEngine;

public class AdjustPivot : MonoBehaviour
{
    void Start()
    {
        // 获取对象的 MeshFilter
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) return;

        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;

        // 获取当前网格的 Bounds
        Bounds bounds = mesh.bounds;

        // 计算右边界位置
        Vector3 newPivotPosition = new Vector3(bounds.max.x, 0, 0);

        // 移动所有顶点到新的 Pivot
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= newPivotPosition;
        }

        // 应用修改
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;

        // 移动对象的位置到新 Pivot
        transform.position += transform.TransformPoint(newPivotPosition) - transform.position;
    }
}
