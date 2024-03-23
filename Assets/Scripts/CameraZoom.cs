using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float zoomSpeed = 1.0f;
    [SerializeField] private float minZoom = 1.0f;
    [SerializeField] private float maxZoom = 5.0f;

    void Update()
    {
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(camera.orthographicSize - zoomDelta, minZoom, maxZoom);
    }
}
