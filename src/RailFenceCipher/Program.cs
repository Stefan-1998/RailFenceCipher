namespace Cipher.RailFence;

static public class RailFenceCipher
{
    static void Main() { }
    public static string Encode(string s, int n)
    {
        if (String.IsNullOrEmpty(s)) { return String.Empty; }
        var output = String.Empty;
        for (int i = 0; i < n; i++)
        {
            int cycle = 0;
            var indexes = CalculateIndexes(cycle, n, i);

            while (indexes.First < s.Length)
            {
                cycle++;
                if (IsMinOrMaxDepth(indexes, i, n))
                {
                    AddLetterToOutput(ref output, indexes.First, s);
                }
                else
                {
                    AddLetterToOutput(ref output, indexes.First, s);
                    AddLetterToOutput(ref output, indexes.Second, s);
                }
                indexes = CalculateIndexes(cycle, n, i);
            }
        }
        return output;
    }
    private static bool IsMinOrMaxDepth((int First, int Second) indexes, int currentDepth, int maxDepth) => indexes.First == indexes.Second || IsMaxDepth(currentDepth, maxDepth);
    private static (int First, int Second) CalculateIndexes(int cycle, int depth, int currentline)
    {
        return (First: CalculateFirstIndex(cycle, depth, currentline), Second: CalculateSecondIndex(cycle, depth, currentline));
    }
    private static void AddLetterToOutput(ref string output, int index, string originalString)
    {
        if (IsValidIndex(index, originalString)) { output += originalString[index].ToString(); }
    }
    //currentDepth starts at 0
    private static bool IsMaxDepth(int currentDepth, int maxDepth) => maxDepth == currentDepth + 1;

    private static int CalculateSecondIndex(int cycle, int depth, int currentline)
    {
        return cycle * (2 * depth - 2) + currentline;
    }
    private static int CalculateFirstIndex(int cycle, int depth, int currentline)
    {
        return cycle * (2 * depth - 2) - currentline;
    }
    private static bool IsValidIndex(int index, string text)
    {
        if (index < 0) { return false; }
        if (index > text.Length - 1) { return false; }
        return true;
    }

    public static string Decode(string s, int n)
    {
        if (String.IsNullOrEmpty(s)) { return String.Empty; }
        char[,] rail = new char[n, s.Length];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < s.Length; j++)
                rail[i, j] = '\n';

        bool isDownwardDirection = true;
        int row = 0;
        int col = 0;

        for (int i = 0; i < s.Length; i++)
        {
            // check the direction of flow
            if (row == 0)
                isDownwardDirection = true;
            if (row == n - 1)
                isDownwardDirection = false;

            // place the marker
            rail[row, col++] = '*';

            // find the next row using direction flag
            row = isDownwardDirection ? row + 1 : row - 1;
        }

        // now we can construct the fill the rail matrix
        int index = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < s.Length; j++)
            {
                if (rail[i, j] == '*' && index < s.Length)
                { rail[i, j] = s[index++]; }
            }
        }



        row = 0;
        col = 0;
        string output = String.Empty;
        for (int i = 0; i < s.Length; i++)
        {
            // check the direction of flow
            if (row == 0)
                isDownwardDirection = true;
            if (row == n - 1)
                isDownwardDirection = false;

            // place the marker
            if (rail[row, col] != '*')
            {
                output += rail[row, col].ToString();
                col++;
            }

            // find the next row using direction flag
            row = isDownwardDirection ? row++ : row--;
        }

        return output;
    }
}
