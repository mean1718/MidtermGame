using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float knockback = 8f;
    public bool loseLife = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody rb = collision.rigidbody;
        if (rb != null)
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir.y = 0f;
            dir = (dir.normalized + Vector3.up * 0.4f).normalized;
            rb.AddForce(dir * knockback, ForceMode.Impulse);
        }

        AudioManager.Play(Sfx.Hit);
        CameraFollow cam = FindFirstObjectByType<CameraFollow>();
        if (cam != null) cam.Shake(0.15f, 0.12f);

        if (loseLife) GameManager.Instance.LoseLife();
    }
}