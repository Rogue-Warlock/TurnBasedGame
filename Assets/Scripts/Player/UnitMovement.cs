using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int moveRange = 3;

    private bool isMoving = false;
    private Vector3 targetPosition;
    private Rigidbody rb;
    private GridSpawner gridSpawner;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;

        gridSpawner = FindObjectOfType<GridSpawner>();
        StartCoroutine(WaitForGridReady());
    }

    IEnumerator WaitForGridReady()
    {
        while (gridSpawner == null || !gridSpawner.gridReady)
        {
            yield return null;
        }

        HighlightMovementRange();
    }

    public void TryMoveTo(Vector3 clickedPos)
    {
        if (isMoving) return;

        int currentX = Mathf.RoundToInt(transform.position.x);
        int currentZ = Mathf.RoundToInt(transform.position.z);
        int targetX = Mathf.RoundToInt(clickedPos.x);
        int targetZ = Mathf.RoundToInt(clickedPos.z);

        int distance = Mathf.Abs(targetX - currentX) + Mathf.Abs(targetZ - currentZ);

        if (distance <= moveRange)
        {
            clickedPos.y = transform.position.y;
            MoveTo(clickedPos);
        }
        else
        {
            Debug.Log("Target tile is out of movement range.");
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);

            if ((targetPosition - rb.position).sqrMagnitude < 0.01f)
            {
                rb.MovePosition(targetPosition);
                isMoving = false;
                HighlightMovementRange();
            }
        }
    }

    public void MoveTo(Vector3 position)
    {
        targetPosition = position;
        isMoving = true;
    }

    public void HighlightMovementRange()
    {
        if (gridSpawner == null) return;

        int currentX = Mathf.RoundToInt(transform.position.x);
        int currentZ = Mathf.RoundToInt(transform.position.z);

        for (int x = 0; x < gridSpawner.width; x++)
        {
            for (int z = 0; z < gridSpawner.height; z++)
            {
                GameObject tileObj = gridSpawner.tiles[x, z];
                if (tileObj == null) continue;

                Tile tile = tileObj.GetComponent<Tile>();
                if (tile == null) continue;

                int distance = Mathf.Abs(currentX - x) + Mathf.Abs(currentZ - z);

                if (distance <= moveRange)
                    tile.Highlight(Color.cyan);
                else
                    tile.ResetColor();
            }
        }
    }

   
}



