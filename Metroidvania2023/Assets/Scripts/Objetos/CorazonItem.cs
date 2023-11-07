using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonItem : MonoBehaviour
{
    private int tiempo = 6;
    private float temporizador = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<MovimientoPlayer>().CurandoDa�o(1);
            gameObject.SetActive(false);
            temporizador = tiempo;
            
        }
    }
    private void Update()
    {
        // Verifica si el temporizador ha llegado a cero para reaparecer el coraz�n.
        if (!gameObject.activeSelf && temporizador > 0f)
        {
            temporizador -= Time.deltaTime;

            if (temporizador <= 0f)
            {
                // Activa el coraz�n para que reaparezca.
                gameObject.SetActive(true);
            }
        }
    }
}
