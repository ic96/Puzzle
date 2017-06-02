using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour
{

    public delegate void PuzzleClickDelegate(Puzzle puzzle);
    public PuzzleClickDelegate OnMouseDownEvent;

    public Vector2 p_offset;
    Vector2 p_scale;
    public Vector2 correctOffset;
    public PointerInputInterface inputDetector;

    void Awake()
    {
        inputDetector.OnPointerDownEvent += OnPointerDown;
        inputDetector.OnPointerUpEvent += OnPointerUp;
    }

    // Creates the puzzle based on size
    public void CreatePuzzle(int size)
    {
        transform.localScale = new Vector3(transform.localScale.x / size, transform.localScale.z / size, 1);
    }

    // Changes position of image
    public void SetImage(Vector2 scale, Vector3 offset)
    {
        p_offset = offset;
        p_scale = scale;
        AssignImage(offset);
    }

    // Saves offset and adjusts image dimensions
    private void AssignImage(Vector3 offset)
    {
        p_offset = offset;
        GetComponent<RawImage>().uvRect = new Rect(offset.x, offset.y, p_scale.x, p_scale.y);
    }

    // Returns current offset
    public Vector2 GetOffset()
    {
        return p_offset;
    }

    // Check if image offset is correct
    public bool CheckPlace()
    {
        return p_offset == correctOffset;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnMouseDownEvent != null)
            OnMouseDownEvent(this);
 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
