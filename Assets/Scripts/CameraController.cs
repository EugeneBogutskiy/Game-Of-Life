using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float zoomSpeed = 3.0f; // скорость приближения камеры

    void LateUpdate()
    {
        float input = Input.GetAxis("Mouse ScrollWheel"); // вращаем колесо мыши

        if (input != 0)
        {
            Camera.main.orthographicSize -= input * zoomSpeed;

            if (Camera.main.orthographicSize <= 0.5) // проверка, чтобы размер камеры не ушел в минус
            {
                Camera.main.orthographicSize = 0.5f;
            }

            if (Camera.main.orthographicSize >= 3)
            {
                Camera.main.orthographicSize = 3f;
            }
        }
    }
}
