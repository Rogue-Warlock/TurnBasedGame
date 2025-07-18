using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public UnitController unitController; // Reference to your UnitController
    public float height = 10f;

    void LateUpdate()
    {
        if (unitController == null) return;

        var selectedUnit = unitController.SelectedUnit;
        if (selectedUnit == null) return;

        Vector3 unitPos = selectedUnit.transform.position;
        Vector3 newPos = new Vector3(unitPos.x, height, unitPos.z);

        transform.position = newPos;
    }
}
