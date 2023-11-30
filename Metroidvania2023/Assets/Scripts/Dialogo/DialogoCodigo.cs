using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoCodigo : MonoBehaviour
{
    [TextArea(3, 10)] // Especifica el área de texto con un mínimo de 3 líneas y un máximo de 10 líneas
    public string[] mensajes;
    //public string mensaje = "Este es el texto que se mostrará"; // Texto que queremos que se muestre

    public TextMeshProUGUI textoPanel;  // Referencia al Texto para escribir
    public GameObject panel; // Referencia al panel

    private bool playerEnArea = false;
    private int mensajeActual = 0; // Índice del mensaje actual

    private void Start()
    {
        //ActualizarMensaje();
        //textoPanel.text = mensaje; // Configura el texto predeterminado
        panel.SetActive(false); // Oculta el panel al inicio
    }

    private void Update()
    {
        if (playerEnArea && Input.GetKeyDown(KeyCode.U)) // Puedes cambiar KeyCode.Space a la tecla que desees
        {
            SiguienteMensaje();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnArea = true;
            MostrarPanel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnArea = false;
            OcultarPanel();
        }
    }

    private void MostrarPanel()
    {
        panel.SetActive(true);
        ActualizarMensaje();
    }

    private void OcultarPanel()
    {
        panel.SetActive(false);
    }

    private void ActualizarMensaje()
    {
        if (mensajeActual < mensajes.Length)
        {
            textoPanel.text = mensajes[mensajeActual];
        }
    }

    public void SiguienteMensaje()
    {
        mensajeActual++;
        if (mensajeActual < mensajes.Length)
        {
            ActualizarMensaje();
        }
        else
        {
            OcultarPanel();
        }
    }
}
