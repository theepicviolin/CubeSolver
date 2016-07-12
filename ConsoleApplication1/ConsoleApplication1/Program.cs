using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    enum Color { Red = 1, Orange = 2, White = 3, Yellow = 4, Blue = 5, Green = 6 };
    enum Face { F, U, R, D, L, B };
    
    class Program
    {
        static Cublet[] cublets = { new Corner(1, 1, 1, Color.Red, Color.White, Color.Blue), new Corner(1, -1, 1, Color.Green, Color.White, Color.Red), new Corner(-1, -1, 1, Color.Orange, Color.White, Color.Green), new Corner(-1, 1, 1, Color.Blue, Color.White, Color.Orange), new Edge(1, 0, 1, Color.Red, Color.White), new Edge(0, -1, 1, Color.Green, Color.White), new Edge(-1, 0, 1, Color.Orange, Color.White), new Edge(0, 1, 1, Color.Blue, Color.White), new Center(0, 0, 1, Color.White) };
        static void Main(string[] args)
        {
            char c;
            do
            {
                displayCube();
                c = Console.ReadKey().KeyChar;
                if (c == 'a')
                    rotateFace(Face.F, -1);
                else if (c == 'b')
                    rotateFace(Face.F, 1);
                Console.WriteLine();
                //displayCube();
                Console.WriteLine();
                Console.WriteLine();
            }
            while (c != 'q');

            Console.WriteLine("Press any key to exit . . .");
            Console.ReadKey();
        }

        static void enumToColor(Color c)
        {
            switch (c)
            {
                case Color.Blue:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Color.Orange:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case Color.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

            }
            Console.Write("██");
            //Console.Write(string.Format("{0, 7}", c.ToString()));
        }

        static void displayCube()
        {
            int x = -1;
            int y = 1;

            for (int i = 0; i < cublets.Length; i++)
            {
                Cublet c = cublets[i];
                if (c.x == x && c.y == y)
                {
                    enumToColor(c.c1);
                    //Console.Write(c.c1 + " ");
                    x++;
                    if (x == 2)
                    {
                        x = -1;
                        y--;
                        Console.WriteLine();
                    }
                    i = -1;
                }
            }
        }
        
        static void rotateFace(Face f, int direction)
        {
            foreach (Cublet c in cublets)
            {
                c.rotate(f, direction);
            }
        }
    }

    abstract class Cublet
    {
        public int x;
        public int y;
        public int z;
        public int a;
        public int b;
        public Color c1;

        public abstract void rotate(Face f, int dir);

        public void to2D(Face f)
        {
            switch (f)
            {
                case Face.B:
                    a = -x;
                    b = y;
                    break;
                case Face.D:
                    a = x;
                    b = z;
                    break;
                case Face.F:
                    a = x;
                    b = y;
                    break;
                case Face.L:
                    a = z;
                    b = y;
                    break;
                case Face.R:
                    a = -z;
                    b = y;
                    break;
                case Face.U:
                    a = x;
                    b = -z;
                    break;
            }
        }

        public void to3D(Face f)
        {
            switch (f)
            {
                case Face.B:
                    x = -a;
                    y = b;
                    break;
                case Face.D:
                    x = a;
                    z = b;
                    break;
                case Face.F:
                    x = a;
                    y = b;
                    break;
                case Face.L:
                    z = a;
                    y = b;
                    break;
                case Face.R:
                    z = -a;
                    y = b;
                    break;
                case Face.U:
                    x = a;
                    z = -b;
                    break;
            }
        }
    }

    class Corner : Cublet
    {
        Color c2;
        Color c3;

        public Corner (int xpos, int ypos, int zpos, Color color1, Color color2, Color color3)
        {
            x = xpos;
            y = ypos;
            z = zpos;
            c1 = color1;
            c2 = color2;
            c3 = color3;
        }

        public override void rotate(Face f, int dir)
        {
            to2D(f);
            if (a == dir * b)
                b = -b;
            else
                a = -a;
            to3D(f);
        }
    }

    class Edge : Cublet
    {
        Color c2;

        public Edge (int xpos, int ypos, int zpos, Color color1, Color color2)
        {
            x = xpos;
            y = ypos;
            z = zpos;
            c1 = color1;
            c2 = color2;
        }

        public override void rotate(Face f, int dir)
        {
            to2D(f);
            if (a == 0)
            {
                a = dir * b;
                b = 0;
            }
            else if (b == 0)
            {
                b = dir * -a;
                a = 0;
            }
            to3D(f);
        }
    }

    class Center : Cublet
    {
        public Center(int xpos, int ypos, int zpos, Color color)
        {
            x = xpos;
            y = ypos;
            z = zpos;
            c1 = color;
        }

        public override void rotate(Face f, int dir)
        {
            
        }
    }
}
