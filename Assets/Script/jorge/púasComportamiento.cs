using Unity.VisualScripting;
using UnityEngine;

public class púasComportamiento: MonoBehaviour
{
    private int vida = 1;

    private Animator animator;

    private float velocidad = 1f;

    private SpriteRenderer spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
       
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
    
    
    /*if(collision.collider.tag == "fuego")
    {
            vida--;
            velocidad = 0f;
            Destroy(gameObject);
            animator.SetBool("dead", true);
    }*/
    
}
}
