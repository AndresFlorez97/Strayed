using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MovimientoPlayer : MonoBehaviour
{
    //Movimiento
    [Header("Movimiento")]
    public float velocidadMovimiento = 5f;
    //Salto
    [Header("Salto")]
    public float saltoFuerza;
    private bool tocandoSuelo;
    public int saltosExtras;
    private int saltosRestantes;
    //Achicar
    [Header("Achicar")]
    private Vector2 escalaOriginal;
    private bool seAchico = false;
    public float escalaChiquito = 0.5f;
    //Escalar
    [Header("Escalar")]
    public float velocidadEscalda = 5f;
    private bool tocandoPared;
    //Dash
    [Header("Dash")]
    public float velocidadDash = 5f;
    public float tiempoDash;
    private float gravedadInical;
    private bool puedeDashear = true;
    private bool sepuedeMover = true;
    //Planear
    [Header("Planear")]
    public float velocidadCaida = 2.5f;
    //bool isFalling = false;
    //Vidas
    [Header("Vidas")]
    public int vidaMaxima;
    private int vidaRestante;
    public TextMeshProUGUI contadorVidas;
    [Header("UI")]
    //Pausa
    public GameObject panelPausa;
    //GameOver
    public GameObject panelGameOver;
    [Header("Combate")]
    public Vector2 velocidadRebote;
    public float tiempoPerdidaControl;

    private Collider2D col;
    private Rigidbody2D rb2d;
    private Animator animacion;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        saltosRestantes = saltosExtras;
        escalaOriginal = transform.localScale;
        gravedadInical = rb2d.gravityScale;
        vidaRestante = vidaMaxima;
        Time.timeScale = 1f;
    }

    void Update()
    {
        //Controla la vida para verificar cuando muere
        #region GameOver
        if (vidaRestante <= 0)
        {
            GameOver();
        }
        #endregion
        //Invoca la pausa
        #region Pausar
        if (Input.GetKeyDown(KeyCode.J))
        {
            Pausa();
        }
        #endregion

        //Manejo del Dash
        #region Dash
        //Invoca la funciona del Dash
        if (Input.GetKeyDown(KeyCode.E) && puedeDashear) 
        {
            StartCoroutine(Dash());
        }
        #endregion

        //Manejan el salto
        #region Salto
        //Chequea si esta en el piso
        tocandoSuelo = col.IsTouchingLayers(LayerMask.GetMask("Ground"));
  
        // Reinicia los saltos si el personaje está en el suelo.
        if (tocandoSuelo)
        {
            saltosRestantes = saltosExtras;
        }
        //Funcion de saltar al apretar la tecla espacio,Ademas se le agrega la funcion de doble salto 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Realizo el salto basico
            if (tocandoSuelo)
            {
                Salto();
            }
            //Funciona para hacer doble salto,despues de le deberia agregar la variable de obtener la habilidad
            if(saltosRestantes > 0)
            {
                Salto();
                Debug.Log("Hizo un doble salto");
            }
        }
        #endregion

        //Manje la habilidad de hacerce chiquito
        #region Achicar
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (seAchico)
            {
                //vuelve a la escala normal
                transform.localScale = escalaOriginal;
                seAchico = false;
            }
            else
            {
                //se achica
                transform.localScale = escalaOriginal * escalaChiquito;
                seAchico = true;
            }
        }
        #endregion
    }

    void FixedUpdate()
    {
        
        //Maneja la habilidad de escalar
        #region Escalada
        float inputMovimientoY = Input.GetAxis("Vertical");
        tocandoPared = col.IsTouchingLayers(LayerMask.GetMask("Wall"));
        animacion.SetFloat("EscaladaAnim", inputMovimientoY);
        if (tocandoPared && inputMovimientoY > 0)
        {
            rb2d.velocity = new Vector2(0, inputMovimientoY * velocidadEscalda);
            Debug.Log("Esta escalando");
        }

        #endregion

        //Manejo de Planear
        #region Planear

        bool isFalling = rb2d.velocity.y < 0 && !tocandoSuelo;
        // Aplicar una gravedad personalizada para planear en el aire.
        if (Input.GetKey(KeyCode.Q) && isFalling)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -velocidadCaida, float.MaxValue));
            Debug.Log("Estaria Planeando");
        }
        #endregion

        //Manejan el Movimiento
        #region Movimiento
        float inputMovimiento = Input.GetAxis("Horizontal");
        if (sepuedeMover)
        {
            rb2d.velocity = new Vector2(inputMovimiento * velocidadMovimiento, rb2d.velocity.y);
        }
        //Modificar direccion del sprite
        if (inputMovimiento > 0 && sepuedeMover == true)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (inputMovimiento < 0 && sepuedeMover == true)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        #endregion

        //Manejan las animaciones
        #region Animaciones
        //Maneja la transicion de animacion a correr
        animacion.SetFloat("HorizontalAnim", Mathf.Abs(inputMovimiento));
        //Setea la bool para ver si esta cayendo
        bool isFalling2 = rb2d.velocity.y < 0 && !tocandoSuelo;

        // Activa el parámetro "IsFalling" en el Animator.
        animacion.SetBool("EstaCayendo", isFalling2);

        animacion.SetFloat("VerticalAnim", rb2d.velocity.y);
        if (tocandoSuelo)
        {
            animacion.SetBool("IsGround", true);
        }
        else
        {
            animacion.SetBool("IsGround", false);
        }
        #endregion
    }

    //Clases

    //Funcion de Saltar
    void Salto()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, saltoFuerza);
        saltosRestantes--;
    }

    //Funcion para hacer el Dash,es una corrutina
    #region Corrutina Dash
    private IEnumerator Dash()
    {
        sepuedeMover = false;
        puedeDashear = false;
        rb2d.gravityScale = 0;
        //rb2d.velocity = new Vector2(velocidadDash * transform.localScale.x, 0);
        rb2d.AddForce(new Vector2(velocidadDash * transform.localScale.x, 0), ForceMode2D.Impulse);
        animacion.SetTrigger("Dash");

        yield return new WaitForSeconds(tiempoDash);

        sepuedeMover = true;
        puedeDashear = true;
        rb2d.gravityScale = gravedadInical;
    }
    #endregion
    //El controlador de vidas,el cual sera llamado por los enemigos
    public void TomandoDaño(int daño, Vector2 posicion)
    {
        vidaRestante -= daño;
        contadorVidas.text = vidaRestante.ToString();
        animacion.SetTrigger("RecibioDaño");
        Rebote(posicion);
        StartCoroutine(PerderControl());
        /*
        if (vidaRestante == 0)
        {
            GameOver();
        }
        */
    }
    //Corrutina de perdida de control
    private IEnumerator PerderControl()
    {
        sepuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        sepuedeMover = true;
    }

    //el controlador para recuperar vida
    public void CurandoDaño(int cura)
    {
        vidaRestante += cura;
        contadorVidas.text = vidaRestante.ToString();
        Debug.Log("Te curaste vida: " + cura);
    }

    //Invoca al gameover,aun no programado
    void GameOver()
    {
        Time.timeScale = 0f;
        panelGameOver.SetActive(true);
        Debug.Log("Moriste");
    }
    //Reinicia la escena despues de morir al apretar el boton de volver a empezar
    public void RecargarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Incova la pausa
    public void Pausa()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
            panelPausa.SetActive(false);
        }
        else if(Time.timeScale > 0)
        {
            Time.timeScale = 0;
            panelPausa.SetActive(true);
        }
    }

    public void Rebote(Vector2 puntoGolpe)
    {
        rb2d.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
    }

}
