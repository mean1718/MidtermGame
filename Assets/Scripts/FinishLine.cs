using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public float pulseSpeed = 3f;
    private Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        float s = 1f + Mathf.Sin(Time.time * pulseSpeed) * 0.04f;
        transform.localScale = new Vector3(baseScale.x * s, baseScale.y, baseScale.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Win();
        }
    }
}