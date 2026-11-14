using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Color activatedColor = new Color(1f, 0.92f, 0.2f);
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated || !other.CompareTag("Player")) return;
        activated = true;

        GameManager.Instance.SetCheckpoint(transform.position);
        AudioManager.Play(Sfx.Checkpoint);

        Renderer r = GetComponent<Renderer>();
        if (r != null) r.material.color = activatedColor;
    }
}