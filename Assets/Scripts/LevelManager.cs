using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel { get; private set; } = 1;

    private GameManager gameManager;
    private CharacterController characterController;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        characterController = FindObjectOfType<CharacterController>();
    }

    public void LevelUp()
    {
        if (gameManager.collectedCoins.Count >= 3) //TO DO
        {
            currentLevel += 1;
            for (int i = 2; i >= 0; i--)
            {
                gameManager.collectedCoins[i].isCollected = false;
                gameManager.collectedCoins.RemoveAt(i);
            }
           
            characterController.StartNewLevel(GetStartPosition());
        }
    }

    public Vector3 GetStartPosition()
    {
        switch (currentLevel)
        {
            case 1:
                return new Vector3(2f, 0.3f, 1f);
            case 2:
                return new Vector3(-4f, 3.3f, 11f);
            case 3:
                return new Vector3(-4f, 6.5f, 31f);
            default:
                return Vector3.zero;
        }
        
    }
}
