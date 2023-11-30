using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatePlayer : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private int da�oGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoProximoAtaque;
    private Animator animacion;

    private void Start()
    {
        animacion = GetComponent<Animator>();
    }

    private void Update()
    {
        if(tiempoProximoAtaque > 0)
        {
            tiempoProximoAtaque -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && tiempoProximoAtaque <= 0)
        {
            Golpe();
            tiempoProximoAtaque = tiempoEntreAtaques;
        }
    }
    private void Golpe()
    {
        
        Collider2D[] golpe = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        animacion.SetTrigger("Ataca");

        foreach (Collider2D colisionador in golpe)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                //colisionador.transform.GetComponent<ControladorEnemigo>().RecibeDa�o(da�oGolpe);
                if (colisionador.CompareTag("Enemigo"))
                {
                    ControladorEnemigo controladorEnemigo = colisionador.GetComponent<ControladorEnemigo>();
                    if (controladorEnemigo != null)
                    {
                        controladorEnemigo.RecibeDa�o(da�oGolpe);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
