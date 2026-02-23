using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    public float speed = 3f;

    private Vector3 startPosition;
    private float direction = 1f;

    void Start()
    {
        startPosition = transform.position;

    }

    void Update()
    {

        transform.position += Vector3.right * speed * direction * Time.deltaTime;


    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pared"))
        {

            speed *= -1f;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}
