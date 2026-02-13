using System;
using System.Collections.Generic;
using UnityEngine;

public class CMazeGenerator : MonoBehaviour
{
    [SerializeField] private int Width;
    [SerializeField] private int Depth;

    [SerializeField] private CMazeCell Prefab;

    private CMazeCell[,] Cells;

    public List<CMazeCell> GetNeighbors(CMazeCell _cell)
    {
        List<CMazeCell> _neighbors = new List<CMazeCell>();

        int x = (int)_cell.transform.position.x;
        int z = (int)_cell.transform.position.z;

        if(x / 5 + 1 < Width)
        {
            var right = Cells[x / 5 + 1, z];
            if(!right.IsVisited)
            {
                _neighbors.Add(right);
            }
        }
        if (x / 5 - 1 >= 0)
        {
            var left = Cells[x / 5 - 1, z];
            if (!left.IsVisited)
            {
                _neighbors.Add(left);
            }
        }
        if (z / 5 + 1 < Depth)
        {
            var back = Cells[x, z / 5 + 1];
            if (!back.IsVisited)
            {
                _neighbors.Add(back);
            }
        }
        if (z / 5 - 1 >= 0)
        {
            var front = Cells[x, z / 5 - 1];
            if (!front.IsVisited)
            {
                _neighbors.Add(front);
            }
        }

        return _neighbors;
    }

    public CMazeCell? GetNextCell(CMazeCell _cell)
    {
        var _neighbors = GetNeighbors(_cell);

        System.Random r = new System.Random();

        if(_neighbors.Count == 0)
        {
            return null; 
        }
        else
        {
            int n = r.Next(0, _neighbors.Count - 1);
            return _neighbors[n];
        }
    }

    public void ClearWalls(CMazeCell _prev, CMazeCell _curr)
    {
        int x = (int)_curr.transform.position.x;
        int z = (int)_curr.transform.position.z;

        if (x > _prev.transform.position.x)
        {
            _prev.ClearLeftWall();
            _curr.ClearRightWall();
            return;
        }
        if (x < _prev.transform.position.x)
        {
            _prev.ClearRightWall();
            _curr.ClearLeftWall();
        }
        if (z > _prev.transform.position.z)
        {
            _prev.ClearBackWall();
            _curr.ClearFrontWall();
        }
        if (z < _prev.transform.position.z)
        {
            _prev.ClearFrontWall();
            _curr.ClearBackWall();
        }
    }

    void Start()
    {
        Cells = new CMazeCell[Width, Depth];
        for(int x = 0; x < Width; x++)
        {
            for(int z = 0; z < Depth; z++)
            {
                Cells[x, z] = Instantiate(Prefab, new Vector3(5*x, 0, 5*z), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
