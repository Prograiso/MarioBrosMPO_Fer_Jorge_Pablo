using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool isBreakable;
    public GameObject brickPiecePrefab;
    public int numCoins = 10;
    public GameObject coinBlockPrefab;

    private bool bouncing;
    public Sprite emptyBlock;
    bool isEmpty;

    public GameObject[] itemPrefabs;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HeadMario"))
        {


            if (!isEmpty)
            {
                if (numCoins > 0)
                {
                    if (!bouncing)
                    {
                        numCoins--;
                        Instantiate(coinBlockPrefab, transform.position + Vector3.up, Quaternion.identity);
                        Bounce();

                        if (numCoins <= 0 && (itemPrefabs == null || itemPrefabs.Length == 0))
                        {
                            SetEmpty();
                        }
                    }
                }
                // Si no quedan monedas pero hay items en el array
                else if (numCoins <= 0 && itemPrefabs.Length > 0)
                {
                    if (!bouncing)
                    {
                        Bounce();
                        // 1. Elegimos un índice aleatorio
                        int randomIndex = Random.Range(0, itemPrefabs.Length);
                        GameObject selectedItem = itemPrefabs[randomIndex];

                        // 2. Soltamos el item elegido
                        StartCoroutine(ProcessItem(selectedItem));
                        SetEmpty();
                    }
                }
            }
        }
    }

    private void SetEmpty()
    {
        isEmpty = true;
        GetComponent<SpriteRenderer>().sprite = emptyBlock;
    }

    IEnumerator ProcessItem(GameObject itemPrefab)
    {
        if (itemPrefab == null) yield break;

        // Comportamiento especial para BlockCoin (instancia inmediata)
        if (itemPrefab.CompareTag("BlockCoin"))
        {
            Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
        else
        {
            // Comportamiento para items que "emergen" (Champi, Flor...)
            yield return StartCoroutine(ShowItem(itemPrefab));
        }
    }

    IEnumerator ShowItem(GameObject itemPrefab)
    {
        yield return new WaitForSeconds(0.2f);

        GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);

        // Ajuste de altura por Tag
        float upwardDistance = 1f;
        if (newItem.CompareTag("Flower")) upwardDistance = 1.5f;
        else if (newItem.CompareTag("Mushroom")) upwardDistance = 1f;

        // Intentar obtener el script de movimiento
        MonoBehaviour autoMovement = newItem.GetComponent("AutoMovement") as MonoBehaviour;
        if (autoMovement != null) autoMovement.enabled = false;

        float time = 0;
        float duration = 0.8f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = transform.position + Vector3.up * upwardDistance;

        while (time < duration)
        {
            newItem.transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        newItem.transform.position = targetPos;
        if (autoMovement != null) autoMovement.enabled = true;
    }

    // --- ANIMACIONES Y FÍSICAS ---

    void Bounce()
    {
        if (!bouncing) StartCoroutine(BounceAnimation());
    }

    IEnumerator BounceAnimation()
    {
        bouncing = true; // Usamos la variable de clase correctamente
        float time = 0;
        float duration = 0.1f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * 0.25f;

        // Sube
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // Baja
        time = 0;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(targetPosition, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
        bouncing = false;
    }


}