using UnityEngine;
using System.Collections;

public class RoofFadeTilemap : MonoBehaviour
{
    public GameObject roof;
    public float fadeDuration = 0.5f;

    private Coroutine currentCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player bước vào sàn nhà → ẩn mái
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(FadeRoof(1f, 0f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player bước ra khỏi sàn nhà → hiện mái
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(FadeRoof(0f, 1f));
        }
    }

    private IEnumerator FadeRoof(float startAlpha, float targetAlpha)
    {
        float elapsed = 0f;
        var renderers = roof.GetComponentsInChildren<Renderer>();
        Color[] startColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
            startColors[i] = renderers[i].material.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            for (int i = 0; i < renderers.Length; i++)
            {
                Color c = startColors[i];
                c.a = Mathf.Lerp(startAlpha, targetAlpha, t);
                renderers[i].material.color = c;
            }

            yield return null;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            Color c = startColors[i];
            c.a = targetAlpha;
            renderers[i].material.color = c;
        }
    }
}
