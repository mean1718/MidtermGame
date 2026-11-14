using System.Collections;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 12f;

    private Vector3 originalScale;
    private bool animating = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }

        AudioManager.Play(Sfx.Bounce);
        if (!animating) StartCoroutine(Squash());
    }

    IEnumerator Squash()
    {
        animating = true;
        float duration = 0.25f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.Sin((t / duration) * Mathf.PI);
            transform.localScale = new Vector3(
                originalScale.x * (1f + 0.25f * k),
                originalScale.y * (1f - 0.5f * k),
                originalScale.z * (1f + 0.25f * k));
            yield return null;
        }

        transform.localScale = originalScale;
        animating = false;
    }
}