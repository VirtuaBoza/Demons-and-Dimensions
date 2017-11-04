﻿using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public PlayerCharacter playerCharacter; // Set in Inspector

    private float playerSpeed;
    private Animator animator;
    private Vector3 target;
    private Vector3 lastTarget;
    private bool isMovingToTarget = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = transform.position;
        lastTarget = target;

        Dictionary<PlayerCharacter, Character> characterDictionary = FindObjectOfType<CharacterDatabase>().CharacterDictionary;
        playerSpeed = characterDictionary[playerCharacter].Speed / 5;
    }

    void Update()
    {
        // Moves PlayerCharacter with mouseclick if not receiving other movement input
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
            isMovingToTarget = true;

            // Resets animation
            if (transform.position == target || target != lastTarget)
            {
                ResetAnimation();
            }
            else
            {
                // Animates movement
                float xDiff = target.x - transform.position.x;
                float yDiff = target.y - transform.position.y;
                Animate(xDiff, yDiff);
            }

            lastTarget = target;
        }
        else
        {
            // Resets animation
            if (isMovingToTarget)
            {
                ResetAnimation();
                isMovingToTarget = false;
            }
            else
            {
                float moveX = Input.GetAxis("Horizontal");
                float moveY = Input.GetAxis("Vertical");
                float xAndY = Mathf.Sqrt(Mathf.Pow(moveX, 2) + Mathf.Pow(moveY, 2));
                transform.Translate(moveX * playerSpeed * Time.deltaTime / xAndY, moveY * playerSpeed * Time.deltaTime / xAndY, transform.position.z, Space.Self);
                Animate(moveX, moveY);

                target = transform.position;
            }
        }
    }

    // Called by Ground.cs when the ground is clicked
    public void MovePlayer(Vector3 here)
    {
        target = here;
    }

    void Animate(float x, float y)
    {
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            animator.SetFloat("speedX", x);
            animator.SetFloat("speedY", 0f);
        }
        else
        {
            animator.SetFloat("speedY", y);
            animator.SetFloat("speedX", 0f);
        }
    }

    void ResetAnimation()
    {
        animator.SetFloat("speedX", 0f);
        animator.SetFloat("speedY", 0f);
    }
}
