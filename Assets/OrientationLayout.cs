using UnityEngine;
using UnityEngine.UI;

public class OrientationLayout : MonoBehaviour
{
    public GridLayoutGroup grid;

    void Update()
    {
        if (Screen.width > Screen.height)
        {
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 2;
        }
        else
        {
            grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            grid.constraintCount = 2;
        }
    }
}