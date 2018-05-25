using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Changed_game.Scripts.Level
{
    public class NewBoardManager : MonoBehaviour {
        [Serializable]
        public class Count
        {
            public int minimum;
            public int maximum;

            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }

        public int columns = 8;
        public int rows = 8;
        public Count wallCount = new Count(5, 9);
        public Count foodCount = new Count(1, 5);
        public GameObject exit;
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;
        public GameObject[] enemyTiles;
        public GameObject[] outerWallTiles;
        public TileType[,] BoardTiles;

        private Transform boardHolder;
        private List<Vector3> gridPositions = new List<Vector3>();

        void InitializeList()
        {
            
            gridPositions.Clear();

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if ((x == 0 & y == 0) || (x == columns - 1 && y == rows - 1))
                        continue;

                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }

        void BoardSetup()
        {
            boardHolder = new GameObject("Board").transform;
            BoardTiles = new TileType[columns, rows];
            for (int x = -1; x < columns + 1; x++)
            {
                for (int y = -1; y < rows + 1; y++)
                {
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    }
                    else
                    {//if it is not an outer object update map with floor
                        BoardTiles[x, y] = TileType.Empty;
                    }

                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);
                }
            }
        }

        Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            gridPositions.RemoveAt(randomIndex);
            return randomPosition;
        }

        void LayoutObectAtRandom(GameObject[] tileArray, int minimum, int maximum, TileType type)
        {
            int objectCount = Random.Range(minimum, maximum + 1);

            for (int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = RandomPosition();
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
                BoardTiles[(int)randomPosition.x, (int)randomPosition.y] = type;
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }

        public void SetupScene(int level)
        {
            //do
            {
                BoardSetup();
                InitializeList();
                LayoutObectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum, TileType.Wall);
                LayoutObectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum, TileType.Food);
                int enemyCount = (int)Mathf.Log(level, 2f);
                LayoutObectAtRandom(enemyTiles, enemyCount, enemyCount, TileType.Enemy);
                Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
                Debug.Log("exit");
            }
            //while (PathFinding.Calculate(new Vector2(0,0), new Vector2(7, 7)).Count == 0); //Pathfinding returns no moves
        }
    }
}
