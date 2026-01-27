using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        float time = 0;
        // Aumentamos la duración para que el movimiento sea más fluido y visible
        float duration = 0.45f;

        Vector3 startPosition = transform.position;
        // Aumentamos el recorrido (2.5 unidades hacia arriba)
        Vector3 targetPosition = startPosition + Vector3.up * 2.5f;

        // --- SUBIDA ---
        while (time < duration)
        {
            // Usamos una curva de suavizado para que empiece rápido y frene al llegar arriba
            float t = time / duration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        time = 0;

        // --- BAJADA ---
        while (time < duration)
        {
            // Usamos una curva para que empiece lento y acelere al caer (gravedad)
            float t = time / duration;
            t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);

            transform.position = Vector3.Lerp(targetPosition, startPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;

        // Un pequeño retraso antes de desaparecer para que no se corte de golpe
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
}