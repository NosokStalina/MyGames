using UnityEngine;
public class MainChest : MonoBehaviour
{
    [Header("���������� �������")]
    public uint Health;
    public ulong Money;
    public bool GameOver = false;
    [Header("���������� �������")]
    public GameObject GameOverPanel;
    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.SetQualityLevel(0);
    }
    private void FixedUpdate()
    {
        CheckHealth();
    }
    public void GetDamage(uint damage)
    {
        if (damage >= Health)
            Health = 0;
        else
            Health -= damage;
    }
    void CheckHealth()
    {
        if (Health <= 0 && GameOver == false)
        {
            GameOver = true; Time.timeScale = 0; GameOverPanel.SetActive(true);
        }
    }
}
