using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Space : MonoBehaviour
{
    private GameController gameController;
    private GameObject button;

    private int row;
    private int column;

    private bool isRed = false;

    public void SetGameController(GameController gameController)
    {
        this.gameController = gameController;
    }

    public void SetButton(GameObject button)
    {
        this.button = button;
    }

    public void SetPosition(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public System.Numerics.Vector2 GetPosition()
    {
        return new System.Numerics.Vector2(row, column);
    }

    public void OnClick()
    {
        gameController.ButtonPressed(this);
    }

    public void ChangeColorToRed()
    {
        button.GetComponent<Image>().color = Color.red;
    }

    public void ChangeColorToWhite()
    {
        button.GetComponent<Image>().color = Color.white;
    }

    public void SwitchColor()
    {
        isRed = !isRed;
    }

    public void Render()
    {
        if (isRed) ChangeColorToRed();
        else ChangeColorToWhite();
    }

    public List<Space> GetNeighbors()
    {
        List<Space> results = new List<Space>();
        for (int r = 0; r < gameController.GetSize(); ++r)
        {
            for (int c = 0; c < gameController.GetSize(); ++c)
            {
                Space target = gameController.GetSpace(r, c);
                if (target != null && gameController.Distance(this, target) == 1)
                {
                    results.Add(target);
                }
            }
        }
        return results;
    }
}
