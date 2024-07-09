using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController uIController;

    [Header("Waves")]
    public float timeToSpawn = 0.0f;
    [SerializeField] private Text timeTxt;
    [SerializeField] private Text waveTxt;
    [SerializeField] private Text maxWaveTxt;

    [Header("Money")]
    [SerializeField] private Text moneyTxt;
    public float money = 400.0f;
    public float price = 100.0f;

    [Header("Lifes")]
    [SerializeField] private Text lifesTxt;
    [SerializeField] private float lifes = 3.0f;
    [SerializeField] private float Maxlifes = 3.0f;

    [Header("Panels")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Image healthBar;

    [Header("Variables")]
    [SerializeField] private bool isPaused;
    public int maxWaveNumber = 10;
    public int waveNumber;
    public int enemeisCount;

    [Header("Guns")]
    [SerializeField] private GameObject machineGun;
    [SerializeField] private GameObject rayGun;
    [SerializeField] private Text machineGunValueTxt;
    [SerializeField] private Text rayGunValueTxt;
    void Awake()
    {
        uIController = this;
    }
    void Start()
    {
        InitialState();
    }
    void Update()
    {
        timeTxt.text = timeToSpawn.ToString("0.00");
        moneyTxt.text = money.ToString("0.00");
        waveTxt.text = waveNumber.ToString();
        CheckWin();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                UnpauseGame();
            }
            else{
                PauseGame();
            }
        }
    }
    private void CheckWin()
    {
        if ((waveNumber == maxWaveNumber) && (enemeisCount == 0))
        {
            WinGame();
        }
    }
    public void SetMoney(float _amount)
    {
        money += _amount;
    }
    public void SetLifes(float _amount)
    {
        lifes += _amount;
        float value = lifes * 1 / Maxlifes;
        healthBar.fillAmount = value;
        if (lifes < 1)
        {
            LoseGame();
        }
    }

    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        losePanel.SetActive(true);
    }
    public void WinGame()
    {
        Time.timeScale = 0.0f;
        winPanel.SetActive(true);
    }
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
    }
    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }

    public void TryAgainBtn()
    {
        SceneManager.LoadScene("TowerDeffense");
    }
    public void MenuBtn()
    {
        SceneManager.LoadScene("Menu");
    }

    public void InitialState()
    {
        Time.timeScale = 1.0f;
        maxWaveTxt.text = maxWaveNumber.ToString();
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void SetNumberOfEnemies(int number){
        enemeisCount += number;
    }

    public void MachineGunBt(float value){
        BuildManager.instance.metralhadoraAConstruir = machineGun;
        machineGunValueTxt.text = "$ " + value.ToString("0.00");
        price = value;
    }
    public void RayGunBt(float value){
        BuildManager.instance.metralhadoraAConstruir = rayGun;
        rayGunValueTxt.text = "$ " + value.ToString("0.00");
        price = value;
    }
    
    

}
