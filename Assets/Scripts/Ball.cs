using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private bool isStuckToPlayer;
    private Transform playerBallPosition;
    private Player playerScript;
    private Vector2 previousLocation;
    private float speed;

    public bool IsStuckToPlayer { get => isStuckToPlayer; set => isStuckToPlayer = value; }


    private void Start()
    {
        playerScript = playerTransform.GetComponent<Player>();
        playerBallPosition = playerTransform.Find("BallPosition");
    }

    private void Update()
    {
        if (!isStuckToPlayer)
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
            if (distanceToPlayer < 0.5f)
            {
                isStuckToPlayer = true;
                playerScript.AttachedBall = this;
            }
        }
        else
        {
            Vector2 currentLocation = new Vector2(transform.position.x, transform.position.z);
            speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
            transform.position = playerBallPosition.position;
            transform.Rotate(playerTransform.right, speed, Space.World);
            previousLocation = currentLocation;
        }

        // Respawn da bola se cair
        if (transform.position.y < -2f)
        {
            transform.position = new Vector3(-0.178f, 1f, -0.054f);
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
