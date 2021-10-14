using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gridButton;
    public GameObject panel;
    public GameObject canvas;

    private Space[,] board;
    private int size;

    private void Awake()
    {
        CreateBoard(8);
    }

    private void CreateBoard(int size)
    {
        this.size = size;

        float k = ((float)size - 1) / 2;
        float intervalWidth = 5;
        float totalLength = panel.GetComponent<RectTransform>().rect.width;
        float unitLength = (totalLength - intervalWidth) / size;
        float gridLength = unitLength - intervalWidth;

        board = new Space[size, size];

        for (int r = 0; r < size; ++r)
        {
            for (int c = 0; c < size; ++c)
            {
                float x = unitLength * (c - k);
                float y = 0 - unitLength * (r - k);
                GameObject button = Instantiate(gridButton, new Vector3(x, y, 0), Quaternion.identity, panel.transform);
                button.transform.position += canvas.transform.position;
                button.GetComponent<RectTransform>().sizeDelta = new Vector2(gridLength, gridLength);
                button.GetComponent<Space>().SetPosition(r, c);
                button.GetComponent<Space>().SetGameController(this);
                button.GetComponent<Space>().SetButton(button);
                board[r, c] = button.GetComponent<Space>();
            }
        }
    }

    public void ButtonPressed(Space space)
    {
        Debug.Log(space.GetPosition());
        space.SwitchColor();
        Render();
    }

    private void Render()
    {
        for (int r = 0; r < size; ++r)
        {
            for (int c = 0; c < size; ++c)
            {
                board[r, c].Render();
            }
        }
    }

    public int Distance(Space space1, Space space2)
    {
        int r1 = (int)space1.GetPosition().X;
        int c1 = (int)space1.GetPosition().Y;
        int r2 = (int)space2.GetPosition().X;
        int c2 = (int)space2.GetPosition().Y;
        return Mathf.Abs(r1 - r2) + Mathf.Abs(c1 - c2);
    }

    public Space GetSpace(int row, int column)
    {
        if (row < 0 || row > size - 1 || column < 0 || column > size - 1)
        {
            return null;
        }
        return board[row, column];
    }

    public int GetSize()
    {
        return size;
    }
}
