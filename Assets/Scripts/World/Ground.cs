using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour, IPointerClickHandler
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        PolygonCollider2D[] colliders = GetComponentsInChildren<PolygonCollider2D>();
        foreach (PolygonCollider2D collider in colliders)
        {
            collider.isTrigger = true;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = player.transform.position.z;
        player.MovePlayer(target);
    }
}
