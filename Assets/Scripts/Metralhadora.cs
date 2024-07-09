using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metralhadora : MonoBehaviour
{
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    // Define a distancia maxima para a mira
    [SerializeField] private float range = 17f;

    [SerializeField] private Transform engrenagem;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    [Header("Ataque")]
    // Define que vamos atirar um projetil por segundo
    [SerializeField] private float fireRate = 1f;
    // Determina a proximo momento de disparo (ja comeca atirando)
    [SerializeField] private float fireCountdown = 0f;
    private void Update()
    {
        // Executa a logica somente se tivermos um alvo
        if (target != null)
        {
            /* Codigo de rotacao */
            #region Rotacao
            Vector3 direcaoParaMirar = target.position - transform.position;

            // Pega a rotacao necessaria para virar para posicao do alvo
            Quaternion rotacaoNecessariaParaVirar = Quaternion.LookRotation(direcaoParaMirar);
            // Faz o calculo do vetor de rotacao
            Vector3 rotacaoParaMirar = rotacaoNecessariaParaVirar.eulerAngles;
            // Define a rotacao da engrenagem para este vetor de rotacao
            engrenagem.rotation = Quaternion.Euler(0f, rotacaoParaMirar.y, 0f);
            #endregion
            /* Codigo de rotacao */

            // Verifica se eh hora de atirar
            if (fireCountdown <= 0f)
            {
                Fire();
                // Atualiza a variavel fireCountdown para respeitar o fireRate
                fireCountdown = 1f / fireRate;
            }
            // Atualiza a variavel fireCountdown
            fireCountdown -= Time.deltaTime;

        }
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        // InvokeRepeating("Fire", 0f, 1.0f);
    }
    private void UpdateTarget()
    {
        // Encontra inimigos spawnados na cena
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy");
        // Variaveis para armazenar informacoes do inimigo mais proximo
        GameObject inimigoMaisProximo = null;
        float distanciaInimigoMaisProximo = Mathf.Infinity;
        // Faz loop no vetor de inimigos e descobre o inimigo mais proximo
        foreach (GameObject inimigo in inimigos)
        {
            float distanciaAteInimigo = Vector3.Distance(transform.position,
            inimigo.transform.position);
            if (distanciaAteInimigo < distanciaInimigoMaisProximo)
            {
                distanciaInimigoMaisProximo = distanciaAteInimigo;
                inimigoMaisProximo = inimigo;
            }
        }
        // Verifica se o inimigo mais proximo esta dentro da area de cobertura
        if (inimigoMaisProximo != null && distanciaInimigoMaisProximo < range)
        {
            target = inimigoMaisProximo.transform;
        }
        else
        {
            target = null; // limpa o alvo caso nao encontre um alvo valido
        }
    }

    void Fire()
    {
        // Aqui a referencia para a prefab do projetil se chama projetilPrefab e sua posicao de spawn eh o Transform firePoint

        GameObject projetilGObject = (GameObject)Instantiate(projectile, firePoint.position, firePoint.rotation);
        Projetil projetil = projetilGObject.GetComponent<Projetil>();

        if (projetil != null)
        {
            projetil.BuscarAlvo(target);

        }
        /* Método Antigo Feito por mim */
        #region Antigo
        // if (target != null)
        // {
        //     var myProjectile = Instantiate(projectile, aim.transform.position, Quaternion.identity);
        //     myProjectile.GetComponent<Projetil>().target = target;
        // }
        #endregion
        /* Método Antigo Feito por mim */
    }
    /* Demais definicoes dos metodos */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}