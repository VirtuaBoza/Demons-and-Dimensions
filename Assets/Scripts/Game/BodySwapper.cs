using UnityEngine;

public class BodySwapper : MonoBehaviour
{
    private GameManager gameManager;
    private CharacterDatabase characterDatabase;
    private Animator animator;
    private Animator playerAnimator;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        characterDatabase = FindObjectOfType<CharacterDatabase>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckForCharacterSwapCommand();
    }

    private void CheckForCharacterSwapCommand()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapCharacterTo(PlayerCharacterName.Crystal);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapCharacterTo(PlayerCharacterName.Teddy);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapCharacterTo(PlayerCharacterName.Hunter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwapCharacterTo(PlayerCharacterName.Damien);
        }
        else if (Input.GetButtonDown("CycleUp"))
        {
            int currentPlayerIndex = (int)gameManager.currentCharacter;
            if (currentPlayerIndex == 3)
            {
                SwapCharacterTo(PlayerCharacterName.Crystal);
            }
            else
            {
                SwapCharacterTo((PlayerCharacterName)currentPlayerIndex + 1);
            }
        }
        else if (Input.GetButtonDown("CycleDown"))
        {
            int currentPlayerIndex = (int)gameManager.currentCharacter;
            if (currentPlayerIndex == 0)
            {
                SwapCharacterTo(PlayerCharacterName.Damien);
            }
            else
            {
                SwapCharacterTo((PlayerCharacterName)currentPlayerIndex - 1);
            }
        }
    }

    private void SwapCharacterTo(PlayerCharacterName playerCharacter)
    {
        gameManager.currentCharacter = playerCharacter;
        animator.runtimeAnimatorController = AnimatorReplicator.CreateAnimatorFromPlayerAnimator(characterDatabase.CharacterDictionary[playerCharacter].AnimClipDictionary, playerAnimator);
    }

    public void SetCurrentCharacterAnimator(Animator playerAnimator)
    {
        this.playerAnimator = playerAnimator;
        GetComponent<Animator>().runtimeAnimatorController = AnimatorReplicator.CreateAnimatorFromPlayerAnimator(characterDatabase.CharacterDictionary[gameManager.currentCharacter].AnimClipDictionary, playerAnimator);
    }
}
