using UnityEngine;

public class Equipper : MonoBehaviour
{
    public Animator animator;
    public EquipType equipType;
    public bool hasEquippedItem;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ClearAnimator()
    {
        hasEquippedItem = false;
        GetComponent<Animator>().runtimeAnimatorController = new RuntimeAnimatorController();
    }

    public void CreateEquipmentAnimatorFromPlayerAnimator(IEquipable equipment, Animator playerAnimator)
    {
        GetComponent<Animator>().runtimeAnimatorController = AnimatorReplicator.CreateAnimatorFromPlayerAnimator(equipment.AnimClipDictionary, playerAnimator);
        hasEquippedItem = true;
    }
}
