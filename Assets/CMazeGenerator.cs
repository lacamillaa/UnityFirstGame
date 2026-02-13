using UnityEngine;

public class CMazeGenerator : MonoBehaviour
{
    [SerializeField] private int Width;
    [SerializeField] private int Depth;

    [SerializeField] private CMazeCell Prefab;

    private CMazeCell[,] Cells;

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
