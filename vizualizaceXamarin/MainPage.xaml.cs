using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace vizualizaceXamarin
{
    public class Memory
    {
        char c = new char();
        StringBuilder sb = new StringBuilder();
        char[] memory;
        int i = 0;

        public Memory(int size)
        {
            memory = new char[size];
        }

        public void Push(char a)
        {
            memory[i] = a;
            i += 1;
        }
        public bool IsEmpty()
        {
            return i == 0;
        }
        public char Pop()
        {
            if (IsEmpty() == false)
            {
                c = memory[i - 1];
                i -= 1;
                return c;
            }
            return c;
        }
        public string Print()
        {
            sb.Clear();
            foreach (char c in memory)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
    public class Robot
    {
        public string kroky;
        public char[,,] MazeSteps;
        public char[,] Maze;
        //Memory memory = new Memory();
        public void Robotuj(string s)
        {
            int amountofsteps = 0;
            bool konec = false;
            int RobotX = 0;
            int RobotY = 0;
            int Possible = 0;
            string MazeRead = System.IO.File.ReadAllText(@s);
            string[] Rows = MazeRead.Split('\n');
            int width = 0;
            for (int i = 0; i < Rows.Length; i++)
            {
                string chars = string.Concat(Rows[i].Split(' ')).TrimEnd();
                width = chars.Length;
            }
            Maze = new char[Rows.Length, width];
            for (int i = 0; i < Rows.Length; i++)
            {
                string chars = string.Concat(Rows[i].Split(' ')).TrimEnd();
                for (int it = 0; it < chars.Length; it++)
                {
                    Maze[i, it] = chars[it];
                    if (chars[it] == 'Z')
                    {
                        RobotX = it;
                        RobotY = i;
                    }
                    if (chars[it] == '0')
                    {
                        Possible += 2;
                    }
                }
            }
            MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
            amountofsteps += 1;
            var memory = new Memory(Possible * 2);
            for (int i = 0; i < Possible; i++)
            {
                char Up = '1';
                char Down = '1';
                char Left = '1';
                char Right = '1';
                if (RobotY > 0)
                {
                    Up = Maze[RobotY - 1, RobotX];
                }
                if (RobotY < Rows.Length - 1)
                {
                    Down = Maze[RobotY + 1, RobotX];
                }
                if (RobotX > 0)
                {
                    Left = Maze[RobotY, RobotX - 1];
                }
                if (RobotX < width - 1)
                {
                    Right = Maze[RobotY, RobotX + 1];
                }
                if (konec == true)
                {
                    break;
                }
                //Hledání konce.
                if (Up == 1 && Down == 1 && Left == 1 && Right == 1)
                {
                    break;
                }
                if (Up == 'K')
                {
                    Maze[RobotY, RobotX] = '2';
                    RobotY -= 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    memory.Push('^');
                    kroky = memory.Print();
                    break;
                }
                else if (Right == 'K')
                {
                    Maze[RobotY, RobotX] = '2';
                    RobotX += 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    memory.Push('>');
                    kroky = memory.Print();
                    break;
                }
                else if (Down == 'K')
                {
                    Maze[RobotY, RobotX] = '2';
                    RobotY += 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    memory.Push('v');
                    kroky = memory.Print();
                    break;
                }
                else if (Left == 'K')
                {
                    Maze[RobotY, RobotX] = '2';
                    RobotX -= 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    memory.Push('<');
                    kroky = memory.Print();
                    break;
                }
                //Hledání dálšího volného místa
                if (Up == '0')
                {
                    Maze[RobotY, RobotX] = '2';
                    RobotY -= 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    memory.Push('^');
                    continue;
                }
                else if (Right == '0')
                {
                    Maze[RobotY, RobotX] = '2';
                    RobotX += 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    memory.Push('>');
                    continue;
                }
                else if (Down == '0')
                {
                    Maze[RobotY, RobotX] = '2';
                    RobotY += 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    memory.Push('v');
                    continue;
                }
                else if (Left == '0')
                {
                    Maze[RobotY, RobotX] = '2';
                    RobotX -= 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    memory.Push('<');
                    continue;
                }
                //Couvání a betonování pokud se nenalezl ani konec, ani volné místo
                char c = memory.Pop();
                if (c == '^')
                {
                    Maze[RobotY, RobotX] = '1';
                    RobotY += 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    continue;
                }
                else if (c == '>')
                {
                    Maze[RobotY, RobotX] = '1';
                    RobotX -= 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    continue;
                }
                else if (c == 'v')
                {
                    Maze[RobotY, RobotX] = '1';
                    RobotY -= 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    continue;
                }
                else if (c == '<')
                {
                    Maze[RobotY, RobotX] = '1';
                    RobotX += 1;
                    for (int ix = 0; ix < Maze.GetLength(0); ix++)
                    {
                        for (int it = 0; it < Maze.GetLength(1); it++)
                        {
                            MazeSteps[ix, it, amountofsteps] = Maze[ix, it];
                        }
                    }
                    MazeSteps[RobotX, RobotY, amountofsteps] = 'Z';
                    amountofsteps += 1;
                    continue;
                }

            }
        }
    }
    public partial class MainPage : ContentPage
    {
        private const int velikost = 40;
        private const int mezera = 5;
        public MainPage()
        {
            InitializeComponent();

        }
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var surface = args.Surface;
            var canvas = surface.Canvas;
            canvas.Clear();
            int marginX = 10;
            int marginY = 10;
            string Maze = @"Resources/Maze.txt";
            Robot robot = new Robot();
            robot.Robotuj(Maze);
            for (int j = 0; j < robot.MazeSteps.GetLength(0); j++)
            {
                for (int i = 0; i < robot.MazeSteps.GetLength(1); i++)
                {
                    marginX += mezera;
                    SKRect rect = new SKRect(marginX, marginY, velikost, velikost);
                    SKPaint paint;
                    if (robot.MazeSteps[j, i, 0] == '1')
                    {
                        paint = new SKPaint
                        {
                            Color = SKColors.Red
                        };
                    }
                    if (robot.MazeSteps[j, i, 0] == '2')
                    {
                        paint = new SKPaint
                        {
                            Color = SKColors.Gray
                        };
                    }
                    if (robot.MazeSteps[j, i, 0] == '0')
                    {
                        paint = new SKPaint
                        {
                            Color = SKColors.Green
                        };
                    }
                    if (robot.MazeSteps[j, i, 0] == 'K')
                    {
                        paint = new SKPaint
                        {
                            Color = SKColors.Black
                        };
                    }
                    if (robot.MazeSteps[j, i, 0] == 'Z')
                    {
                        paint = new SKPaint
                        {
                            Color = SKColors.Blue
                        };
                    }
                    else
                    {
                        paint = new SKPaint
                        {
                            Color = SKColors.Gray
                        };
                    }
                    canvas.DrawRect(rect, paint);
                }
            }
        }
    }
}
