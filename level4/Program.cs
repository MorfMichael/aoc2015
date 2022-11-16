using System.Security.Cryptography;
using System.Text;

string input = File.ReadAllText("level4.in");

Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));

int Part1(string input)
{
    int i = 1;
    string output = CreateMD5(input+i);
    while (!output.StartsWith("00000")) output = CreateMD5(input + ++i);
    return i;
}

int Part2(string input)
{
    int i = 1;
    string output = CreateMD5(input + i);
    while (!output.StartsWith("000000")) output = CreateMD5(input + ++i);
    return i;
}


string CreateMD5(string input)
{
    using (var md5 = MD5.Create())
    {
        byte[] input_bytes = Encoding.ASCII.GetBytes(input);
        byte[] hashed = md5.ComputeHash(input_bytes);

        return Convert.ToHexString(hashed);
    }
}