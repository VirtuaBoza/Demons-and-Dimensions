using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private float playerSpeed;
    private Animator animator;
    private Vector3 target;
    private Vector3 lastTarget;
    private bool isMovingToTarget = true;
    private GameManager gameManager;
    private CharacterDatabase characterDatabase;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        target = transform.position;
        lastTarget = target;

        characterDatabase = FindObjectOfType<CharacterDatabase>();
        playerSpeed = characterDatabase.CharacterDictionary[gameManager.currentCharacter].Speed / 5;
        GetComponentInChildren<BodySwapper>().SetCurrentCharacterAnimator(animator);
    }

    void Update()
    {
        MovePlayer();
        // TEST TEST TEST
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (Animator anim in GetComponentsInChildren<Animator>())
        {
            if (anim.GetComponent<Equipper>() == null || anim.GetComponent<Equipper>().hasEquippedItem)
            {
                anim.SetTrigger("slash");
            }
        }
        }
    }

    public void UpdatePlayerEquipment()
    {
        ICollection<EquipType> equippedItems = characterDatabase.CharacterDictionary[gameManager.currentCharacter].EquippedItems.Keys;
        foreach (Equipper equipper in GetComponentsInChildren<Equipper>())
        {
            if (equippedItems.Contains(equipper.equipType))
            {
                equipper.CreateEquipmentAnimatorFromPlayerAnimator(
                    characterDatabase.CharacterDictionary[gameManager.currentCharacter].EquippedItems[equipper.equipType], animator);
            }
            else
            {
                equipper.ClearAnimator();
            }
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
        foreach (Animator anim in GetComponentsInChildren<Animator>())
        {
            if (anim.GetComponent<Equipper>() == null || anim.GetComponent<Equipper>().hasEquippedItem)
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    anim.SetFloat("speedX", x);
                    anim.SetFloat("speedY", 0f);
                }
                else
                {
                    anim.SetFloat("speedY", y);
                    anim.SetFloat("speedX", 0f);
                }
            }
        }
    }

    void ResetAnimation()
    {
        foreach (Animator anim in GetComponentsInChildren<Animator>())
        {
            if (anim.GetComponent<Equipper>() == null || anim.GetComponent<Equipper>().hasEquippedItem)
            {
                anim.SetFloat("speedX", 0f);
                anim.SetFloat("speedY", 0f);
            }
        }
    }
}
