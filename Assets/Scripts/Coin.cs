using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 10;
    public float rotationSpeed = 100f;

    [Header("Juice")]
    public float bobHeight = 0.2f;
    public float bobSpeed = 3f;
    public GameObject collectEffect;   // optional particle prefab

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        float y = Mathf.Sin(Time.time * bobSpeed + startPos.z) * bobHeight;
        transform.position = startPos + new Vector3(0, y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.AddScore(coinValue);
        AudioManager.Play(Sfx.Coin);

        if (collectEffect != null)
        {
            GameObject fx = Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(fx, 2f);
        }

        Destroy(gameObject);
    }
}