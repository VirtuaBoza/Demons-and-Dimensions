using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    private float playerSpeed;
    private Animator animator;
    private Vector3 target;
    private Vector3 lastTarget;
    private bool isMovingToTarget = true;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        target = transform.position;
        lastTarget = target;

        Dictionary<PlayerCharacter, Character> characterDictionary = FindObjectOfType<CharacterDatabase>().CharacterDictionary;
        playerSpeed = characterDictionary[gameManager.currentCharacter].Speed / 5;
    }

    void Update()
    {
        CheckForCharacterSwapCommand();
        MovePlayer();
    }

    private void CheckForCharacterSwapCommand()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapCharacterTo(PlayerCharacter.Crystal);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapCharacterTo(PlayerCharacter.Teddy);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapCharacterTo(PlayerCharacter.Hunter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwapCharacterTo(PlayerCharacter.Damien);
        }
        else if (Input.GetButtonDown("CycleUp"))
        {
            int currentPlayerIndex = (int)gameManager.currentCharacter;
            if (currentPlayerIndex == 3)
            {
                SwapCharacterTo(PlayerCharacter.Crystal);
            }
            else
            {
                SwapCharacterTo((PlayerCharacter)currentPlayerIndex + 1);
            }
        }
        else if (Input.GetButtonDown("CycleDown"))
        {
            int currentPlayerIndex = (int)gameManager.currentCharacter;
            if (currentPlayerIndex == 0)
            {
                SwapCharacterTo(PlayerCharacter.Damien);
            }
            else
            {
                SwapCharacterTo((PlayerCharacter)currentPlayerIndex - 1);
            }
        }
    }

    private void SwapCharacterTo(PlayerCharacter playerCharacter)
    {
        gameManager.currentCharacter = playerCharacter;

        Animator animator = GetComponent<Animator>();
        switch (playerCharacter)
        {
            case PlayerCharacter.Crystal:
                animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("AnimatorControllers/CrystalController");
                break;
            case PlayerCharacter.Teddy:
                animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("AnimatorControllers/TeddyController");
                break;
            case PlayerCharacter.Hunter:
                animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("AnimatorControllers/HunterController");
                break;
            case PlayerCharacter.Damien:
                animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("AnimatorControllers/DamienController");
                break;
        }
    }

    private void MovePlayer()
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
