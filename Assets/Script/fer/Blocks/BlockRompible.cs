using UnityEngine;

public class BlockRompible : MonoBehaviour
{
    public GameObject brickPiecePrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HeadMario"))
        {
            Break();
        }
    }

    private void Break()
    {
        GameObject brickPiece;

        //Arriba a la derecha
        brickPiece = Instantiate(brickPiecePrefab, transform.position,
         Quaternion.Euler(new Vector3(0, 0, 0)));

        brickPiece.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(6f, 12f);

        //Arriba a la izquierda
        brickPiece = Instantiate(brickPiecePrefab, transform.position,
         Quaternion.Euler(new Vector3(0, 0, 90)));
        brickPiece.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-6f, 12f);

        //Abajo a la derecha
        brickPiece = Instantiate(brickPiecePrefab, transform.position,
         Quaternion.Euler(new Vector3(0, 0, -90)));
        brickPiece.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(6f, -8f);

        //Abajo a la izquierda
        brickPiece = Instantiate(brickPiecePrefab, transform.position,
         Quaternion.Euler(new Vector3(0, 0, 180)));
        brickPiece.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-6f, -8f);

        Destroy(gameObject);
    }
}