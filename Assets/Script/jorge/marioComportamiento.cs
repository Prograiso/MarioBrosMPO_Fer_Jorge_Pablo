using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class marioComportamiento : MonoBehaviour
{
    private marioComportamiento script;
   public float velocidad = 5f;
   private SpriteRenderer spriteRenderer;
   private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        script = gameObject.GetComponent<marioComportamiento>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
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
    }
    
    // De esta forma no se cambia de escena de golpe y espera unos segundos
    System.Collections.IEnumerator EsperarCambiarEscena()
    {
        yield return new WaitForSeconds(3f);
       
        SceneManager.LoadScene(1);
    }
}
