using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpPower = 5;
    [SerializeField]
    private float playerRotationJump;
    [SerializeField]
    private float playerRotationFall;
    [SerializeField]
    private float rotationTime = 2;
    [SerializeField]
    private AnimationCurve rotationCurve;

    private Rigidbody2D playerRigidboby;
    private Animator playerAnimator;
    private float playerRotationCurrent;
    private float timeSinceLastJump;
    private bool isAlive;
    private Vector3 startPosition;
    private Quaternion startAngle;
    internal event Action OnStopGame;
    private float currentJumpPower;


    private void Start()
    {
        playerRigidboby = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        startPosition = playerRigidboby.position;
        startAngle = playerRigidboby.transform.rotation;
        currentJumpPower = jumpPower;
        ResetGame();
        playerRigidboby.simulated = false;
        isAlive = false;
    }

    internal void OnCollisionEnter2D(Collision2D collision)
    {
        while (isAlive)
        {
            OnStopGame?.Invoke();
            playerAnimator.enabled = false;
            isAlive = false;
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            timeSinceLastJump += Time.deltaTime;
            playerRotationCurrent = Mathf.Lerp(playerRotationJump, playerRotationFall, rotationCurve.Evaluate(timeSinceLastJump / rotationTime));
            transform.rotation = Quaternion.Euler(0, 0, playerRotationCurrent);
        }
        else
        {
            currentJumpPower = 0;
        }
    }

    public void Jump()
    {
        playerRigidboby.velocity = Vector2.up * currentJumpPower;
        playerRotationCurrent = playerRotationJump;
        timeSinceLastJump = 0;
    }
    public void ResetGame()
    {
        isAlive = true;
        playerRigidboby.simulated = true;
        playerAnimator.enabled = true;
        playerRigidboby.position = startPosition;
        playerRigidboby.transform.rotation = startAngle;
        timeSinceLastJump = 0;
        playerRotationCurrent = transform.rotation.z;
        currentJumpPower = jumpPower;
    }
}

