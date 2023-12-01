using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesbloqueoNiveles : MonoBehaviour
{
    public int animalesRequeridos = 20;
    public int numeroDeNivel = 2; // Este valor puede ser diferente para cada nivel
    public GameObject candado;

    private void Start()
    {
        VerificarDesbloqueoNivel();
    }

    private void Update()
    {
        if (DatosGlobales.nivelesDesbloqueados[numeroDeNivel])
        {
            candado.SetActive(false);

        }
    }

    private void VerificarDesbloqueoNivel()
    {
        if (DatosGlobales.animalesRecolectadosGlobal >= animalesRequeridos)
        {
            DatosGlobales.nivelesDesbloqueados[numeroDeNivel] = true;
        }
    }

    public void OnClick()
    {
        if (DatosGlobales.nivelesDesbloqueados[numeroDeNivel])
        {
            CargarNivel();
        }
        else
        {
            Debug.LogWarning($"¡Nivel {numeroDeNivel} no desbloqueado!");
            // Puedes mostrar un mensaje o realizar otras acciones si intenta ingresar a un nivel no desbloqueado.
        }
    }

    private void CargarNivel()
    {
        SceneManager.LoadScene(2);
    }
}
