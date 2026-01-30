using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class  comportamientoMario : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audios;
    private CapsuleCollider2D capsuleCollider2D;
    private Rigidbody2D rb;
    public int vidas = 1;
    private marioComportamiento script;
   public float velocidad = 5f;
   private SpriteRenderer spriteRenderer;
   private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        script = gameObject.GetComponent<marioComportamiento>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        capsuleCollider2D=gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.position+=new Vector3(0,5,0)*velocidad*Time.deltaTime;
            animator.SetBool("saltar",true);
        }
        else
        {
            animator.SetBool("saltar",false);
        }

        if(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position+=new Vector3(-1,0,0)*velocidad*Time.deltaTime;
            spriteRenderer.flipX = true;
            animator.SetBool("correr",true);
        }
       
        else if(Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position+=new Vector3(1,0,0)*velocidad*Time.deltaTime;
            spriteRenderer.flipX = false;
            animator.SetBool("correr",true);
        }
        else
        {
            
         animator.SetBool("correr",false);   
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.CompareTag("moneda"))
    {
        collision.GetComponent<Collider2D>().enabled = false;
        audioSource.PlayOneShot(audios[0]);
        Destroy(collision.gameObject);
    }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "peach")
        {
            animator.SetBool("correr",false);
            animator.SetBool("saltar",false);
            animator.SetBool("idle",true);
            script.enabled = false;
            StartCoroutine(EsperarCambiarEscena());
        }
        if(collision.collider.tag == "lava")
        {
            Debug.Log("ENTRA");
            vidas=0;
            animator.SetBool("morir",true);
            script.enabled = false;
            velocidad = 0;
            rb.AddForce(Vector3.up * 5.0f , ForceMode2D.Impulse);
            capsuleCollider2D.isTrigger = true;
            Destroy(gameObject, 2f);
        }

        if(collision.collider.tag == "gomba")
        {
            animator.SetBool("saltar",true);
            rb.AddForce(Vector2.up*4f,ForceMode2D.Impulse);

        }
    }

    
    
    // De esta forma no se cambia de escena de golpe y espera unos segundos
    System.Collections.IEnumerator EsperarCambiarEscena()
    {
        yield return new WaitForSeconds(3f);
       
        SceneManager.LoadScene(1);
    }
}
