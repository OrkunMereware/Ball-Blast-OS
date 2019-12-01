using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera camera;
    public Vector2 targetRes;
    public float distance = 0.0f;

    [HideInInspector] public Vector3 startPos;

    void Awake()
    {
        startPos = camera.transform.position;
    }

    void Update()
    {
        camera.transform.position = startPos + camera.transform.forward * (1f - (targetRes.x / targetRes.y) / ((float)Screen.width / (float)Screen.height)) * distance;
    }

}