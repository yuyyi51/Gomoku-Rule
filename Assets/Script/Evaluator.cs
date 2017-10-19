using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Evaluator
{
    public enum Rank
    {
        WIN = 0, ALIVE4 = 1, DDEAD4 = 2, D4A3 = 3, DALIVE3 = 4, D3A3 = 5, DEAD4 = 6,
        JDEAD4 = 7, ALIVE3 = 8, JALIVE3 = 9, DALIVE2 = 10, ALIVE2 = 11, JALIVE2 = 12, DEAD3 = 13,
        JDEAD3 = 14, DEAD2 = 15, JDEAD2 = 16, OTHER = 17, MEANINGLESS = 18
    };
    public static Dictionary<string, Rank> Figure;
    public static int[] Score;
    public static int[,] DeltaDirection;
    static Evaluator()
    {
        #region Figure_Init
        Figure = new Dictionary<string, Rank>();
        Figure.Add("11111", Rank.WIN);
        Figure.Add("011110", Rank.ALIVE4);
        Figure.Add("211110", Rank.DEAD4);
        Figure.Add("011112", Rank.DEAD4);
        Figure.Add("11101", Rank.JDEAD4);
        Figure.Add("10111", Rank.JDEAD4);
        Figure.Add("11011", Rank.JDEAD4);
        Figure.Add("011100", Rank.ALIVE3);
        Figure.Add("001110", Rank.ALIVE3);
        Figure.Add("011010", Rank.JALIVE3);
        Figure.Add("010110", Rank.JALIVE3);
        Figure.Add("211100", Rank.DEAD3);
        Figure.Add("001112", Rank.DEAD3);
        Figure.Add("211010", Rank.JDEAD3);
        Figure.Add("010112", Rank.JDEAD3);
        Figure.Add("210110", Rank.JDEAD3);
        Figure.Add("011012", Rank.JDEAD3);
        Figure.Add("11001", Rank.JDEAD3);
        Figure.Add("10011", Rank.JDEAD3);
        Figure.Add("10101", Rank.JDEAD3);
        Figure.Add("2011102", Rank.JDEAD3);
        Figure.Add("001100", Rank.ALIVE2);
        Figure.Add("01010", Rank.JALIVE2);
        Figure.Add("010010", Rank.JALIVE2);
        Figure.Add("211000", Rank.DEAD2);
        Figure.Add("000112", Rank.DEAD2);
        Figure.Add("210100", Rank.JDEAD2);
        Figure.Add("001012", Rank.JDEAD2);
        Figure.Add("210010", Rank.JDEAD2);
        Figure.Add("010012", Rank.JDEAD2);
        Figure.Add("10001", Rank.JDEAD2);
        Figure.Add("212", Rank.MEANINGLESS);
        Figure.Add("2112", Rank.MEANINGLESS);
        Figure.Add("2102", Rank.MEANINGLESS);
        Figure.Add("2012", Rank.MEANINGLESS);
        Figure.Add("21112", Rank.MEANINGLESS);
        Figure.Add("20112", Rank.MEANINGLESS);
        Figure.Add("21102", Rank.MEANINGLESS);
        Figure.Add("20012", Rank.MEANINGLESS);
        Figure.Add("21002", Rank.MEANINGLESS);
        Figure.Add("21012", Rank.MEANINGLESS);
        Figure.Add("211112", Rank.MEANINGLESS);
        Figure.Add("201112", Rank.MEANINGLESS);
        Figure.Add("211102", Rank.MEANINGLESS);
        Figure.Add("200112", Rank.MEANINGLESS);
        Figure.Add("211002", Rank.MEANINGLESS);
        Figure.Add("200012", Rank.MEANINGLESS);
        Figure.Add("210002", Rank.MEANINGLESS);
        Figure.Add("201002", Rank.MEANINGLESS);
        Figure.Add("200102", Rank.MEANINGLESS);
        Figure.Add("201102", Rank.MEANINGLESS);
        Figure.Add("210112", Rank.MEANINGLESS);
        Figure.Add("211012", Rank.MEANINGLESS);
        #endregion
        #region Score_Init
        Score = new int[19];
        Score[(int)Rank.WIN] = 99999;
        Score[(int)Rank.ALIVE4] = 50000;
        Score[(int)Rank.DDEAD4] = 50000;
        Score[(int)Rank.D4A3] = 50000;
        Score[(int)Rank.DALIVE3] = 10000;
        Score[(int)Rank.D3A3] = 5000;
        Score[(int)Rank.DEAD4] = 1000;
        Score[(int)Rank.JDEAD4] = 900;
        Score[(int)Rank.ALIVE3] = 500;
        Score[(int)Rank.JALIVE3] = 400;
        Score[(int)Rank.DALIVE2] = 100;
        Score[(int)Rank.ALIVE2] = 50;
        Score[(int)Rank.JALIVE2] = 40;
        Score[(int)Rank.DEAD3] = 20;
        Score[(int)Rank.JDEAD3] = 15;
        Score[(int)Rank.DEAD2] = 10;
        Score[(int)Rank.JDEAD2] = 5;
        Score[(int)Rank.OTHER] = 3;
        Score[(int)Rank.MEANINGLESS] = 1;
        #endregion
        #region DeltaDirection_Init
        DeltaDirection = new int[,]
        {
            { 1 , 0 } , { 0 , 1 } ,
            { 1 , 1 } , { 1 , -1 }
        };
        #endregion
    }
    public static Rank CheckRank(string str)
    {
        if (Figure.ContainsKey(str))
            return Figure[str];
        else
            return Rank.OTHER;
    }
    public static int CheckScore(Rank r)
    {
        return Score[(int)r];
    }
    public int[,] chessboard;
    public int[,] scoreboard;
    private int len1;
    private int len2;
    private int self;
    private int oppo;
    public Evaluator(int[,] board, int s, int o)
    {
        self = s;
        oppo = o;
        len1 = board.GetLength(0);
        len2 = board.GetLength(1);
        chessboard = (int[,])board.Clone();
        scoreboard = new int[len1, len2];
    }
    public List<KeyValuePair<int, int>> Evaluate()
    {
        List<KeyValuePair<int, int>> re = new List<KeyValuePair<int, int>>();
        int maxscore = -1;
        int score = 0;
        int score2 = 0;
        for (int i = 0; i < len1; ++i)
        {
            for (int j = 0; j < len2; ++j)
            {

                //scoreboard[i, j] = PointEvaluate(i, j);
                //to do
                //
                score = PointEvaluate(i, j, self, oppo);
                score2 = PointEvaluate(i, j, oppo, self) - 1;
                scoreboard[i, j] = score > score2 ? score : score2;
                if (scoreboard[i, j] > maxscore)
                    maxscore = scoreboard[i, j];
            }
        }
        if (maxscore == 0)
            return null;
        for (int i = 0; i < len1; ++i)
        {
            for (int j = 0; j < len2; ++j)
            {
                if (scoreboard[i, j] == maxscore)
                    re.Add(new KeyValuePair<int, int>(i, j));
            }
        }
        return re;
    }
    public int PointEvaluate(int x, int y, int s, int o)
    {
        if (chessboard[x, y] != 0)
            return 0;
        int re = 0;
        int[] buffer = new int[19];
        Rank r;
        Rank maxrank;
        string line;
        int pos;
        int score;
        int maxscore = -1;

        for (int i = 0; i != 4; ++i)
        {
            line = GetLine(x, y, DeltaDirection[i, 0], DeltaDirection[i, 1], s, o, out pos);
            score = LineEvaluate(line, pos, out r);
            buffer[(int)r]++;
            if (score > maxscore)
            {
                maxscore = score;
                maxrank = r;
            }
        }
        r = EvaluateRankForMutiLines(buffer);
        score = CheckScore(r);
        if (score > maxscore)
        {
            maxscore = score;
            maxrank = r;
        }
        re = maxscore;

        return re;
    }
    private Rank EvaluateRankForMutiLines(int[] buffer)
    {
        if (buffer[(int)Rank.ALIVE3] + buffer[(int)Rank.JALIVE3] >= 1 &&
            buffer[(int)Rank.DEAD4] + buffer[(int)Rank.JDEAD4] >= 1)
        {
            return Rank.D4A3;
        }
        if (buffer[(int)Rank.DEAD4] + buffer[(int)Rank.JDEAD4] >= 2)
        {
            return Rank.DDEAD4;
        }
        if (buffer[(int)Rank.ALIVE3] + buffer[(int)Rank.JALIVE3] >= 2)
        {
            return Rank.DALIVE3;
        }
        if (buffer[(int)Rank.ALIVE3] + buffer[(int)Rank.JALIVE3] >= 1 &&
            buffer[(int)Rank.DEAD3] + buffer[(int)Rank.JDEAD3] >= 1)
        {
            return Rank.D3A3;
        }
        if (buffer[(int)Rank.ALIVE2] + buffer[(int)Rank.JALIVE2] >= 2)
        {
            return Rank.DALIVE2;
        }
        return Rank.OTHER;
    }
    public string GetLine(int x, int y, int dx, int dy, int s, int o, out int pos)
    {
        string re = "";
        int tx = x;
        int ty = y;
        const int os = 2;
        const int ss = 1;
        pos = 0;
        int count = 0;
        while (IsLegal(tx + dx, ty + dy) && chessboard[tx + dx, ty + dy] != o && count < 7)
        {
            ++count;
            tx += dx;
            ty += dy;
        }
        if (count != 7)
        {
            re += (char)(os + '0');
        }
        while (tx != x || ty != y)
        {
            re += (char)((chessboard[tx, ty] == s ? ss : 0) + '0');
            tx -= dx;
            ty -= dy;
        }
        re += (char)(ss + '0');
        pos = re.Length - 1;
        count = 0;
        while (IsLegal(tx - dx, ty - dy) && chessboard[tx - dx, ty - dy] != o && count < 7)
        {
            ++count;
            tx -= dx;
            ty -= dy;
            re += (char)((chessboard[tx, ty] == s ? ss : 0) + '0');
        }
        if (count != 7)
        {
            re += (char)(os + '0');
        }
        return re;
    }
    public bool IsLegal(int x, int y)
    {
        return x >= 0 && x < len1 && y >= 0 && y < len2;
    }
    public int LineEvaluate(string line, int pos, out Rank maxrank)
    {
        int re = 0;
        Rank r;
        int score;
        //Rank maxrank;
        int maxscore = -1;
        maxrank = Rank.OTHER;
        for (int i = 0; i < line.Length; ++i)
        {
            for (int j = 1; j <= 7 && i + j <= line.Length; ++j)
            {
                if (i + j - 1 < pos)
                {
                    continue;
                }
                r = CheckRank(line.Substring(i, j));
                score = CheckScore(r);
                if (score > maxscore)
                {
                    maxscore = score;
                    maxrank = r;
                }
            }
        }
        re = maxscore;
        return re;
    }
}

