using UnityEngine;

public class Tile : MonoBehaviour
{
    public int gridX;
    public int gridZ;

    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void Highlight(Color color)
    {
        rend.material.color = color;
    }

    public void ResetColor()
    {
        rend.material.color = originalColor;
    }
}
