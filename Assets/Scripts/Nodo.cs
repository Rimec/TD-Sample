using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Este script vai controlar se existe ou nao uma metralhadora (torre) 
em um Nodo do cenario. Alem disso, ele vai controlar inputs do usuario 
como clique e passar o mouse por cima de um Nodo */
public class Nodo : MonoBehaviour
{
    #region Atributos
    [SerializeField] private Color hoverColor;
    private Color defaultColor;
    private Renderer rend;
    #endregion
    [SerializeField] private GameObject metralhadora;

    void Start()
    {
        // Pega o Renderer do Nodo
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
    }
    void OnMouseEnter()
    {
        // Vamos alterar o material no Nodo quando passarmos o mouse por cima
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        // Volta com a cor padrao quando tirarmos o mouse de cima do Nodo
        rend.material.color = defaultColor;
    }

    void OnMouseDown()
    {
        if (UIController.uIController.money < UIController.uIController.price)
        {
            Debug.Log("Impossivel criar metralhadora, pois o não tem dinheiro o suficiente");
            return;
        }
        // Verifica se ja existe uma metralhadora no Nodo
        if (metralhadora != null)
        {
            Debug.Log("Impossivel criar metralhadora, pois o Nodo já está ocupado");
            return;
        }
        // Constroi uma metralhadora
        // Note que estamos utilizando o padrao de projeto Singleton aqui
        UIController.uIController.SetMoney(-UIController.uIController.price);
        GameObject metralhadoraAConstruir = BuildManager.instance.GetMetralhadoraAConstruir();

        // Instancia a metralhadora
        metralhadora = (GameObject)Instantiate(metralhadoraAConstruir, transform.position, transform.rotation);
    }
}
