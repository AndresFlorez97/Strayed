using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalesItems : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DatosGlobales.animalesRecolectadosGlobal++;
            gameObject.SetActive(false);
        }
    }
}
