using UnityEngine;

public class ChampiyFlor : MonoBehaviour
{

    public float velocidad = 1f;
    private int vida = 1;

    void FixedUpdate()
    {
        if (gameObject.CompareTag("champi"))
        {
            gameObject.transform.position += new Vector3(1, 0, 0) * velocidad * Time.deltaTime;
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("pared"))
        {
            // Invierte la posición para que no se quede atascado
            velocidad *= -1f;
        }
    }
}
