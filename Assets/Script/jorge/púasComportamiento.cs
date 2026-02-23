using Unity.VisualScripting;
using UnityEngine;

public class púasComportamiento: MonoBehaviour
{
    private int vida = 1;

    private púasComportamiento script;
    
    bool seMueve = false;
    private Animator animator;

    private float velocidad = 1f;

    private SpriteRenderer spriteRenderer;

    private CircleCollider2D circleCollider2D;

    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        script = gameObject.GetComponent<púasComportamiento>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void FixedUpdate()
{
    gameObject.transform.position += Vector3.right * velocidad * Time.deltaTime; 

    if (velocidad < 0)
    {
        spriteRenderer.flipX = false; 
    }
    else if (velocidad > 0)
    {
        spriteRenderer.flipX = true; 
    }

    animator.SetBool("andando", true); 
}

void OnCollisionEnter2D(Collision2D collision)
{
    if(collision.gameObject.CompareTag("pared"))
    {
        // Invierte la posición para que no se quede atascado
        velocidad *= -1f; 
    }
    if (collision.collider.tag == "bola")
        {
             vida--;
             if(vida == 0)
            {
                transform.Rotate(0,0,180f);
                seMueve = false;
                rb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
                circleCollider2D.isTrigger = true;
                script.enabled = false;
                Destroy(gameObject,4f);
            }
        }
    
    
    /*if(collision.collider.tag == "fuego")
    {
            vida--;
            velocidad = 0f;
            Destroy(gameObject);
            animator.SetBool("dead", true);
    }*/
    
}
}
