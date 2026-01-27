using UnityEngine;

public class bowserComportamiento : MonoBehaviour
{
    private bowserComportamiento script;
    // Condiciones para cuando se causa daño a bowser
    public float tiempoParado= 0f;
    public float duracionParada = 2f;
    private CircleCollider2D circleCollider2D;
    
    public int vidas = 5;
    // Muro invisible
    public GameObject muroInvisible;
    private bool estaSuelo = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    // parametros salto
    public Transform puntoInicio;
    public Transform puntoFinal;
    public float velocidad = 2f;
    public float distanciaBatalla = 10f;
    public float fuerzaSalto = 8f;
    public float minTiempoSalto = 1f;
    public float maxTiempoSalto = 5f;

    // parametros disparo
    bool puedeDisparar;
    public GameObject fuegoBowser;
    public Transform posicionInicialDisparo;
    public float minTiempoDisparo = 2f;
    public float maxTiempoDisparo = 5f;
    float tiempoDisparo;
    public float minDistanciaDisparo = 50f;

    Transform marioTransform;
    Vector3 posicionObjetivo;
    private Animator animator;
    float tiempoSalto;
    bool seMueve = false;

    void Start()
    {
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Localizamos a mario mediante su tag con FindGameObjectWithTag.
        // Bowser se activa cuando entre en el rango del puntoInicio y puntoFinal de bowser
        // Nos permite acceder a todas las caracteristicas del tag = "Player" de su componenete transform.
        marioTransform = GameObject.FindGameObjectWithTag("Player").transform;
        posicionObjetivo = puntoFinal.position;
        script = gameObject.GetComponent<bowserComportamiento>();
        // Temporizador aleatorio de los saltos
        tiempoSalto = Random.Range(minTiempoSalto, maxTiempoSalto);
        tiempoDisparo = Random.Range(minTiempoDisparo,maxTiempoDisparo);
    }

    void Update()
    {
        // Cálculamos la distancia entre bowser mario en eje x
        float distanciaX = transform.position.x - marioTransform.position.x;
        if (distanciaX < 0) { distanciaX = distanciaX * -1; }

        if (distanciaX < distanciaBatalla)
        {
            seMueve = true;
        }

        if (seMueve)
        {
            // Giro de bowser
            // Si Mario está a la derecha de Bowser, activamos o desactivamos el flipX
            spriteRenderer.flipX = marioTransform.position.x > transform.position.x;
            // Recorrido que tendrá bowser
            float proximaX = Mathf.MoveTowards(transform.position.x, posicionObjetivo.x, velocidad * Time.deltaTime);
            // Aplicamos la X pero mantenemos la Y del Rigidbody para que pueda saltar
            transform.position = new Vector3(proximaX, transform.position.y, transform.position.z);

            // Puntos de recorrido de bowser
            if (transform.position.x == posicionObjetivo.x)
            {
                if (posicionObjetivo == puntoFinal.position)
                {
                    posicionObjetivo = puntoInicio.position;
                }
                else
                {
                    posicionObjetivo = puntoFinal.position;
                }
            }

            // Temporizador de salto de manera aeatoria
            tiempoSalto -= Time.deltaTime;
            if (tiempoSalto <= 0 && estaSuelo)
            {
                rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                tiempoSalto = Random.Range(minTiempoSalto, maxTiempoSalto);
            }
            // Temporizador de disparo de manera aeatoria
            tiempoDisparo -= Time.deltaTime;
            if (tiempoDisparo <= 0)
            {
                if(distanciaX < minDistanciaDisparo)
                {
                    Disparar();
                }
                tiempoDisparo = Random.Range(minTiempoDisparo, maxTiempoDisparo);
            }
          
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "suelo")
        {
            estaSuelo = true;;
        }
        if(collision.collider.tag == "Player")
        {
           
            vidas--;
            Debug.Log("vidas quitada");
            

            if(vidas == 0)
            {
                
                animator.SetBool("morir",true);
                transform.Rotate(0,0,180f);
                seMueve = false;
                rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
                circleCollider2D.isTrigger = true;
                script.enabled = false;
                Destroy(muroInvisible);
                Destroy(gameObject,4f);
            }
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "suelo")
        {
            estaSuelo = false;
        }
    }
    
    // Accion del disparo de fuego de bowser
    void Disparar()
    {
        GameObject proyectil =Instantiate(fuegoBowser,posicionInicialDisparo.position, Quaternion.identity);
        Rigidbody2D rbfuego = proyectil.GetComponent<Rigidbody2D>();
        SpriteRenderer spriteFuego = proyectil.GetComponent<SpriteRenderer>();
        float direccion = -1f;
        if (spriteRenderer.flipX)
        {
            direccion = 1f;
            spriteFuego.flipX = false;
        }
        else
        {
            direccion = -1f;
            spriteFuego.flipX = true;
        }
        rbfuego.AddForce(new Vector2(direccion * 10f, 0), ForceMode2D.Impulse);
        Destroy(proyectil,3f);
    }

    
}
