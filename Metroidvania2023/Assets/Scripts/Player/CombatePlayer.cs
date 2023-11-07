using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatePlayer : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private int dañoGolpe;
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
        animacion.SetTrigger("Ataca");

        Collider2D[] golpe = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach(Collider2D colisionador in golpe)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<ControladorEnemigo>().RecibeDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
