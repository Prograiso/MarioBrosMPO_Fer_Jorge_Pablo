using System.Collections;
using UnityEngine;

public class BlockDeMonedas : MonoBehaviour
{
    public int numCoins = 10;
    public GameObject coinBlockPrefab;
    public Sprite emptyBlockSprite;

    private bool bouncing;
    private bool isEmpty;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HeadMario") && !bouncing && !isEmpty)
        {
            GiveCoin();
        }
    }

    private void GiveCoin()
    {
        numCoins--;
        Instantiate(coinBlockPrefab, transform.position + Vector3.up, Quaternion.identity);
        StartCoroutine(BounceAnimation());

        if (numCoins <= 0)
        {
            isEmpty = true;
            GetComponent<SpriteRenderer>().sprite = emptyBlockSprite;
        }
    }

    IEnumerator BounceAnimation()
    {
        bouncing = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * 0.25f;
        float duration = 0.1f;

        // Subida
        float time = 0;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        // Bajada
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