// See https://aka.ms/new-console-template for more information
/*
   Find way from S -> E
   maze = new char[,] {
                    { 'S', '0', '0', '1', '0' },
                    { '1', '1', '0', '1', '0' },
                    { '0', '0', '0', '0', '0' },
                    { '0', '1', '1', '1', '0' },
                    { '0', '0', 'E', '1', '0' }
                    };


*/

using System.Text;

var M = 5;
var N = 5;
char[,] maze;
bool[,] visited = new bool[N, M];
int[] horizontalMove = {0, 0, 1, -1 };
int[] verticalMove = { 1, -1, 0, 0 };
Stack<(int, int)> path = new Stack<(int, int)>();

maze = new char[,]
{
    { 'S', '0', '0', '0', '0' },
    { '1', '1', '0', '1', '0' },
    { '0', '0', '0', '0', '0' },
    { '0', '1', '1', '0', '0' },
    { '0', '1', 'E', '0', '1' }
};

bool DFS(int row, int column)
{
    if (maze[row, column] == 'E')
    {
        path.Push((row, column));
        return true;
    }

    visited[row, column] = true;

    for (int direct = 0; direct < 4; direct++)
    {
        var newRow = row + horizontalMove[direct];
        var newColumn = column + verticalMove[direct];

        if (newRow < N && newRow >= 0 && newColumn < N && newColumn >= 0 && maze[newRow, newColumn] != '1' && !visited[newRow, newColumn])
        {
            var isSuccess = DFS(newRow, newColumn);

            if (isSuccess)
            {
                path.Push((row, column));
                return true;
            }
        }
    }



    return false;
}


// Tìm điểm xuất phát S 
// i, j là tạo độ của maze
var startX = 0; var startY = 0;
var endX = false;

for (int i = 0; i< N && !endX; i++)
{
    for (int j = 0; j< M; j++)
    {
        if (maze[i, j] == 'S')
        {
            startX = i;
            startY = j;
            endX = true;
            break;
        }
    }
}

var isSucces =DFS(startX, startY);

if (isSucces)
{
    Console.WriteLine("Path from S to E:");

    var sb = new StringBuilder();
    while (path.Count > 0)
    {
        var position = path.Pop();
        sb.Append($"({position.Item1} , {position.Item2}) -->");
    }

    Console.WriteLine(sb.ToString().Substring(0, sb.Length- 3));
}
else
{
    Console.WriteLine("Not Found Path :");
}