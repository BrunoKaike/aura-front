using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private Animator animator;
    private Ball attachedBall;
    private float shotTime;
    private const int ShootLayer = 1;
    private int myScore, otherScore;
    private AudioSource dribbleSound;
    private AudioSource shootSound;
    private AudioSource cheerSound;
    private float distanceSinceLastDribble;
    private float goalTextColorAlpha;
    private Rigidbody characterController;

    public Ball AttachedBall { get => attachedBall; set => attachedBall = value; }

    private void Start()
    {
        characterController = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        // dribbleSound = GameObject.Find("Sound/dribble").GetComponent<AudioSource>();
        // shootSound = GameObject.Find("Audio/shoot").GetComponent<AudioSource>();
        // cheerSound = GameObject.Find("Sound/cheer").GetComponent<AudioSource>();
    }

    private void Update()
    {
        float speed = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z).magnitude;

        if (Input.GetMouseButtonDown(0))
        {
            shotTime = Time.time;
            animator.Play("Shoot", ShootLayer, 0f);
            animator.SetLayerWeight(ShootLayer, 1f);
        }

        if (shotTime > 0f)
        {
            // Shoot ball
            if (attachedBall != null && Time.time - shotTime > 0.2f)
            {
                // shootSound.Play();
                attachedBall.IsStuckToPlayer = false;
                Rigidbody ballRigidbody = attachedBall.transform.gameObject.GetComponent<Rigidbody>();
                Vector3 shootDirection = transform.forward;
                shootDirection.y += 0.3f;
                ballRigidbody.AddForce(shootDirection * 2f, ForceMode.Impulse);
                attachedBall = null;
            }

            // Finish kicking animation
            if (Time.time - shotTime > 0.5f)
            {
                shotTime = 0f;
            }
        }
        else
        {
            animator.SetLayerWeight(ShootLayer, Mathf.Lerp(animator.GetLayerWeight(ShootLayer), 0f, Time.deltaTime * 10f));
        }

        /*if (goalTextColorAlpha > 0f)
        {
            goalTextColorAlpha -= Time.deltaTime;
            textGoal.alpha = goalTextColorAlpha;
            textGoal.fontSize = 200 - (goalTextColorAlpha * 120);
        }*/

        if (attachedBall != null)
        {
            distanceSinceLastDribble += speed * Time.deltaTime;
            if (distanceSinceLastDribble > 3f)
            {
                // dribbleSound.Play();
                distanceSinceLastDribble = 0f;
            }
        }
    }

    public void IncreaseMyScore()
    {
        myScore++;
        UpdateScore();
    }

    public void IncreaseOtherScore()
    {
        otherScore++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        // cheerSound.Play();
        scoreText.text = "Score: " + myScore + "-" + otherScore;
    }
}
