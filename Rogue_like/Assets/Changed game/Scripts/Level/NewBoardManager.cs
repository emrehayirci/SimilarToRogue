using Assets.Changed_game.Scripts.Inventory;
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
        public GameObject[] randomCollectable;

        public TileType[,] BoardTiles;
		public ShopKeeper shopKeeper;



        //PickUp Items Container for ground items
        public List<PickupItem> pickupItems = new List<PickupItem>();

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
          BoardSetup();
          InitializeList();
          int random = (int)Random.Range(0,10);
          if (random == 5) {
            Instantiate (shopKeeper, new Vector3(columns/2,rows/2,0f),Quaternion.identity);
            LayoutObectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum, TileType.Food);
          } else {
            LayoutObectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum, TileType.Wall);
            LayoutObectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum, TileType.Food);
            int enemyCount = (int)Mathf.Log (level, 2f);
            LayoutObectAtRandom (enemyTiles, enemyCount, enemyCount,TileType.Enemy);
            LayoutObectAtRandom(randomCollectable, 5, 6, TileType.Empty);
          }
          Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f),Quaternion.identity);
        }


        public void AddPickupItem(PickupItem item, Vector2 location)
        {
            Vector2 newLocation;

            if(GetEmptyLocationNear(location, out newLocation))
            {
                item.location = newLocation;
                pickupItems.Add(item);
            }
            else
            {
                //if there is no suitable place
                return; // basicly equals to deleting the object
            }

            //find Item prefab
            GameObject itemPrefab = null;
            foreach(var prefab in randomCollectable)
            {
                if(prefab.GetComponent<PickUpBehavior>().PickupTypeId == item.PickUpTypeId)
                {
                    itemPrefab = prefab;
                }
            }
            if(itemPrefab != null)
            {
                itemPrefab.transform.position = item.location;
                Instantiate(itemPrefab);
            }
            
            return;
        }

        //Basic Level, needs to improve
        public bool GetEmptyLocationNear(Vector2 start, out Vector2 location)
        {
            

            int x = (int)start.x;
            int y = (int)start.y;
            if (x < BoardTiles.GetLength(0) - 1 && BoardTiles[x + 1,y] == TileType.Empty)
            {
                location = new Vector2(x + 1, y);
                return true;
            }
            else if (y < BoardTiles.GetLength(1) - 1 && BoardTiles[x, y + 1] == TileType.Empty)
            {
                location = new Vector2(x, y + 1);
                return true;
            }
            else if (x > 0  && BoardTiles[x - 1, y] == TileType.Empty)
            {
                location = new Vector2(x - 1, y);
                return true;
            }
            else if (y > 0 && BoardTiles[x, y - 1] == TileType.Empty)
            {
                location = new Vector2(x, y - 1);
                return true;
            }

            location = start;
            return false;
        }
    }
}
