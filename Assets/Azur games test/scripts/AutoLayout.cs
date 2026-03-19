using UnityEngine;

[ExecuteAlways] // работает в редакторе и в Play Mode
public class AutoLayout : MonoBehaviour
{
    [Header("Сцены")]
    public Transform leftRoot;
    public Transform rightRoot;

    [Header("Настройка сцены")]
    public float sceneWidth = 6f;
    public float sceneHeight = 6f;

    [Header("Вертикальная ориентация (Portrait)")]
    public Vector3 leftPosPortrait = new Vector3(0, 3, 0);
    public Vector3 rightPosPortrait = new Vector3(0, -3, 0);
    public float minScalePortrait = 0.5f;
    public float maxScalePortrait = 1f;

    [Header("Горизонтальная ориентация (Landscape)")]
    public Vector3 leftPosLandscape = new Vector3(-3, 0, 0);
    public Vector3 rightPosLandscape = new Vector3(3, 0, 0);
    public float minScaleLandscape = 0.5f;
    public float maxScaleLandscape = 1f;

    [Header("Расстояние между сценами (только для автоматического расчета масштаба)")]
    public float spacing = 1.5f;

    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (leftRoot == null || rightRoot == null || cam == null) return;

        bool landscape = Screen.width > Screen.height;

        // Позиция
        if (landscape)
        {
            leftRoot.localPosition = leftPosLandscape;
            rightRoot.localPosition = rightPosLandscape;
        }
        else
        {
            leftRoot.localPosition = leftPosPortrait;
            rightRoot.localPosition = rightPosPortrait;
        }

        // Масштаб
        float scale = CalculateScale(landscape);

        if (landscape)
            scale = Mathf.Clamp(scale, minScaleLandscape, maxScaleLandscape);
        else
            scale = Mathf.Clamp(scale, minScalePortrait, maxScalePortrait);

        leftRoot.localScale = Vector3.one * scale;
        rightRoot.localScale = Vector3.one * scale;
    }

    float CalculateScale(bool landscape)
    {
        if (landscape)
        {
            float totalWidth = sceneWidth * 2 + spacing;
            float aspect = (float)Screen.width / Screen.height;
            return (cam.orthographicSize * 2 * aspect) / totalWidth;
        }
        else
        {
            float totalHeight = sceneHeight * 2 + spacing;
            return (cam.orthographicSize * 2) / totalHeight;
        }
    }
}