using UnityEngine;

public class CMazeCell : MonoBehaviour 
{
    [SerializeField] private GameObject LeftWall;
    [SerializeField] private GameObject RightWall;
    [SerializeField] private GameObject FrontWall;
    [SerializeField] private GameObject BackWall;

    [SerializeField] private GameObject UnvisitedCell;

    [SerializeField] private GameObject GoldCoin;

    public bool IsVisited { get; private set; }
    public bool HasCoin { get; private set; }

    public void Start()
    {
        System.Random r = new ();
        int coin = r.Next(0, 100);
        if(coin >= 75)
        {
            SetCoin();
        }
    }

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

    public void SetCoin()
    {
        HasCoin = true;
        GoldCoin.SetActive(true);
    }

    public void PickupCoin()
    {
        GoldCoin.SetActive(false);
    }
}
