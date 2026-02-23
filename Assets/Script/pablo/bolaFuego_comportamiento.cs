using UnityEngine;

public class BolaFuego : MonoBehaviour
{
    private bool direccion;       // true = derecha, false = izquierda
    private float velocidad = 3f;
    private float reboteY = 10f;   // altura del rebote
    private float gravedad = -2f;
    private float velocidadY = 4f; // velocidad vertical simulada
    private bool enSuelo = false;

    void Update()
    {
        // Movimiento horizontal
        if (direccion)
            transform.position += new Vector3(1, 0, 0) * velocidad * Time.deltaTime;
        else
            transform.position -= new Vector3(1, 0, 0) * velocidad * Time.deltaTime;

        // Movimiento vertical simulado con rebote
        if (!enSuelo)
        {
            velocidadY += gravedad * Time.deltaTime;             // aplicamos gravedad
            transform.position += new Vector3(0, velocidadY * Time.deltaTime, 0);
        }

        // Rotación visual
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    public void SetDireccion(bool dir)
    {
        direccion = dir;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
        Debug.Log("Bola de fuego destruida por salir de cámara");
    }

    void OnTriggerEnter2D(Collider2D collision)  //no me gusta este código, hay que hacer que al tocar el suelo siba directa hacia arriba otra vez y vuelva a caer
    {
        if (collision.tag == "enemy")
        {
            Destroy(gameObject);
            Debug.Log("Bola de fuego destruida por colisión con enemigo");
        }

        if (collision.tag == "suelo")
        {
            // Rebote vertical
            velocidadY = reboteY;
            enSuelo = false; // sigue aplicando gravedad después
        }
    }
}
