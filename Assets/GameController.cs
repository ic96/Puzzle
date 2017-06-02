using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // Use this for initialization
    public int size;
    public GameObject puzzle_piece;
    public Puzzle firstPiece;
    public Puzzle secondPiece;
    public Canvas canvas;
    public Timer clock;
    public Image finished_puzzle;
    public GUIText gameOverText;
    public int fade = 1;

    private Puzzle[,] puzzle;
    private int image_width;
    private int image_height;
    private List<Vector2> offsetList;
    private List<Vector2> correctList;
    private bool gameState;
    

    float temp_width;
    float temp_height;
    float split_size;

    // Get canvas width height
    // Get no. size
    // Calc grid width height
    void Start()
    {
        GameInit();

        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState)
        {
            RenderFinishedPuzzle();
        }

    }

    #region Game Flow

    private void GameInit()
    {
        split_size = 1f / size;
        image_width = (int)canvas.GetComponent<RectTransform>().rect.width / 2;
        image_height = (int)canvas.GetComponent<RectTransform>().rect.height;
        puzzle = new Puzzle[size, size];
        offsetList = new List<Vector2>();
        correctList = new List<Vector2>();
        InitPuzzle();
        InitBoard();
        RandomPuzzles();
    }

    private void GameStart()
    {
        StartTimer();
        gameState = false;
        GameStartText();
    }


    private void GameOver()
    {
        gameState = true;
        StopTimer();
        GameEndText();
        DisplayFinishedPuzzle();
    }
    
    private void GameEndText()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over";
    }

    private void GameStartText()
    {
        gameOverText.gameObject.SetActive(false);
        gameOverText.text = "";
    }

    #endregion

    #region Puzzle Handler

    // Randomizes puzzle using offset
    private void RandomPuzzles()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int index = Random.Range(0, offsetList.Count);
                puzzle[i, j].SetImage(new Vector2(split_size, split_size), offsetList[index]);
                offsetList.RemoveAt(index);
            }
        }
    }

    // Create puzzle based on size
    private void InitPuzzle()
    {
        finished_puzzle.gameObject.SetActive(false);
        finished_puzzle.color = new Color(finished_puzzle.color.r, finished_puzzle.color.g, finished_puzzle.color.b, 0);
        GameObject temp;
        float temp_width = this.GetComponent<RectTransform>().rect.width;
        float temp_height = this.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                temp = (GameObject)Instantiate(puzzle_piece, new Vector3(i * temp_width / size, j * temp_height / size, 0), Quaternion.identity);
                temp.transform.SetParent(transform);
                temp.transform.localPosition = new Vector3(temp.transform.localPosition.x + CalcInitX(size), temp.transform.localPosition.y + CalcInitY(size), 0);
                temp.GetComponent<Puzzle>().OnMouseDownEvent += OnPuzzleClicked;
                puzzle[i, j] = (Puzzle)temp.GetComponent("Puzzle");
                puzzle[i, j].CreatePuzzle(size);
            }
        }
    }
    
    // Swap and check game over condition
    private void OnPuzzleClicked(Puzzle puzzle_piece)
    {

        if (firstPiece == null)
        {
            firstPiece = puzzle_piece;
            firstPiece.GetComponentInChildren<RawImage>().color = Color.gray;
        }
        else
            secondPiece = puzzle_piece;

        if (firstPiece && secondPiece)
        {
            Swap();
            ResetSelection();
            if (CorrectBoard())
                GameOver();
        }
    }

    #endregion

    #region Timer Handler

    private void StartTimer()
    {
        clock.StartTimer();
    }

    public void StopTimer()
    {
        clock.StopTimer();
    }

    #endregion

    #region Puzzle Board / Game Board Handler

    // Center x-pos of image
    private float CalcInitX(int size)
    {
        float center_width = image_width / 2;
        return center_width + center_width / size;
    }
    // Center y-pos of image
    private float CalcInitY(int size)
    {
        float center_height = image_height / 2;
        return center_height / size;
    }

    // Initializes correct offset and assigns image
    private void InitBoard()
    {
        Vector2 offset;
        Vector2 scale = new Vector2(split_size, split_size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                offset = new Vector2(i * (split_size), j * (split_size));
                puzzle[i, j].SetImage(scale, offset);
                puzzle[i, j].correctOffset = offset;
                offsetList.Add(offset);
                correctList.Add(offset);
            }
        }
    }

    // Check pieces are correctly placed
    private bool CorrectBoard()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (!puzzle[i, j].CheckPlace())
                {
                    return false;
                }
            }
        }
        return true;
    }

    #endregion

    #region Finished Puzzle Handler

    private void DisplayFinishedPuzzle()
    {
        finished_puzzle.gameObject.SetActive(true);
        print("display puzzle");
        RenderFinishedPuzzle();
    }
    public void RenderFinishedPuzzle()
    {
        float alpha = Mathf.Clamp((finished_puzzle.color.a + Time.deltaTime) / fade, 0, 1);
        finished_puzzle.color = new Color(finished_puzzle.color.r, finished_puzzle.color.g, finished_puzzle.color.b, alpha);
    }


    #endregion


    // Resets highlighted pieces and selections
    private void ResetSelection()
    {
        firstPiece.GetComponent<RawImage>().color = Color.white;
        secondPiece.GetComponent<RawImage>().color = Color.white;
        firstPiece = null;
        secondPiece = null;
    }

    // Swap pieces
    private void Swap()
    {
        Vector2 temp;
        temp = firstPiece.GetOffset();
        firstPiece.SetImage(new Vector2(split_size, split_size), secondPiece.GetOffset());
        secondPiece.SetImage(new Vector2(split_size, split_size), temp);
    }

}
