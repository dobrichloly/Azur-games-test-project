using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Roots")]
    public Transform leftRoot;
    public Transform rightRoot;

    [Header("Level Prefab")]
    public GameObject levelPrefab;

    void Start()
    {
        GenerateLevel();
    }

    // Генерация уровня
    public void GenerateLevel()
    {
        ClearLevel();

        // создаём левую и правую версии
        GameObject left = Instantiate(levelPrefab, leftRoot);
        GameObject right = Instantiate(levelPrefab, rightRoot);

        SetupDifferences(left, right);
    }

    // Очистка предыдущего уровня
    public void ClearLevel()
    {
        foreach (Transform child in leftRoot)
        {
            DestroyImmediate(child.gameObject);
        }

        foreach (Transform child in rightRoot)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    // Настройка отличий
    void SetupDifferences(GameObject left, GameObject right)
    {
        Difference[] diffsLeft = left.GetComponentsInChildren<Difference>();
        Difference[] diffsRight = right.GetComponentsInChildren<Difference>();

        // левая сторона
        foreach (Difference diff in diffsLeft)
        {
            diff.Apply(true); // включаем/выключаем на левой картинке
        }

        // правая сторона
        foreach (Difference diff in diffsRight)
        {
            diff.Apply(false); // включаем/выключаем на правой картинке
        }
    }
}