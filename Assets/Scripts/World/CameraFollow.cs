using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float xMargin, yMargin;
    public Vector2 minXandY, maxXandY;

    void LateUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
        {
            if (transform.position.x - player.transform.position.x > 0)
            {
                targetX = player.transform.position.x + xMargin;
            }
            else if (transform.position.x - player.transform.position.x < 0)
            {
                targetX = player.transform.position.x - xMargin;
            }
        }

        if (CheckYMargin())
        {
            if (transform.position.y - player.transform.position.y > 0)
            {
                targetY = player.transform.position.y + yMargin;
            }
            else if (transform.position.y - player.transform.position.y < 0)
            {
                targetY = player.transform.position.y - yMargin;
            }
        }

        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) > xMargin;
    }

    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - player.transform.position.y) > yMargin;
    }
}
