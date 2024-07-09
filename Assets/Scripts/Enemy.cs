using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Movimentacao")]
    [SerializeField] private Transform targetPosition;
    private NavMeshAgent navMeshAgent;
    [Header("Atributos")]
    public float healthPoints = 2;
    private string difficultyLevel = "normal";
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.Find("Final").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        difficultyLevel = GameController.gameController.difficulty;
        SetDifficultyLevel(difficultyLevel);
        
    }

    void SetDifficultyLevel(string diff){
        switch (diff)
        {
            case "easy":
                healthPoints = 2;
                navMeshAgent.speed = 2;
                break;
            case "normal":
                healthPoints = 3;
                navMeshAgent.speed = 2.5f;
                break;
            case "hard":
                healthPoints = 4;
                navMeshAgent.speed = 6;
                break;
            default:
                healthPoints = 3;
                navMeshAgent.speed = 2.5f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = targetPosition.position;

        // Calcula a direcao para onde o inimigo vai
        Vector3 direcao = targetPosition.position - transform.position;

        // Verifica se cheagamos ao destino
        if (direcao.magnitude <= 1.0f)
        {
            Chegou();
        }
    }

    void Chegou()
    {
        UIController.uIController.SetLifes(-1);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (healthPoints > 1)
        {
            healthPoints -= damage;
        }
        else
        {
            UIController.uIController.SetMoney(10.0f);
            UIController.uIController.SetNumberOfEnemies(-1);
            Destroy(gameObject);
        }
    }
}
