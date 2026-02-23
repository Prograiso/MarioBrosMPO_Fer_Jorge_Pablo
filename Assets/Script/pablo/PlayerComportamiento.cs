using UnityEngine;
using System.Collections;
using System;

public class ControladorJugador : MonoBehaviour
{
    [Header("Estados Poder")]
    [SerializeField] private EstadoPoder estadoPoder = EstadoPoder.Pequeno;
    private enum EstadoPoder { Pequeno, Grande, Fuego }


    [SerializeField] private double escalaBola;
    [Header("En tierra")]
    [SerializeField] private float gravedadNormal = -20f;
    private float velocidad = 3f;
    [SerializeField] private float fuerzaSalto = 8f;    // Potencia del salto

    [Header(" En Agua")]
    [SerializeField] private float gravedadAgua = -2f;
    [SerializeField] private float velocidadNado = 2f;
    [SerializeField] private float fuerzaNado = 4f;
    [SerializeField] private float velocidadMaxCaidaAgua = -1.5f;

    private float velocidadVertical = 0f;

    private bool enAgua = false;

    [Header("Disparos")]
    [SerializeField] private GameObject bolaFuego; // Prefab de la bola
    [SerializeField] private Transform grupoBolasFuego;  // Empty que contendrá las bolas

    // --- VARIABLES PRIVADAS ---
    private Rigidbody2D rb;
    private Animator animatorComponent; // Referencia al componente de animaciones
    private bool andando;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D col;
    private bool estaEnSuelo = true;

    // Propiedad para que otros scripts lean el estado sin poder modificarlo directamente


    void Start()
    {
        // Inicializamos las referencias a los componentes del objeto
        rb = GetComponent<Rigidbody2D>();
        animatorComponent = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // Ejecución constante de la lógica del personaje
        if (enAgua)
        {
            Nadar();
        }
        else
        {
            Mover();
            Saltar();
            AplicarGravedadNormal();
        }
        Disparar();
        ActualizarAnimator();


        //imprimir por consola estasuelo
        //Debug.Log("¿Está en suelo? " + estaEnSuelo);
        //Debug.Log("¿EATADO? " + estadoPoder);
    }

