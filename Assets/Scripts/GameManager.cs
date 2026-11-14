using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text timerText;
    public TMP_Text centerText;

    [Header("Player")]
    public Transform player;
    public int startingLives = 3;
    public float respawnCooldown = 1f;

    private int score = 0;
    private int lives;
    private int coinsCollected = 0;
    private int totalCoins = 0;
    private float timer = 0f;
    private float lastLifeLostTime = -10f;
    private bool gameOver = false;
    private bool won = false;
    private bool paused = false;
    private Vector3 checkpointPosition;
    private CameraFollow cam;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        lives = startingLives;
        totalCoins = FindObjectsByType<Coin>(FindObjectsSortMode.None).Length;
        cam = FindFirstObjectByType<CameraFollow>();

        if (player != null) checkpointPosition = player.position;
        if (centerText != null) centerText.text = "";

        UpdateUI();
    }

    void Update()
    {
        Keyboard k = Keyboard.current;

        if (k != null)
        {
            // Restart any time with R
            if (k.rKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }

            // Pause with Escape or P
            if (!gameOver && !won && (k.escapeKey.wasPressedThisFrame || k.pKey.wasPressedThisFrame))
            {
                TogglePause();
            }
        }

        if (!gameOver && !won && !paused)
        {
            timer += Time.deltaTime;
            UpdateTimer();
        }
    }

    // ---------- Public API (used by other scripts) ----------

    public void AddScore(int points)
    {
        if (gameOver || won) return;

        score += points;
        coinsCollected++;
        UpdateUI();
        StartCoroutine(PunchText(scoreText));
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position + Vector3.up * 1.2f;
        StartCoroutine(FlashMessage("CHECKPOINT!", 1.2f));
    }

    public void LoseLife()
    {
        if (gameOver || won) return;
        if (Time.time - lastLifeLostTime < respawnCooldown) return;
        lastLifeLostTime = Time.time;

        lives--;
        AudioManager.Play(Sfx.Fall);
        if (cam != null) cam.Shake(0.35f, 0.25f);

        if (lives <= 0)
        {
            lives = 0;
            gameOver = true;
            UpdateUI();
            AudioManager.Play(Sfx.GameOver);
            if (centerText != null)
                centerText.text = "GAME OVER\n<size=50%>Score: " + score + "\nPress R to Restart</size>";
            return;
        }

        RespawnPlayer();
        UpdateUI();
        StartCoroutine(PunchText(livesText));
    }

    public void Win()
    {
        if (won || gameOver) return;
        won = true;

        AudioManager.Play(Sfx.Win);

        // Bonus points for finishing fast
        int timeBonus = Mathf.Max(0, Mathf.RoundToInt((60f - timer) * 2f));
        score += timeBonus;

        // Stars: 1 for finishing, +1 all coins, +1 no lives lost
        int stars = 1;
        if (coinsCollected >= totalCoins) stars++;
        if (lives == startingLives) stars++;

        // Best time
        float best = PlayerPrefs.GetFloat("BestTime", 0f);
        string record = "";
        if (best <= 0f || timer < best)
        {
            PlayerPrefs.SetFloat("BestTime", timer);
            PlayerPrefs.Save();
            best = timer;
            record = "  NEW RECORD!";
        }

        UpdateUI();

        if (centerText != null)
        {
            centerText.text =
                "LEVEL COMPLETE!\n<size=45%>" +
                "Score: " + score + "  (time bonus +" + timeBonus + ")\n" +
                "Coins: " + coinsCollected + "/" + totalCoins + "\n" +
                "Time: " + FormatTime(timer) + record + "\n" +
                "Best: " + FormatTime(best) + "\n" +
                "Rating: " + stars + " / 3 stars\n" +
                "Press R to Play Again</size>";
        }
    }

    // ---------- Internals ----------

    void TogglePause()
    {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        if (centerText != null)
            centerText.text = paused ? "PAUSED\n<size=45%>Press Esc to continue\nPress R to restart</size>" : "";
    }

    void RespawnPlayer()
    {
        if (player == null) return;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        player.position = checkpointPosition;
        Physics.SyncTransforms();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (livesText != null) livesText.text = "Lives: " + lives;
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (timerText != null) timerText.text = FormatTime(timer);
    }

    string FormatTime(float t)
    {
        int minutes = (int)(t / 60f);
        float seconds = t % 60f;
        return minutes.ToString("00") + ":" + seconds.ToString("00.0");
    }

    IEnumerator PunchText(TMP_Text text)
    {
        if (text == null) yield break;

        Transform t = text.transform;
        float elapsed = 0f;
        float duration = 0.25f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float k = Mathf.Sin((elapsed / duration) * Mathf.PI);
            t.localScale = Vector3.one * (1f + 0.35f * k);
            yield return null;
        }
        t.localScale = Vector3.one;
    }

    IEnumerator FlashMessage(string msg, float seconds)
    {
        if (centerText == null) yield break;
        centerText.text = msg;
        yield return new WaitForSeconds(seconds);
        if (!gameOver && !won && !paused) centerText.text = "";
    }
}