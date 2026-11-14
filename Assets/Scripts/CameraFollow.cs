using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 7, -9);
    public float smoothSpeed = 5f;

    [Header("Speed FOV Effect")]
    public float baseFOV = 60f;
    public float maxFOV = 72f;
    public float fovSpeedRef = 12f;

    private Camera cam;
    private Rigidbody targetRb;
    private float shakeTime = 0f;
    private float shakeMagnitude = 0f;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (target != null) targetRb = target.GetComponent<Rigidbody>();
    }

    public void Shake(float duration, float magnitude)
    {
        shakeTime = duration;
        shakeMagnitude = magnitude;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target);

        if (shakeTime > 0f)
        {
            transform.position += Random.insideUnitSphere * shakeMagnitude;
            shakeTime -= Time.deltaTime;
        }

        if (cam != null && targetRb != null)
        {
            float t = Mathf.Clamp01(targetRb.linearVelocity.magnitude / fovSpeedRef);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, Mathf.Lerp(baseFOV, maxFOV, t), 3f * Time.deltaTime);
        }
    }
}