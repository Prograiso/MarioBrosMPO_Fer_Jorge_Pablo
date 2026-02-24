using UnityEngine;

public class enemigoComportamiento : MonoBehaviour
{
    private Animator animator;
    public float velocidad = 1f;
    private int vida = 1;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(vida == 1)
        {
        gameObject.transform.position += new Vector3(1,0,0)*velocidad*Time.deltaTime;
        animator.SetBool("andar",true);  
        }
        
        
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    if(collision.gameObject.CompareTag("pared"))
    {
        // Invierte la posición para que no se quede atascado
        velocidad *= -1f; 
    }
    if(collision.collider.tag == "Player")
        {
            // Cuando el Player colisione en y el gomba morirá (mecánica de eliminar al pisarlo)
            if(collision.transform.position.y > transform.position.y+0.4f)
            {
                vida = 0;
                // Al pisarlo se queda quieto y se aplica la animación
                velocidad=0;
                animator.SetBool("muerte",true);
                Destroy(gameObject,0.5f);
            }
        }
    
    
    // if(collision.collider.tag == "fuego")
    // {
        // vida--;
          // velocidad = 0f;
           // Destroy(gameObject,0.3f);
           // animator.SetBool("dead", true);
    // }
    
}
}
