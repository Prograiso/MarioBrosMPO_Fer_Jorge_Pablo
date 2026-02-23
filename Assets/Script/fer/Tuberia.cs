using UnityEngine;
using System.Collections; // Necesario para usar Corrutinas

public class Tuberia : MonoBehaviour
{
    [SerializeField] private Transform puntoDeSalida;
    [SerializeField] private Transform puntoDeSalida2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("finalTuberia"))
        {
            if (puntoDeSalida != null)
            {
                transform.position = puntoDeSalida.position;

                if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
        }
        if (other.CompareTag("finalTuberia2"))
        {
            if (puntoDeSalida != null)
            {
                transform.position = puntoDeSalida2.position;

                if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("sueloTuberia") && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            // Iniciamos el proceso de desactivar y reactivar
            StartCoroutine(ReactivarSuelo(collision.gameObject.GetComponent<Collider2D>()));
        }
    }

    // Esta función se encarga de esperar sin detener el juego
    private IEnumerator ReactivarSuelo(Collider2D col)
    {
        if (col != null)
        {
            col.enabled = false; // Desactiva

            yield return new WaitForSeconds(3f); // Espera 3 segundos

            col.enabled = true;  // Reactiva
        }
    }
}