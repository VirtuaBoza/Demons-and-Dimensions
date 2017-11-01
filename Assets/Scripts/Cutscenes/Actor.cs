using UnityEngine;

public class Actor : MonoBehaviour
{
    public float playerSpeed = 3f;

    private Animator animator;
    private Vector3 target, lastTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = transform.position;
        lastTarget = target;
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);

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

    // Called by Ground.cs when the ground is clicked
    public void MovePlayer(Vector3 target)
    {
        this.target = target;
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