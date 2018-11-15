using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{

    public class Grid : MonoBehaviour
    {
        #region Singleton
        public static Grid Instance;
        private void Awake()
        {
            Instance = this;
        }
        private void OnDestroy()
        {
            Instance = null;
        }
        #endregion

        public int width = 10, height = 20;

        // 2D Grid for storing the blocks
        public Transform[,] data;

        private void OnDrawGizmos()
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    Gizmos.DrawWireCube(new Vector3(x, y), Vector3.one);
                }
            }
        }
        // Use this for initialization
        void Start()
        {
            data = new Transform[width, height];
        }
        // Checks if position is within border
        public bool InsideBorder(Vector2 pos)
        {
            // Trucate the vector
            int x = (int)pos.x;
            int y = (int)pos.y;

            // Is the position within the bounds of the grid?
            // Do not need to check height
            if (x >= 0 && x < width && y >= 0)
            {
                // Inside of the border
                return true;
            }

            // Outside of the border
            return false;
        }
        // Deletes a row with a given y coordinate
        public void DeleteRow(int y)
        {
            // Loop through the column using x - width 
            for (int x = 0; x < width; x++)
            {
                // Destroy each element 
                Destroy(data[x, y].gameObject);
                // Clear array index
                data[x, y] = null;
            }
        }
        // Shifts the row in the y coordinate down one space
        public void DecreaseRow(int y)
        {
            // Loop through entire column
            for(int x = 0; x < width; x++)
            {
                // Check if index is not null
                if(data[x, y] != null)
                {
                    // Move one towards bottom
                    data[x, y - 1] = data[x, y];
                    // Set grid element to one above
                    data[x, y] = null;
                    // Update block position
                    data[x, y - 1].position += Vector3.down;
                }
                
            }

        }

        // Shifts rows above from given y
        public void DecreaseRowsAbove(int y)
        {
            // Loop each row starting from y
            for (int i = y; i < height; i++)
            {
                // Decrease each row
                DecreaseRow(i);
            }
        }

        // Shifts rows above from given y
        public bool IsRowFull(int y)
        {
            // Loop through each column
            for(int x = 0; x < width; x++)
            {
                // If cell is empty
                if(data[x, y] == null)
                {
                    return false;
                }
            }

            // full row exists
            return true;
        }
        // Delete the full rows
        public int DeleteFullRows()
        {
            int clearedRows = 0;
            // Loop through all rows
            for(int y = 0; y < height; y++)
            {
                // Is the row full
                if(IsRowFull(y))
                {
                    // Add row to count
                    clearedRows++;
                    // Delete entire row
                    DeleteRow(y);
                    // Decrease rows from above
                    DecreaseRowsAbove(y + 1);
                    // Decrease current y coordinate so we don't skip the next row
                    y--;
                }
            }
            // If there are rows that were cleared
            if(clearedRows > 0)
            {
                // Tell GameManager how many rows were cleared
            }

            return clearedRows;
        }
        // Rounds Vector2 to the nearest whole number
        public Vector2 RoundVec2(Vector2 v)
        {
            float roundX = Mathf.Round(v.x);
            float roundY = Mathf.Round(v.y);
            return new Vector2(roundX, roundY);
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}