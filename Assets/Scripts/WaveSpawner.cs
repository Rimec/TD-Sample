using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeBetweenWave = 5.0f;
    [SerializeField] private float countdown = 5.0f;
    [SerializeField] private int waveNumber = 0;
    [SerializeField] private int maxWaveNumber = 0;
    [SerializeField] private int enemiesToSpawn = 0;
    [SerializeField] private string difficultyLevel = "normal";

    [SerializeField] private int[] waveEnemiesNumberEasy = {1, 1, 2, 2, 4, 4, 4, 5, 5, 7}; 
    [SerializeField] private int[] waveEnemiesNumberNormal = {2, 2, 3, 4, 6, 6, 6, 7, 7, 10};
    [SerializeField] private int[] waveEnemiesNumberHard = {4, 4, 5, 5, 8, 8, 8, 10, 10, 13}; 

    void Start(){
        difficultyLevel = GameController.gameController.difficulty;
        maxWaveNumber = UIController.uIController.maxWaveNumber;
        SetDifficultyLevel(difficultyLevel);
    }
    void SetDifficultyLevel(string diff){
        switch (diff)
        {
            case "easy":
                enemiesToSpawn = waveEnemiesNumberEasy[waveNumber];
                break;
            case "normal":
                enemiesToSpawn = waveEnemiesNumberNormal[waveNumber];
                break;
            case "hard":
                enemiesToSpawn = waveEnemiesNumberHard[waveNumber];
                break;
            default:
                enemiesToSpawn = waveEnemiesNumberNormal[waveNumber];
                break;
        }
    }

    void Update()
    {
        if (waveNumber < maxWaveNumber)
        {
            if (countdown <= 0.0f)
            {
                SetDifficultyLevel(difficultyLevel);
                // DifficultyByWaveNumber(waveNumber);
                SpawnWave(enemiesToSpawn);
                countdown = timeBetweenWave;
            }
            
            countdown -= Time.deltaTime;
            UIController.uIController.timeToSpawn = countdown;
        }
    }
    private void DifficultyByWaveNumber(int _waveN){
        // switch (_waveN)
        // {
        //     case 3:
        //         enemiesToSpawn += 2;
        //         break;
        //     case 6:
        //         enemiesToSpawn += 4;
        //         break;
        //     case 8:
        //         enemiesToSpawn += 6;
        //         break;
        //     default:
        //         break;
        // }
    }
    private void SpawnWave(int numberOfEnemies)
    {
        waveNumber++;
        UIController.uIController.waveNumber = waveNumber;
        // enemysSpawned = Random.Range(1, 10);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        UIController.uIController.SetNumberOfEnemies(1);
        Vector3 pos = spawnPoint.position;
        pos.y+= 0.5f;
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
