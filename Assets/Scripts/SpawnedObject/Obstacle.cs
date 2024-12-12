using System.Collections;
using UnityEngine;

public class Obstacle : SpawnedObject
{
    private int sizeLength = 4;


    protected override void ProcessOverlapping()
    {
        Vector3 size = new Vector3(boxCollider.size.x + sizeLength, boxCollider.size.y, boxCollider.size.z + sizeLength);
        Collider[] hitColliders = new Collider[1];
        hitColliders = Physics.OverlapBox(boxCollider.bounds.center, size / 2, transform.rotation);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider == boxCollider)
                continue;
            if ((layerMask.value & 1 << hitCollider.gameObject.layer) > 0)
            {
                Debug.Log("Obstacle Overlapped" + gameObject.name);
                StaticEventHandler.CallObstacleOverlappedEvent(this);
                break;
            }

        }
    }


    private void OnDrawGizmos()
    {
        if (boxCollider == null) return;

        // Tính toán kích thước box
        Vector3 size = new Vector3(
            boxCollider.size.x + sizeLength,
            boxCollider.size.y,
            boxCollider.size.z + sizeLength
        );

        // Lưu trữ ma trận gốc của Gizmos
        Matrix4x4 originalMatrix = Gizmos.matrix;

        // Thiết lập Gizmos theo hệ local của object
        Gizmos.matrix = Matrix4x4.TRS(
            boxCollider.transform.position,  // Vị trí của object
            boxCollider.transform.rotation,  // Xoay của object
            Vector3.one                       // Scale mặc định
        );

        // Đặt màu cho Gizmos
        Gizmos.color = Color.blue;

        // Vẽ box dựa trên hệ local
        Gizmos.DrawWireCube(boxCollider.center, size);

        // Khôi phục ma trận gốc của Gizmos
        Gizmos.matrix = originalMatrix;
    }
}
