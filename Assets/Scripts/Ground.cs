using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name == "Level0Finish" || gameObject.name == "Level1Finish")
        {
            if (collision.gameObject.name == "Character" || collision.gameObject.tag == "Coin")
            {
                levelManager.LevelUp();
            }        
        }
    }
}
