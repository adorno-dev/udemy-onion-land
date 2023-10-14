using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI ammoText;

    private int scoreValue;

    [SerializeField] private GameObject tryAgainButton;

    [Space]
    [SerializeField] private GunController gunController;

    [Header("Reload details")]
    [SerializeField] private BoxCollider2D reloadWindow;
    [SerializeField] private UI_ReloadButton[] reloadButtons;
    [SerializeField] private int reloadSteps;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        reloadButtons = GetComponentsInChildren<UI_ReloadButton>(true);
    }

    private void Update()
    {
        if (Time.time >= 1)
            timerText.text = Time.time.ToString("#,#");
        
        if (Input.GetKeyDown(KeyCode.R))
            OpenReloadUI();
    }

    public void OpenReloadUI()
    {
        float randomX, randomY;

        for (int i = 0; i < reloadButtons.Length; i++)
        {
            reloadButtons[i].gameObject.SetActive(true);

            randomX = Random.Range(reloadWindow.bounds.min.x, reloadWindow.bounds.max.x);
            randomY = Random.Range(reloadWindow.bounds.min.y, reloadWindow.bounds.max.y);

            reloadButtons[i].transform.position = new Vector2(randomX, randomY);
        }

        reloadSteps = reloadButtons.Length;

        Time.timeScale = .4f;
    }

    public void AttemptToReload()
    {
        if (reloadSteps > 0)
            reloadSteps--;
        
        if (reloadSteps <= 0)
            gunController.ReloadGun();
    }

    public void AddScore()
    {
        scoreValue++;
        scoreText.text = scoreValue.ToString();
    }

    public void UpdateAmmoInfo(int currentBullets, int maxBullets)
    {
        ammoText.text = $"{currentBullets}/{maxBullets}";
    }

    public void OpenEndScreen()
    {
        Time.timeScale = 0;
        tryAgainButton.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
