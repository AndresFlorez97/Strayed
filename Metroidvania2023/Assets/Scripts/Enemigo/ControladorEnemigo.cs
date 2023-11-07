using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEnemigo : MonoBehaviour
{
    public Transform inicioPatrulla;    // Punto de inicio del �rea de patrullaje (izquierda).
    public Transform finPatrulla;   // Punto final del �rea de patrullaje (derecha).
    public float patrolSpeed = 2.0f;  // Velocidad de patrullaje del enemigo.
    [SerializeField] private int vidaEnemigo;

    private bool movingRight = true;  // Variable para controlar la direcci�n de movimiento.
    //private Collider2D col;
    /*
    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    */
    private void OnDrawGizmos()
    {
        // Dibuja un gizmo para el �rea de patrullaje en el editor.
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(inicioPatrulla.position, finPatrulla.position);
    }

    void Update()
    {
        // Verifica la direcci�n del movimiento y cambia de direcci�n si es necesario.
        if (transform.position.x >= finPatrulla.position.x)
        {
            movingRight = false;
        }
        else if (transform.position.x <= inicioPatrulla.position.x)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (!movingRight)
         {
            transform.localScale = new Vector2(1, 1);
        }
        // Mueve al enemigo horizontalmente seg�n la direcci�n de movimiento.
        float moveDirection = movingRight ? 1.0f : -1.0f;
        transform.Translate(Vector2.right * moveDirection * patrolSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MovimientoPlayer>().TomandoDa�o(1, collision.GetContact(0).normal);
        }
    }

    public void RecibeDa�o(int da�o)
    {
        vidaEnemigo -= da�o;
        if(vidaEnemigo >= 0)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

}

