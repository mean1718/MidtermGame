using UnityEngine;

public class UIBob : MonoBehaviour
{
    public float amplitude = 12f;
    public float speed = 2f;

    private RectTransform rt;
    private Vector2 startPos;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        startPos = rt.anchoredPosition;
    }

    void Update()
    {
        rt.anchoredPosition = startPos + Vector2.up * Mathf.Sin(Time.time * speed) * amplitude;
    }
}