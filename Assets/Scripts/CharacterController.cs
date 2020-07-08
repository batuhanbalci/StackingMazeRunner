using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float velocity = 3f;

    private bool isMoving;
    private Vector3 travelDirection;
    private Vector3 nextCollisionPosition;
    SwipeDetector.SwipeDirection direction;

    private GameManager gameManager;
    private LevelManager levelManager;

    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void FixedUpdate()
    {
        if (nextCollisionPosition != Vector3.zero)
        {
            if (gameManager.GetStackCount() > 0)
            {
                if (Vector3.Distance(gameManager.GetBottomPosition(), nextCollisionPosition) < 0.6f)
                {
                    StopMovement();
                }
            }
            else if (Vector3.Distance(transform.position, nextCollisionPosition) < 0.6f)
            {
                StopMovement();
            }
        }

        if (isMoving)
        {
            StartMovement();
            return;
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }

        switch (direction)
        {
            case SwipeDetector.SwipeDirection.Up:
                SetDestination(Vector3.forward);
                break;
            case SwipeDetector.SwipeDirection.Down:
                SetDestination(Vector3.back);
                break;
            case SwipeDetector.SwipeDirection.Left:
                SetDestination(Vector3.left);
                break;
            case SwipeDetector.SwipeDirection.Right:
                SetDestination(Vector3.right);
                break;
            default:
                SetDestination(Vector3.zero);
                break;
        }
    }

    private void SwipeDetector_OnSwipe(SwipeDetector.SwipeData data)
    {
        direction = data.Direction;
    }

    private void SetDestination(Vector3 direction)
    {
        travelDirection = direction;
        Vector3 tempPosition = transform.position;

        if (gameManager.GetStackCount() > 0)
        {
            if (gameManager.GetBottomPosition() != Vector3.zero)
            {
                tempPosition = gameManager.GetBottomPosition();
            }
        }

        RaycastHit hit;
        int loopCount = 0;
        if (Physics.Raycast(tempPosition, direction, out hit, 100f))
        {
            Debug.DrawLine(tempPosition, hit.point, Color.red, 50f);
            while (hit.collider != null && hit.collider.gameObject.tag == "Coin")
            {
                if (loopCount++ > 100)
                {
                    break;
                }
                hit.collider.gameObject.GetComponent<Coin>().isTarget = true;
                tempPosition = hit.point;
                Physics.Raycast(tempPosition + direction, direction, out hit, 100f);
                Debug.DrawLine(tempPosition, hit.point, Color.cyan, 50f);
            }
            nextCollisionPosition = hit.point;
            
        }

        isMoving = true;
    }

    private void StartMovement()
    {
        rigidbody.velocity = travelDirection * velocity;

        Vector3 vec = transform.position;

        if (gameManager.collectedCoins.Count == 0)
        {
            vec.y = 0.33f + levelManager.GetStartPosition().y;
            transform.position = vec;
        }

        for (int i = 0; i < gameManager.collectedCoins.Count; i++)
        {
            vec.y = i * 0.33f + 1f + levelManager.GetStartPosition().y;
            transform.position = vec;
            gameManager.collectedCoins[i].transform.position = new Vector3(transform.position.x, i * 0.33f + levelManager.GetStartPosition().y + 0.3f, transform.position.z);
        }
    }

    public void StartNewLevel(Vector3 position)
    {
        StopMovement();
        rigidbody.velocity = Vector3.zero;
        Vector3 vec = position;

        if (gameManager.collectedCoins.Count == 0)
        {
            vec.y = levelManager.GetStartPosition().y;
            transform.position = vec;
        }

        for (int i = 0; i < gameManager.collectedCoins.Count; i++)
        {
            vec.y = i * 0.3f + 1f + levelManager.GetStartPosition().y;
            transform.position = vec;
            gameManager.collectedCoins[i].transform.position = new Vector3(position.x, position.y + i * 0.33f, position.z);
        }
    }

    private void StopMovement()
    {
        isMoving = false;
        travelDirection = Vector3.zero;
        nextCollisionPosition = Vector3.zero;
    }
}
