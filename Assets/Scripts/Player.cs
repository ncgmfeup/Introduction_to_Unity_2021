using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 2f, jumpForce = 7f;
    [SerializeField] private LayerMask layerMask;

    // Box Collision Values
    [SerializeField] private float collisionOffsetPos = .75f;
    [SerializeField] private Vector2 collisionBoxSize = Vector2.one * .25f;

    // Jump Buffer
    private float jumpBufferCount = 0;
    [SerializeField] private float jumpBufferLength = .1f;

    private bool isJumping = false;

    private SpriteRenderer _spr;
    private Rigidbody2D _rb;
    private Animator _anim;

    // Start is called before the first frame update
    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            float axis = Input.GetAxis("Horizontal");
            transform.position += new Vector3(speed * Time.deltaTime * axis, 0, 0);

            if (axis != 0) _spr.flipX = axis < 0;
        }

        // Detect Ground Collisions
        if (isJumping && Physics2D.OverlapBox((Vector2)transform.position + Vector2.down * collisionOffsetPos, collisionBoxSize, 360, layerMask) && _rb.velocity.y <= 0) isJumping = false;

        // Jump Buffer
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        if (jumpBufferCount >= 0 && !isJumping)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }

        // Small Jumps
        if (isJumping && Input.GetButtonUp("Vertical") && _rb.velocity.y > 0) _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * .5f);

        _anim.SetBool("Walking", Input.GetButton("Horizontal"));
        _anim.SetBool("Jumping", isJumping);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube((Vector2)transform.position + Vector2.down * collisionOffsetPos, collisionBoxSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            CollectiblesAudioSource.instance.PickCollectible();
            CollectiblesUI.instance.PickCollectible();
            Destroy(collision.gameObject);
        }
    }
}
