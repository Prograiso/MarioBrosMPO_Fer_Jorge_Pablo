using UnityEngine;

public class gombaComportamiento : MonoBehaviour
{
    [SerializeField] private AudioClip[]audios;
    private AudioSource audioSource;
    private Animator animator;
    public float velocidad = 1f;
    
    private gombaComportamiento script;
    bool seMueve = false;
    private int vida = 1;

    private CircleCollider2D circleCollider2D;

    private Rigidbody2D rb;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        script = gameObject.GetComponent<gombaComportamiento>();
        
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
                audioSource.clip = audios[0];
                audioSource.PlayOneShot(audios[0]);
                vida = 0;
                // Al pisarlo se queda quieto y se aplica la animación
                velocidad=0;
                animator.SetBool("muerte",true);
                Destroy(gameObject,0.5f);
            }
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
    
    
    // if(collision.collider.tag == "fuego")
    // {
        // vida--;
          // velocidad = 0f;
           // Destroy(gameObject,0.3f);
           // animator.SetBool("dead", true);
    // }
    
}
}
