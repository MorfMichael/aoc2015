//row 2947, column 3029
using System.Runtime.CompilerServices;

int row = 2947;
int column = 3029;

long[,] map = new long[5000, 5000];

int max = 0;
long previous = 20151125;
long count = 0;

int r = max, c = 0;
while (true)
{
    r = max; 
    c = 0;
    while (r >= 0)
    {
        while (c <= max)
        {
            //Console.WriteLine($"[{r},{c}]");
            if (r > 0 || c > 0) previous = GetValue(previous);
            if (r+1 == row && c+1 == column)
            {
                goto Result;
            }
            //Console.WriteLine($"[{r+1},{c+1}]");
            //Console.ReadKey();
            r--;
            c++;
        }
    }

    max++;
}

Result:
//14421559
//26405906
Console.WriteLine($"[{r},{c}]");
Console.WriteLine(previous);



long GetValue(long current)
{
    return (current * 252533) % 33554393;
}