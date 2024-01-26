using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 50.0f;
    private Animator animator;
    public ParticleSystem dustParticleSystem;

    private const string isRunningForward = "Running";
    private const string isRunningBackward = "Backwards";


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);

        if (dustParticleSystem == null)
        {
            Debug.LogError("Dust Particle System ist nicht zugewiesen!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);

        HandleMovementAndRotation(forwardPressed, backwardPressed, leftPressed, rightPressed);
        HandleAnimationAndParticles(forwardPressed, backwardPressed);
    }

    private void HandleMovementAndRotation(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed)
    {
        float movement = (forwardPressed ? 1 : backwardPressed ? -1 : 0) * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, movement);

        float rotation = (rightPressed ? 1 : leftPressed ? -1 : 0) * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    private void HandleAnimationAndParticles(bool forwardPressed, bool backwardPressed)
    {
        bool isMoving = forwardPressed || backwardPressed;
        Debug.Log(forwardPressed + " " + backwardPressed);
        animator.SetBool(isRunningForward, forwardPressed);
        animator.SetBool(isRunningBackward, backwardPressed);

        if (isMoving && !dustParticleSystem.isPlaying)
        {
            dustParticleSystem.Play();
        }
        else if (!isMoving && dustParticleSystem.isPlaying)
        {
            dustParticleSystem.Stop();
        }
    }
}
