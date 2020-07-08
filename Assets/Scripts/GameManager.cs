using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager singleton;

    private Ground[] grounds;
    private CharacterController characterController;
    public List<Coin> collectedCoins;


    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        grounds = FindObjectsOfType<Ground>();
        characterController = FindObjectOfType<CharacterController>();
    }

    public int GetStackCount()
    {
        int counter = 0;
        if (collectedCoins != null)
        {
            foreach (var coin in collectedCoins)
            {
                if (coin.isCollected == true)
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    public int GetStackIndex()
    {
        int counter = 0;
        if (collectedCoins != null)
        {
            foreach (var coin in collectedCoins)
            {
                if (coin.isCollected == true)
                {
                    return counter;
                }
            }
        }
        return counter;
    }

    public Vector3 GetBottomPosition()
    {
        return collectedCoins.Count == 0 ? Vector3.zero : collectedCoins[0].transform.position;
    }
}
