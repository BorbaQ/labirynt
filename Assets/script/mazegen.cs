using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // Assign a wall prefab (e.g., a cube scaled to 1x1x1)
    public GameObject floorPrefab; // Assign a floor prefab (e.g., a plane or cube for the base)
    public Material wallMaterial; // Optional: Assign a material for walls to make them look nicer
    public Light mazeLight; // Optional: Assign a light prefab for atmospheric lighting

    private const int width = 50;
    private const int height = 50;

    // Cell structure to represent walls
    private class Cell
    {
        public bool visited = false;
        public bool[] walls = { true, true, true, true }; // North, East, South, West
    }

    private Cell[,] grid;

    void Start()
    {
        GenerateMaze();
        BuildMaze();
    }

    void GenerateMaze()
    {
        grid = new Cell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Cell();
            }
        }

        // Start recursive backtracking from (0,0)
        RecursiveBacktrack(0, 0);
    }

    void RecursiveBacktrack(int x, int y)
    {
        grid[x, y].visited = true;

        // Directions: North, East, South, West
        List<Vector2Int> directions = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0) };
        directions.Shuffle(); // Randomize order for more organic paths

        foreach (Vector2Int dir in directions)
        {
            int nx = x + dir.x;
            int ny = y + dir.y;

            if (nx >= 0 && nx < width && ny >= 0 && ny < height && !grid[nx, ny].visited)
            {
                // Remove walls between current and neighbor
                if (dir == Vector2Int.up) // North
                {
                    grid[x, y].walls[0] = false;
                    grid[nx, ny].walls[2] = false;
                }
                else if (dir == Vector2Int.right) // East
                {
                    grid[x, y].walls[1] = false;
                    grid[nx, ny].walls[3] = false;
                }
                else if (dir == Vector2Int.down) // South
                {
                    grid[x, y].walls[2] = false;
                    grid[nx, ny].walls[0] = false;
                }
                else if (dir == Vector2Int.left) // West
                {
                    grid[x, y].walls[3] = false;
                    grid[nx, ny].walls[1] = false;
                }

                RecursiveBacktrack(nx, ny);
            }
        }
    }

    void BuildMaze()
    {
        // Instantiate the floor
        GameObject floor = Instantiate(floorPrefab, new Vector3(width / 2f - 0.5f, 0, height / 2f - 0.5f), Quaternion.identity);
        floor.transform.localScale = new Vector3(width, 1, height);

        // Instantiate walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];
                Vector3 cellPos = new Vector3(x, 0, y);

                // North wall
                if (cell.walls[0])
                {
                    GameObject wall = Instantiate(wallPrefab, cellPos + new Vector3(0, 0.5f, 0.5f), Quaternion.identity);
                    wall.transform.localScale = new Vector3(1, 1, 0.1f); // Thin wall
                    if (wallMaterial) wall.GetComponent<Renderer>().material = wallMaterial;
                }

                // East wall
                if (cell.walls[1])
                {
                    GameObject wall = Instantiate(wallPrefab, cellPos + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 90, 0));
                    wall.transform.localScale = new Vector3(1, 1, 0.1f);
                    if (wallMaterial) wall.GetComponent<Renderer>().material = wallMaterial;
                }

                // South wall (only for bottom row to avoid duplicates)
                if (cell.walls[2] && y == 0)
                {
                    GameObject wall = Instantiate(wallPrefab, cellPos + new Vector3(0, 0.5f, -0.5f), Quaternion.identity);
                    wall.transform.localScale = new Vector3(1, 1, 0.1f);
                    if (wallMaterial) wall.GetComponent<Renderer>().material = wallMaterial;
                }

                // West wall (only for left column to avoid duplicates)
                if (cell.walls[3] && x == 0)
                {
                    GameObject wall = Instantiate(wallPrefab, cellPos + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 90, 0));
                    wall.transform.localScale = new Vector3(1, 1, 0.1f);
                    if (wallMaterial) wall.GetComponent<Renderer>().material = wallMaterial;
                }
            }
        }

        // Add some atmospheric lighting (optional)
        if (mazeLight)
        {
            Light light = Instantiate(mazeLight, new Vector3(width / 2f, 10, height / 2f), Quaternion.Euler(90, 0, 0));
            light.intensity = 0.5f;
            light.range = Mathf.Max(width, height) * 2;
        }
    }
}

// Extension method to shuffle lists (add this to a separate static class if needed)
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