    // Gestiona el movimiento izquierda/derecha y el giro del personaje (Flip)
    void Mover()//COMPROBAR SIEMPRE GRAVITYSCALE=0 EN EL INSPECTOR DEL RB DEL PLAYER
    {
        // IZQUIERDA
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow) )
        {
            andando = true;
            animatorComponent.SetBool("andando", true);

            // Movemos usando la variable 'velocidad'
            transform.position -= new Vector3(velocidad, 0, 0) * Time.deltaTime;

            // Girar el sprite a la izquierda
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        // DERECHA
        else if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
        {
            andando = true;
            animatorComponent.SetBool("andando", true);

            // Movemos usando la variable 'velocidad'
            transform.position += new Vector3(velocidad, 0, 0) * Time.deltaTime;

            // Girar el sprite a la derecha
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        // QUIETO
        else
        {
            andando = false;
            animatorComponent.SetBool("andando", false);
        }

    }

    // Controla el salto cuando se pulsa arriba y estamos pisando suelo
    void Saltar()
    {
        if (!enAgua && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && estaEnSuelo)
        {
            velocidadVertical = fuerzaSalto;
        }
    }

    void AplicarGravedadNormal()
    {
        velocidadVertical += gravedadNormal * Time.deltaTime;

        transform.position += new Vector3(0, velocidadVertical * Time.deltaTime, 0);

        if (estaEnSuelo && velocidadVertical < 0)
        {
            velocidadVertical = 0f;
        }
    }

    void Nadar()
    {
        float movimientoX = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movimientoX = -velocidadNado;
            // Girar izq
            transform.localScale = new Vector3(-1, 1, 1);

        }

        else if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
        {
            movimientoX = velocidadNado;
            //giro dch
            transform.localScale = new Vector3(1, 1, 1);

        }

        // Impulso hacia arriba
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            velocidadVertical = fuerzaNado;
        }

        // Aplicar gravedad suave
        velocidadVertical += gravedadAgua * Time.deltaTime;

        // Si se pulsa DOWNARROW, acelerar descenso
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            velocidadVertical += gravedadAgua * 2f * Time.deltaTime;
        }
        // Amortiguación (resistencia del agua)
        velocidadVertical *= 0.98f;

        // Limitar subida y bajada
        float limiteCaida = (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow)) ? -3f : -1.5f;
        velocidadVertical = Mathf.Clamp(velocidadVertical, limiteCaida, 4f); //Mathf.Clamp(...) para evitar que suba o baje demasiado rápido

        Vector3 movimiento = new Vector3(
            movimientoX,
            velocidadVertical,
            0);

        transform.position += movimiento * Time.deltaTime;
    }

    // Se ejecuta cuando Mario toca algo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("suelo") && velocidadVertical <= 0)
        {
            estaEnSuelo = true;
            velocidadVertical = 0f;
        }

        if (collision.collider.tag == "Mushroom")
        {
            TomarHongo();
        }

        if (collision.collider.tag == "Flower")
        {
            TomarFlor();
        }
    }

    // Se ejecuta cuando Mario deja de tocar algo
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "suelo")
        {
            estaEnSuelo = false;
        }

    }

    // Lanza bolas de fuego si estamos en estado Fuego y pulsamos X
    void Disparar()
    {
        if (estadoPoder == EstadoPoder.Fuego && Input.GetKeyDown(KeyCode.X))
        {
            // Limitar 5 bolas activas
            if (grupoBolasFuego.childCount < 5)
            {
                //instanciar y aumentar el tamaño de la bola x10
                GameObject bola = Instantiate(bolaFuego, grupoBolasFuego.position, Quaternion.identity);
                bola.transform.gameObject.transform.localScale *= 100;
                escalaBola = bola.transform.localScale.magnitude;

                // Ajustar escala según a donde mira Mario
                bola.transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x), 1, 1);

                // La bola se mueve sola
                bola.GetComponent<BolaFuego>().SetDireccion(bola.transform.localScale.x > 0);
            }
        }
    }

    // Actualiza los parámetros del Animator Controller para cambiar las animaciones
    void ActualizarAnimator()
    {
        // Esto para que el Animator sepa SIEMPRE en qué carpeta estar
        animatorComponent.SetInteger("estadoPoder", (int)estadoPoder);

        animatorComponent.SetBool("estaSuelo", estaEnSuelo);

        animatorComponent.SetBool("estaNadando", enAgua);
    }

    // --- MÉTODOS PÚBLICOS (Llamados por ítems o enemigos) ---

    // Cambia el estado a Grande si éramos pequeños
    public void TomarHongo()
    {
        if (estadoPoder == EstadoPoder.Pequeno)
        {
            estadoPoder = EstadoPoder.Grande;
            AjustarColisionador(true);
            //StartCoroutine(EfectoParpadeo());
        }
    }

    // Cambia el estado a Fuego directamente
    public void TomarFlor()
    {
        if (estadoPoder != EstadoPoder.Fuego)
        {
            bool eraPequeno = estadoPoder == EstadoPoder.Pequeno;
            estadoPoder = EstadoPoder.Fuego;
            if (eraPequeno) AjustarColisionador(true); // Solo ajusta si crecemos
            //StartCoroutine(EfectoParpadeo());
        }
    }

    // Gestiona la pérdida de poder o la muerte
    public void RecibirDanio()
    {
        if (estadoPoder == EstadoPoder.Pequeno)
        {
            animatorComponent.SetTrigger("morir");
            velocidadVertical = fuerzaSalto; // Salto de muerte
            this.enabled = false; // Desactiva el control del jugador
        }
        else
        {
            estadoPoder = EstadoPoder.Pequeno;
            AjustarColisionador(false); // Vuelve al tamaño de collider pequeño
            //StartCoroutine(EfectoParpadeo());
        }
    }

    // Modifica el tamaño y el centro del BoxCollider2D según el estado de poder
    void AjustarColisionador(bool esGrande)
    {
        if (esGrande)
        {
            col.size = new Vector2(col.size.x, 2f);      // Altura para Mario Grande
            col.offset = new Vector2(col.offset.x, 1f);  // Ajusta el centro del collider
        }
        else
        {
            col.size = new Vector2(col.size.x, 1f);      // Altura para Mario Pequeño
            col.offset = new Vector2(col.offset.x, 0.5f);
        }
    }

    // Corrutina que apaga y enciende el sprite para simular un cambio de estado
    // IEnumerator EfectoParpadeo()
    // {
    //     for (int i = 0; i < 6; i++)
    //     {
    //         spriteRenderer.enabled = false;
    //         yield return new WaitForSeconds(0.05f);
    //         spriteRenderer.enabled = true;
    //         yield return new WaitForSeconds(0.05f);
    //     }
    // } COMENTADA POR PABLO, NO ES UTIL, MEJOR TRANSICION DIRECTA

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            enAgua = true;
            estaEnSuelo = false;
            velocidadVertical = 0f; // ← IMPORTANTE NO ACUMULAR VELOCIDAD VERTICAL AL CAER AL AGUA
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            enAgua = false;
        }
    }


}