namespace Cipher.RailFence;

static public class RailFenceCipher
{
    static void Main() { }


    private enum Direction { Up, Down }
    public static string Encode(string s, int n)
    {
        if (String.IsNullOrEmpty(s)) { return String.Empty; }
        char[,] rail = new char[n, s.Length];
        InitializeRail(ref rail, n, s.Length);
        FillDiagonalsWithMessage(ref rail, n, s);
        return ReadOutEncryptedMessagen(ref rail, n, s.Length);
    }
    public static string Decode(string s, int n)
    {
        if (String.IsNullOrEmpty(s)) { return String.Empty; }
        char[,] rail = new char[n, s.Length];
        InitializeRail(ref rail, n, s.Length);
        FillDiagonalsWithPlaceHolder(ref rail, n, s.Length);
        FillCipher(ref rail, n, s);
        return ReadOutDecryptedMessage(ref rail, n, s.Length);
    }

    private static string ReadOutEncryptedMessagen(ref char[,] rails, int depth, int messageLength)
    {
        string output = String.Empty;
        for (int i = 0; i < depth; i++)
            for (int j = 0; j < messageLength; j++)
                if (rails[i, j] != (char)0b1111111)
                {
                    output += rails[i, j].ToString();
                }
        return output;
    }
    private static int MoveToNextRow(int row, Direction currentDirection)
    {
        if (currentDirection == Direction.Down) return row + 1;
        return row - 1;
    }

    private static Direction GetCurrentDirection(int row, int depth, Direction currentDirection)
    {
        if (row == 0 && currentDirection == Direction.Up) return Direction.Down;
        if (row == depth - 1 && currentDirection == Direction.Down) return Direction.Up;
        return currentDirection;
    }
    private static void FillDiagonalsWithPlaceHolder(ref char[,] rails, int depth, int length)
    {
        Direction currentDirection = Direction.Down;
        int row = 0;

        for (int i = 0; i < length; i++)
        {
            currentDirection = GetCurrentDirection(row, depth, currentDirection);
            rails[row, i] = '*';
            row = MoveToNextRow(row, currentDirection);
        }
    }
    private static void FillDiagonalsWithMessage(ref char[,] rails, int depth, string message)
    {
        Direction currentDirection = Direction.Down;
        int row = 0;

        for (int i = 0; i < message.Length; i++)
        {
            currentDirection = GetCurrentDirection(row, depth, currentDirection);
            rails[row, i] = message[i];
            row = MoveToNextRow(row, currentDirection);
        }
    }
    private static void FillCipher(ref char[,] rails, int depth, string ReadOutEncryptedMessagen)
    {
        int index = 0;
        for (int i = 0; i < depth; i++)
        {
            for (int j = 0; j < ReadOutEncryptedMessagen.Length; j++)
            {
                if (rails[i, j] == '*' && index < ReadOutEncryptedMessagen.Length)
                { rails[i, j] = ReadOutEncryptedMessagen[index++]; }
            }
        }
    }
    private static string ReadOutDecryptedMessage(ref char[,] rails, int depth, int messageLength)
    {
        int row = 0;
        Direction currentDirection = Direction.Down;
        string output = String.Empty;
        for (int i = 0; i < messageLength; i++)
        {
            currentDirection = GetCurrentDirection(row, depth, currentDirection);

            if (rails[row, i] != '*')
            {
                output += rails[row, i].ToString();
            }
            row = MoveToNextRow(row, currentDirection);
        }
        return output;
    }
    private static void InitializeRail(ref char[,] rails, int depth, int messageLength)
    {
        //Without setting the default value. The current memory entries are used -> Could cause sporadic erros
        for (int i = 0; i < messageLength; i++)
            for (int j = 0; j < depth; j++)
                rails[j, i] = (char)0b1111111;
    }


}
