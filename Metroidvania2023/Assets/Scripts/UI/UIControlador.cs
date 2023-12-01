using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIControlador : MonoBehaviour
{
    //Aca seleccionamos los panales,esto sirve para que al iniciar siempre abra el menu principal y no otro por error
    [SerializeField] private GameObject inicio;
    //Aca hay que depositar todos los demas panales que tenga le escena de menu
    public GameObject[] paneles;

    //Variables que se necesitan para contabilizar los animales rescatados.
    public TextMeshProUGUI contadorAnimales;

    private void Awake()
    {
        inicio.SetActive(true);
        DesactivarEscenas();
    }

    public void Start()
    {
        ActualizarContador();
    }
    //LLamada que desactiva todas las demas escenas
    void DesactivarEscenas()
    {
        foreach (GameObject panel in paneles)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }
   
    //Esta clase actualiza en pantalla los animales rescatados
    private void ActualizarContador()
    {
        // Actualiza el texto del contador con la cantidad de "Animales" recolectados
        contadorAnimales.text = DatosGlobales.animalesRecolectadosGlobal.ToString();
    }


    //Llamados de los botones,estas clases se van a poner en los botones para llamar a las escenas de juego

    public void CargarEscena1()
    {
        SceneManager.LoadScene(1);
    }
}
