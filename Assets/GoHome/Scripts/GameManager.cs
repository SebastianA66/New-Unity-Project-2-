using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;

namespace GoHome
{
    [Serializable]
    public class GameData
    {
        public int score;
    }

    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance = null;
        private void Awake()
        {
            Instance = this;
            // dataPath = "C:\Users\"User"\Documents\Project\Assets\GoHome\Data\GameScene.XML"
            fullPath = Application.dataPath + "/GoHome/Data/" + fileName + ".xml";
            // Check if file exists
            if(File.Exists(fullPath))
            {
                // Load the file and contents
                Load();
            }
            
        }
        private void OnDestroy()
        {
            Instance = null;
            // Save the data on Destroy
            Save();
        }
        #endregion

        public int currentLevel = 0;
        public int currentScore = 0;
        public bool isGameRunning = true;
        public Transform levelContainer;
        [Header("UI")]
        public Text scoreText;
        [Header("Game Saves")]
        public string fileName = "GameData";


        private Level[] levels;
        private string fullPath;
        private GameData data = new GameData();

        
        private void Save()
        {
            // Set data's score to current
            data.score = currentScore;
            // Create a serializer of GameData
            var serializer = new XmlSerializer(typeof(GameData));
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        private void Load()
        {
            var serializer = new XmlSerializer(typeof(GameData));
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                data = serializer.Deserialize(stream) as GameData;
            }
        }


        // Disable all levels except the levelIndex
        private void SetLevel(int levelIndex)
        {
            // Loop through all levels
            for (int i = 0; i < levels.Length; i++)
            {
                // Get Level GameObject
                GameObject level = levels[i].gameObject;
                level.SetActive(false); // Disable level
                // Is current index (i) the same as levelIndex?
                if(i == levelIndex)
                {
                    // Enable that level instead
                    level.SetActive(true);
                }
            }
        }

        private void Start()
        {
            // Populate levels array with levels in game
            levels = levelContainer.GetComponentsInChildren<Level>(true);
            SetLevel(currentLevel);
        }


        public void GameOver()
        {
            // Stop game from running
            isGameRunning = false;
        }

        public void AddScore(int scoreToAdd)
        {
            currentScore += scoreToAdd;
            scoreText.text = "Score: " + currentScore;
        }
        
        public void AddScore(int scoreToAdd, int modifier)
        {
            AddScore(scoreToAdd * modifier);
        }
        // Call this function to move to the next level
        public void NextLevel()
        {
            // Increase currentLevel
            currentLevel++;
            // If currentLevel excedes level length
            if (currentLevel >= levels.Length)
            {
                // GameOver!
                GameOver();
            }
            // else
            else
            {
                // Update current level
                SetLevel(currentLevel);
            }
            


        }
        public void Restart()
        {
            // Get current scene
            Scene currentScene = SceneManager.GetActiveScene();
            // Reload current scene
            SceneManager.LoadScene(currentScene.name);
        }

    }

}
