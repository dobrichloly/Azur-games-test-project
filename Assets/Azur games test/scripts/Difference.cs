using UnityEngine;

public class Difference : MonoBehaviour
{
    [Header("Versions (что показывать)")]
    public bool showOnLeft = true;
    public bool showOnRight = true;

    [Header("Click VFX")]
    public GameObject clickEffectPrefab; // префаб вспышки/частиц
    public AudioClip clickSound;

    [Header("Highlight/Animation")]
    public Color highlightColor = Color.yellow; // цвет подсветки
    public float highlightDuration = 0.5f;
    public float pulseScale = 1.3f; // на сколько увеличиваем при клике
    public float pulseDuration = 0.25f; // скорость пульсации

    private bool isFound = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Vector3 originalScale;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        originalScale = transform.localScale;
    }

    // вызывается LevelController
    public void Apply(bool isLeftSide)
    {
        gameObject.SetActive(isLeftSide ? showOnLeft : showOnRight);
    }

    void OnMouseDown()
    {
        if (isFound) return;

        isFound = true;

        PlayEffect();
        PlaySound();
        Highlight();
        Pulse();
    }

    void PlayEffect()
    {
        if (clickEffectPrefab != null)
        {
            Instantiate(clickEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    void PlaySound()
    {
        if (clickSound != null)
        {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        }
    }

    void Highlight()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlightColor;
            StartCoroutine(RemoveHighlight());
        }
    }

    System.Collections.IEnumerator RemoveHighlight()
    {
        float timer = 0f;
        while (timer < highlightDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        if(spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }

    void Pulse()
    {
        StartCoroutine(PulseCoroutine());
    }

    System.Collections.IEnumerator PulseCoroutine()
    {
        float timer = 0f;
        Vector3 targetScale = originalScale * pulseScale;

        // увеличиваем
        while(timer < pulseDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / pulseDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        // возвращаем обратно
        timer = 0f;
        while(timer < pulseDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / pulseDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }
}