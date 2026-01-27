using TMPro;
using UnityEngine;

public class peachComportamiento : MonoBehaviour
{
    // metemos el Image para que detecte ese gameobject y poder usarlo
    public GameObject nubeDialogo;
    public TextMeshPro texto;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     nubeDialogo.SetActive(false);
     animator = gameObject.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.collider.tag == "Player")
        {
           nubeDialogo.SetActive(true);
           animator.SetBool("victoria",true);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
             nubeDialogo.SetActive(true);
            animator.SetBool("victoria",false);
    }
    }
}
