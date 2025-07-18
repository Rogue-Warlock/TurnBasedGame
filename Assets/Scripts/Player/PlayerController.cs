using UnityEngine;
using System.Collections.Generic;

public class UnitController : MonoBehaviour
{
    public List<PlayerMovement> playerUnits = new List<PlayerMovement>();
    private PlayerMovement selectedUnit;
    public PlayerMovement SelectedUnit => selectedUnit;

    void Update()
    {
        // Left click: select unit
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Select unit if clicked
                PlayerMovement unit = hit.collider.GetComponent<PlayerMovement>();
                if (unit != null && playerUnits.Contains(unit))
                {
                    selectedUnit = unit;
                    selectedUnit.HighlightMovementRange();
                    return;
                }

                // If a unit is selected, try to move it
                if (selectedUnit != null && hit.collider.CompareTag("Tile"))
                {
                    Vector3 clickedPos = hit.collider.transform.position;
                    selectedUnit.TryMoveTo(clickedPos);
                }
            }
        }
    }
}

