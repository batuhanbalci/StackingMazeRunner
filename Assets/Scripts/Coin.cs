using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool isCollected;
    public bool isTarget;

    [SerializeField] private GameObject character;
    private GameManager gameManager;
    private AudioSource audioCollect;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioCollect = GameObject.Find("AudioCollect").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (isCollected)
        {
            //gameObject.GetComponent<Rigidbody>().velocity = character.GetComponent<Rigidbody>().velocity;
        }
        else if (isTarget)
        {
            if (gameManager.GetStackCount() == 0)
            {
                if (Vector3.Distance(transform.position, character.transform.position) < 1f)          
                {
                    isCollected = true;
                    gameManager.collectedCoins.Add(this);
                    audioCollect.Play();
                    isTarget = false;
                }
            }

            else
            {
                if (transform.position != gameManager.GetBottomPosition() && Vector3.Distance(transform.position, gameManager.GetBottomPosition()) < 1f)
                {
                    isCollected = true;
                    gameManager.collectedCoins.Add(this);
                    audioCollect.Play();
                    isTarget = false;
                }
            }
        }
    }

}

