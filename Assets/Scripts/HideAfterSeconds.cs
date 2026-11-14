using UnityEngine;

public class HideAfterSeconds : MonoBehaviour
{
    public float seconds = 6f;

    void Start()
    {
        Invoke(nameof(Hide), seconds);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}