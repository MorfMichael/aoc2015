using System.Globalization;
using System.Security.Cryptography.X509Certificates;

string[] input = File.ReadAllLines("level18.in");
int steps = 100;
int size = 100;
bool[,] map = new bool[size, size];

for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        map[i, j] = input[i][j] == '#';
    }
}

for (int c = 0; c < steps; c++)
{
    bool[,] newmap = new bool[size,size];

    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            if (IsCorner(j, i))
            {
                newmap[i, j] = true;
                continue;
            }

            var neighbours = GetNeighbours(i, j);
            if (map[i, j])
            {
                var non = neighbours.Count(x => x.value);
                newmap[i, j] = non == 2 || non == 3;
            }
            else
            {
                newmap[i, j] = neighbours.Count(x => x.value) == 3;
            }
        }
    }
    map = newmap;
}


int count = 0;
for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        if (map[i, j]) count++;
    }
}


Console.WriteLine(count);

List<(int x, int y, bool value)> GetNeighbours(int y, int x)
{
    return new()
    {
        (x-1, y, GetValue(x-1,y)),
        (x+1, y, GetValue(x+1,y)),
        (x, y-1, GetValue(x,y-1)),
        (x, y+1, GetValue(x,y+1)),
        (x-1, y-1, GetValue(x-1,y-1)),
        (x+1, y+1, GetValue(x+1,y+1)),
        (x-1, y+1, GetValue(x-1,y+1)),
        (x+1, y-1, GetValue(x+1,y-1)),
    };
}

bool IsCorner(int x, int y) => ((x == 0 && y == 0) ||
        (x == map.GetLength(1) - 1 && y == 0) ||
        (x == 0 && y == map.GetLength(0) - 1) ||
        (x == map.GetLength(1) - 1 && y == map.GetLength(0) - 1));

    bool GetValue(int x, int y)
{
    if (IsCorner(x,y)) return true;

    if (x >= 0 && x < map.GetLength(1) && y >= 0 && y < map.GetLength(0)) return map[y, x];
    return false;
}