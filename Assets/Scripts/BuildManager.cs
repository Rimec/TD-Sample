using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Aqui estamos implementando o padrao de projeto Singleton.
    // A ideia eh que estamos garantindo que existe *somente um* BuildManager na cena.
    public static BuildManager instance;

    public GameObject metralhadoraAConstruir;

    // Define qual a prefab vamos utilizar para construir uma nova metralhadora
    [SerializeField] private GameObject metralhadoraPadraoPrefab;

    void Awake()
    {
        // Verifica se ja existe uma instancia de BuildManager na cena
        if (instance != null)
        {
            Debug.LogError("Mais de uma instancia de BuildManager rodando!");
            return;
        }
        instance = this;
    }

    void Start()
    {
        metralhadoraAConstruir = metralhadoraPadraoPrefab;
    }

    public GameObject GetMetralhadoraAConstruir()
    {
        return metralhadoraAConstruir;
    }

}
