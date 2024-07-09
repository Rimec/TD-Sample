using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    // Define a distancia maxima para a mira
    [SerializeField] private float range = 12f;

    // [SerializeField] private Transform engrenagem;
    // [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [Header("Ataque")]
    // Define que vamos atirar um projetil por segundo
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private float fireDamage = 0.075f;
    // Determina a proximo momento de disparo (ja comeca atirando)
    [SerializeField] private float fireCountdown = 0f;
    [SerializeField] private LineRenderer line;
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
            firePoint.rotation = Quaternion.Euler(rotacaoParaMirar);
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
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
    }

    void Fire()
    {
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, target.position);
        target.gameObject.GetComponent<Enemy>().TakeDamage(fireDamage);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
