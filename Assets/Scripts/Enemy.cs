using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f, movementTime = 1f;

    private float currentMovementTime = 0;
    private bool goingRight = true;

    private SpriteRenderer _spr;

    // Start is called before the first frame update
    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        currentMovementTime += Time.deltaTime;
        if(currentMovementTime >= movementTime)
        {
            goingRight = !goingRight;
            currentMovementTime = movementTime - currentMovementTime;
        }

        transform.position += new Vector3(speed * Time.deltaTime * (goingRight ? 1 : -1), 0, 0);
        _spr.flipX = !goingRight;
    }
}
