using UnityEngine;
namespace CSharp
{

    public class DragObject : MonoBehaviour
    {
        private bool isDragging = false;
        private Vector3 offset;

        private void OnMouseDown()
        {
            // 当点击到物体时触发
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();
        }

        private void OnMouseUp()
        {
            // 当释放点击时触发
            isDragging = false;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }

        private void Update()
        {
            if (isDragging)
            {
                // 当持续拖动时更新物体位置
                transform.position = GetMouseWorldPosition() + offset;
            }
        }
    }
}