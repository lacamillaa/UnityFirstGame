using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CMazeGenerator : MonoBehaviour
{
    [SerializeField] private int Width;
    [SerializeField] private int Depth;

    [SerializeField] private CMazeCell Prefab;

    [SerializeField] private Transform StartPoint;

    private CMazeCell[,] Cells;

    private float floorWidth;
    private float floorDepth;
    private float scaleX;
    private float scaleZ;
    private float startX;
    private float startZ;
    private float f;

    public List<CMazeCell> GetNeighbors(CMazeCell _cell)
    {
        List<CMazeCell> _neighbors = new List<CMazeCell>();

        int x = (int)((_cell.transform.position.x - startX) / scaleX);
        int z = (int)((_cell.transform.position.z - startZ) / scaleZ);

        Debug.Log(_cell.transform.position.x + " " + _cell.transform.position.z + " -> " + x + " " + z);

        if(x + 1 < Width)
        {
            var right = Cells[x + 1, z];
            if(!right.IsVisited)
            {
                _neighbors.Add(right);
            }
        }
        if (x - 1 >= 0)
        {
            var left = Cells[x - 1, z];
            if (!left.IsVisited)
            {
                _neighbors.Add(left);
            }
        }
        if (z + 1 < Depth)
        {
            var back = Cells[x, z + 1];
            if (!back.IsVisited)
            {
                _neighbors.Add(back);
            }
        }
        if (z - 1 >= 0)
        {
            var front = Cells[x, z - 1];
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

        System.Random r1 = new();
        System.Random r2 = new();

        if(_neighbors.Count == 0)
        {
            return null; 
        }
        else
        {
            _neighbors.Sort((_cell1, _cell2) =>
            {
                return r2.Next() - r1.Next();
            });
            return _neighbors[0];
        }
    }

    public void ClearWalls(CMazeCell _prev, CMazeCell _curr)
    {
        int x = (int)_curr.transform.position.x;
        int z = (int)_curr.transform.position.z;

        if (_prev == null) return;

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

    private void GenerateMaze(CMazeCell previousCell, CMazeCell currentCell)
    {
        if (currentCell == null) return;

        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        CMazeCell nextCell;
        do 
        {
            nextCell = GetNextCell(currentCell);
            if (nextCell != null) GenerateMaze(currentCell, nextCell); 
        } while (nextCell != null);
    }

    void Start()
    {
        Cells = new CMazeCell[Width, Depth];

        floorWidth = StartPoint.localScale.x * 10;
        floorDepth = StartPoint.localScale.z * 10;

        scaleX = floorWidth / Width;
        scaleZ = floorDepth / Depth;

        startX = StartPoint.position.x - floorWidth / 2;
        startZ = StartPoint.position.z - floorDepth / 2;

        Debug.Log("width: " + floorWidth + " depth: " + floorDepth);
        Debug.Log("scale X: " + scaleX + " scale Z: " + scaleZ);
        Debug.Log("start X: " + startX + " start Z: " + startZ);

        for (int x = 0; x < Width; x++)
        {
            for(int z = 0; z < Depth; z++)
            {
                Cells[x, z] = Instantiate(Prefab, new Vector3(
                    startX + scaleX * x, StartPoint.position.y, startZ + scaleZ * z), Quaternion.identity);
            }
        }

        GenerateMaze(null, Cells[0, 0]);
        Cells[0, 0].ClearFrontWall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
