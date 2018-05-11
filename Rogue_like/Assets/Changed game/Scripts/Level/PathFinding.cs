using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Priority_Queue;
using UnityEngine;

namespace Assets.Changed_game.Scripts.Level
{
    class PathFinding
    {
        public enum Moves { Up, Down, Right, Left }
        public static List<Moves> MoveQueue;

        public static Dictionary<Vector2, Vector2> cameFrom;
        public static Dictionary<Vector2, double> costSoFar;

        /*
            Heuristic function for A* pathfinding. Simply prioritize movements that will be done.
            Calculates Heuristical cost of movement
            A* is a path finding algorithm prioritize the direction (heuristic for our case)
         */
        static public double Heuristic(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }



        public static void getNeighbours(Vector2 location){
            //add up down right left in list and return
            //(int)location.x;
        }


        /* 
            Implements A* path finding algorithm (Proved to be best solution of that kind of problems)
            PARAMS: 
                Takes tiles arrray that board manager probably have. 
                startx and starty coordinates for starting position on the map
                endx and endy coordinates for ending position
        */
        public static bool Calculate(TileType [,] tiles, Vector2 start, Vector2 end)
        {
            cameFrom = new Dictionary<Vector2, Vector2>();
            costSoFar = new Dictionary<Vector2, double>();
            var frontier = new FastPriorityQueue<Location>(100);
            frontier.Enqueue(new Location(start), 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue().position;

                if (current.Equals(end))
                {
                    break;
                }

                //get neighbours
                List<Vector2> neighbours = new List<Vector2>();
                if(current.x < tiles.GetLength(0) -1 ){ //first dimension
                    neighbours.Add(new Vector2(current.x + 1, current.y));
                }

                if(current.y < tiles.GetLength(0) - 1 ){ //sec dimension
                    neighbours.Add(new Vector2(current.x, current.y + 1));
                }

                if(current.x > 0 ){
                    neighbours.Add(new Vector2(current.x - 1, current.y));
                }

                if(current.y < 0 ){
                    neighbours.Add(new Vector2(current.x, current.y - 1));
                }

                foreach (var next in neighbours)
                {
                    double newCost = costSoFar[current];
                    if (tiles[(int)next.x, (int)next.y] == TileType.Wall)
                    {
                        Debug.Log("Before " + newCost);
                    }
                    newCost = costSoFar[current] + ((tiles[(int)next.x, (int)next.y] == TileType.Wall) ? 3 : 1);
                    if(tiles[(int)next.x, (int)next.y] == TileType.Wall)
                    {
                        Debug.Log("After " + newCost);
                    }

                    if (!costSoFar.ContainsKey(next)
                        || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        double priority = newCost + Heuristic(next, end);
                        frontier.Enqueue(new Location(next), (float)priority);
                        cameFrom[next] = current;
                    }
                }
            }
            Debug.Log(costSoFar);
            Debug.Log(cameFrom);
            BackTrace(start, end);
            return false;
        }        

        private static void BackTrace(Vector2 start, Vector2 end)
        {
            
            if (cameFrom.ContainsKey(end))
            {
                var current = end;
                while (current != start)
                {
                    Debug.Log(current);
                    Debug.Log(costSoFar[current]);
                    current = cameFrom[current];
                }
            }
            else
            {
                Debug.Log("No way Bro!");
            }
           
           
        }

    }

    class Location: FastPriorityQueueNode
    {
        public Vector2 position { get; set; }

        public Location(Vector2 position)
        {
            this.position = position;
        }
    }
}
