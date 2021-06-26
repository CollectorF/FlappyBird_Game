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
    private float playerRotationCurrent;
    private float timeSinceLastJump;
    private bool isAlive;
    private Vector3 startPosition;
    private Quaternion startAngle;
    internal delegate void EventHandler();
    internal event EventHandler OnResetGame;


    private void Start()
    {
        playerRigidboby = GetComponent<Rigidbody2D>();
        startPosition = playerRigidboby.position;
        startAngle = playerRigidboby.transform.rotation;
        Reset();
    }

    internal void OnCollisionEnter2D(Collision2D collision)
    {
        OnResetGame?.Invoke();
    }

    private void Update()
    {
        timeSinceLastJump += Time.deltaTime;
        playerRotationCurrent = Mathf.Lerp(playerRotationJump, playerRotationFall, rotationCurve.Evaluate(timeSinceLastJump / rotationTime));
        transform.rotation = Quaternion.Euler(0, 0, playerRotationCurrent);
    }

    public void Jump()
    {
        playerRigidboby.velocity = Vector2.up * jumpPower;
        playerRotationCurrent = playerRotationJump;
        timeSinceLastJump = 0;
    }
    public void Reset()
    {
        playerRigidboby.position = startPosition;
        playerRigidboby.transform.rotation = startAngle;
        timeSinceLastJump = 0;
        playerRotationCurrent = transform.rotation.z;
        isAlive = true;
    }
}

