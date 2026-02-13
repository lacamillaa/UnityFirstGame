using UnityEngine;

public class CMazeCell : MonoBehaviour 
{
    [SerializeField] private GameObject LeftWall;
    [SerializeField] private GameObject RightWall;
    [SerializeField] private GameObject FrontWall;
    [SerializeField] private GameObject BackWall;

    [SerializeField] private GameObject UnvisitedCell;

    public bool IsVisited { get; private set; }

    public void Visit()
    {
        IsVisited = true;
        UnvisitedCell.SetActive(false);
    }

    public void ClearLeftWall()
    {
        LeftWall.SetActive(false);
    }

    public void ClearRightWall()
    {
        RightWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        FrontWall.SetActive(false);
    }

    public void ClearBackWall()
    {
        BackWall.SetActive(false);
    }
}
