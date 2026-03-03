using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool gameWin;

    public GameObject gameOverPanel;
    public GameObject levelWinPanel;

    public static int CurrentLevelIndex;
    public static int noOfPassingRings;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    public Slider ProggresBar;

    // Khai báo biến để cache (lưu trữ) HelixManager
    private HelixManager helixManager;

    public void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    private void Start()
    {
        noOfPassingRings = 0;
        gameOver = false;
        gameWin = false;
        Time.timeScale = 1f;

        // Tìm và lưu trữ HelixManager CHỈ MỘT LẦN duy nhất khi bắt đầu màn chơi
        helixManager = FindAnyObjectByType<HelixManager>();

        if (helixManager == null)
        {
            Debug.LogError("Cảnh báo: Không tìm thấy HelixManager trong Scene!");
        }
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(0);
            }
        }

        // Cập nhật text an toàn
        if (currentLevelText != null) currentLevelText.text = CurrentLevelIndex.ToString();
        if (nextLevelText != null) nextLevelText.text = (CurrentLevelIndex + 1).ToString();

        // Sử dụng biến đã lưu trữ để tính toán, cực kỳ nhẹ và không bao giờ bị lỗi Null nếu đã tìm thấy ở Start
        if (helixManager != null && ProggresBar != null)
        {
            int proggres = noOfPassingRings * 100 / helixManager.noOfRings;
            ProggresBar.value = proggres;
        }

        if (gameWin)
        {
            levelWinPanel.SetActive(true);
            if (Input.GetMouseButton(0))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex + 1);
                SceneManager.LoadScene(0);
            }
        }
    }

    public void ResetGameProgress()
    {
        // Xóa cụ thể key lưu Level
        PlayerPrefs.DeleteKey("CurrentLevelIndex");

        // Hoặc dùng PlayerPrefs.DeleteAll(); nếu bạn muốn xóa TẤT CẢ (âm thanh, điểm số cao nhất...) sau này

        // Reset lại biến hiển thị ngay lập tức
        CurrentLevelIndex = 1;

        // Tải lại màn chơi
        SceneManager.LoadScene(0);
    }
}