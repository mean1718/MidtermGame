using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Vector3 spinSpeed = new Vector3(20, 40, 10);
    public float floatHeight = 0.5f;
    public float floatSpeed = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        floatSpeed *= Random.Range(0.7f, 1.3f);
    }

    void Update()
    {
        transform.Rotate(spinSpeed * Time.deltaTime);
        transform.position = startPos + Vector3.up *
            Mathf.Sin(Time.time * floatSpeed + startPos.x) * floatHeight;
    }
}