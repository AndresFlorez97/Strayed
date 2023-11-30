using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControlador : MonoBehaviour
{
    //Aca seleccionamos los panales,esto sirve para que al iniciar siempre abra el menu principal y no otro por error
    [SerializeField] private GameObject inicio;
    //Aca hay que depositar todos los demas panales que tenga le escena de menu
    public GameObject[] paneles;

    private void Awake()
    {
        inicio.SetActive(true);
        DesactivarEscenas();
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


    //Llamados de los botones,estas clases se van a poner en los botones para llamar a las escenas de juego

    public void CargarEscena1()
    {
        SceneManager.LoadScene(1);
    }
}
