﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    class DFS
    {
        private Stack<Simpul> visited;
        private Stack<Simpul> visitedHome;
        private Stack<Simpul> result;
        private int count;
        private int countSteps;
        private int totalTreasure;
        private int countVisited;
        private int[,] maze;
        private int[,] visitedMaze;
        private int x;
        private int y;
        public DFS(int x, int y, int total, int[,] maze)
        {
            this.visited = new Stack<Simpul>();
            this.visitedHome = new Stack<Simpul>();
            this.result = new Stack<Simpul>();
            this.x = x;
            this.y = y;
            this.totalTreasure = total;
            this.maze = maze;
            this.visitedMaze = (int[,])maze.Clone();
            this.count = 0;
            this.countSteps = 0;
            this.countVisited = 0;
        }
        public void findHome()
        {
            findPath();
            Simpul first = new Simpul(visited.Peek());
            visitedMaze[first.getX(), first.getY()] = -2;
            visitedHome.Push(first);
            result.Push(first);

            Simpul top;
            bool valid;
            while (true)
            {
                valid = true;
                top = new Simpul(visitedHome.Pop());
                if (top.isHome(top.getX(), top.getY()))
                {
                    visitedHome.Push(top);
                    break;
                }
                if (top.canGoRight && valid)
                {
                    if (visitedMaze[top.getX(), top.getY() + 1] != -2)
                    {
                        top.canGoRight = false;
                        Simpul temp = new Simpul(top.getX(), top.getY() + 1, maze);
                        visitedHome.Push(top);
                        visitedHome.Push(temp);
                        result.Push(temp);
                        visitedMaze[top.getX(), top.getY() + 1] = -2;
                        valid = false;
                    }
                }
                if (top.canGoDown && valid)
                {
                    if (visitedMaze[top.getX() + 1, top.getY()] != -2)
                    {
                        top.canGoDown = false;
                        Simpul temp = new Simpul(top.getX() + 1, top.getY(), maze);
                        visitedHome.Push(top);
                        visitedHome.Push(temp);
                        result.Push(temp);
                        visitedMaze[top.getX() + 1, top.getY()] = -2;
                        valid = false;
                    }
                }
                if (top.canGoLeft && valid)
                {
                    if (visitedMaze[top.getX(), top.getY() - 1] != -2)
                    {
                        top.canGoLeft = false;
                        Simpul temp = new Simpul(top.getX(), top.getY() - 1, maze);
                        visitedHome.Push(top);
                        visitedHome.Push(temp);
                        result.Push(temp);
                        visitedMaze[top.getX(), top.getY() - 1] = -2;
                        valid = false;
                    }
                }
                if (top.canGoUp && valid)
                {
                    if (visitedMaze[top.getX() - 1, top.getY()] != -2)
                    {
                        top.canGoUp = false;
                        Simpul temp = new Simpul(top.getX() - 1, top.getY(), maze);
                        visitedHome.Push(top);
                        visitedHome.Push(temp);
                        result.Push(temp);
                        visitedMaze[top.getX() - 1, top.getY()] = -2;
                        valid = false;
                    }
                }
                if (valid)
                {
                    visitedHome.Push(top);
                    top = visitedHome.Pop();
                    result.Push(visitedHome.Peek());
                }
            }
        }
        
        public void findPath()
        {
            Simpul first = new Simpul(this.x, this.y, maze);
            visitedMaze[x, y] = -1;
            visited.Push(first);
            result.Push(first);
            /*
                urutan prioritas :
                - kanan
                - bawah
                - kiri
                - atas
            */
            Simpul top;
            bool valid;
            while (true)
            {
                valid = true;
                top = new Simpul(visited.Pop());
                if (maze[top.getX(), top.getY()] == 3)
                {
                    count++;
                    maze[top.getX(), top.getY()] = 2;
                }
                if (count == totalTreasure)
                {
                    visited.Push(top);
                    break;
                }
                if (top.canGoRight && valid)
                {
                    if (visitedMaze[top.getX(), top.getY() + 1] != -1)
                    {
                        top.canGoRight = false;
                        Simpul temp = new Simpul(top.getX(), top.getY() + 1, maze);
                        visited.Push(top);
                        visited.Push(temp);
                        result.Push(temp);
                        visitedMaze[top.getX(), top.getY() + 1] = -1;
                        valid = false;
                    }
                }
                if (top.canGoDown && valid)
                {
                    if (visitedMaze[top.getX() + 1, top.getY()] != -1)
                    {
                        top.canGoDown = false;
                        Simpul temp = new Simpul(top.getX() + 1, top.getY(), maze);
                        visited.Push(top);
                        visited.Push(temp);
                        result.Push(temp);
                        visitedMaze[top.getX() + 1, top.getY()] = -1;
                        valid = false;
                    }
                }
                if (top.canGoLeft && valid)
                {
                    if (visitedMaze[top.getX(), top.getY() - 1] != -1)
                    {
                        top.canGoLeft = false;
                        Simpul temp = new Simpul(top.getX(), top.getY() - 1, maze);
                        visited.Push(top);
                        visited.Push(temp);
                        result.Push(temp);
                        visitedMaze[top.getX(), top.getY() - 1] = -1;
                        valid = false;
                    }
                }
                if (top.canGoUp && valid)
                {
                    if (visitedMaze[top.getX() - 1, top.getY()] != -1)
                    {
                        top.canGoUp = false;
                        Simpul temp = new Simpul(top.getX() - 1, top.getY(), maze);
                        visited.Push(top);
                        visited.Push(temp);
                        result.Push(temp);
                        visitedMaze[top.getX() - 1, top.getY()] = -1;
                        valid = false;
                    }
                }
                if (valid)
                {
                    visited.Push(top);
                    top = visited.Pop();
                    result.Push(visited.Peek());
                }
                this.countSteps++;
            }
        }
        public void displayPath()
        {
            Stack<Simpul> copy = new Stack<Simpul>(this.result);
            foreach (Simpul value in copy)
            {
                value.displaySimpul();
            }
        }

        public void displaySteps()
        {
            Console.WriteLine(this.countSteps);
        }

        public void displayVisited()
        {
            for (int i = 0; i < this.visitedMaze.GetLength(0); i++)
            {
                for (int j = 0; j < this.visitedMaze.GetLength(1); j++)
                {
                    if (this.visitedMaze[i, j] == -1)
                    {
                        this.countVisited++;
                    }
                }
            }
            Console.WriteLine(this.countVisited);
        }

        public Stack<Simpul> getResult()
        {
            return this.result;
        }
        public String getRoute()
        {
            String ret = "";
            Stack<Simpul> copy = new Stack<Simpul>(this.result);
            bool first = true;
            Simpul q = copy.Pop();
            while(copy.Count>0)
            {
                Simpul p = q;
                if (first)
                {
                    first = false;
                }
                else
                {
                    q = copy.Pop();
                } 
                int px = p.getX();
                int py = p.getY();
                int x = q.getX();
                int y = q.getY();
                if (x-px == 1)
                {
                    ret+="D ";
                }
                else if (x-px == -1)
                {
                    ret+="U ";
                }
                else if(y-py == 1)
                {
                    ret+="R ";
                }
                else if (y-py == -1)
                {
                    ret+="L ";
                }
            }
            return ret;
        }
        public int getCountVisited()
        {
            return this.result.Count;
        }
        public int getStep()
        {
            return this.result.Count-1;
        }
    }
}
