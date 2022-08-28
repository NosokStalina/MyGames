using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
public class GUI_Game_script : MonoBehaviour
{
    [Header("Компоненты TMPro - UI")]
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI HealthText;
    [Header("Компоненты Scripts")]
    public MainChest chest_script;
    private void FixedUpdate()
    {
        CheckingScores();
    }
    public void CheckingScores()
    {
        MoneyText.text = chest_script.Money.ToString();
        HealthText.text = chest_script.Health.ToString();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }
    public void ExitToTheGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
