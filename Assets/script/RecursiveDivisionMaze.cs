using System;

public class RecursiveDivisionMaze
{
    const int S = 1;
    const int E = 2;

    const int HORIZONTAL = 1;
    const int VERTICAL = 2;

    System.Random rng;

    public int[,] Generate(int width, int height, int? seed = null)
    {
        rng = seed.HasValue ? new System.Random(seed.Value) : new System.Random();

        int[,] grid = new int[height, width];

        Divide(grid, 0, 0, width, height, ChooseOrientation(width, height));

        return grid;
    }

    int ChooseOrientation(int width, int height)
    {
        if (width < height) return HORIZONTAL;
        if (height < width) return VERTICAL;
        return rng.Next(2) == 0 ? HORIZONTAL : VERTICAL;
    }

    void Divide(int[,] grid, int x, int y, int width, int height, int orientation)
    {
        if (width < 2 || height < 2)
            return;

        bool horizontal = orientation == HORIZONTAL;

        int wx = x + (horizontal ? 0 : rng.Next(width - 2));
        int wy = y + (horizontal ? rng.Next(height - 2) : 0);

        int px = wx + (horizontal ? rng.Next(width) : 0);
        int py = wy + (horizontal ? 0 : rng.Next(height));

        int dx = horizontal ? 1 : 0;
        int dy = horizontal ? 0 : 1;

        int length = horizontal ? width : height;
        int dir = horizontal ? S : E;

        for (int i = 0; i < length; i++)
        {
            if (!(wx == px && wy == py))
                grid[wy, wx] |= dir;

            wx += dx;
            wy += dy;
        }

        int nx = x;
        int ny = y;
        int w = horizontal ? width : (wx - x);
        int h = horizontal ? (wy - y) : height;

        Divide(grid, nx, ny, w, h, ChooseOrientation(w, h));

        nx = horizontal ? x : (wx + 1);
        ny = horizontal ? (wy + 1) : y;
        w = horizontal ? width : (x + width - wx - 1);
        h = horizontal ? (y + height - wy - 1) : height;

        Divide(grid, nx, ny, w, h, ChooseOrientation(w, h));
    }
}
