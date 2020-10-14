using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveVect;
    private Rigidbody2D rb;

    public KeyCode attackKey;
    public GameObject attackHitbox;
    private Transform attackPos;

    private Inventory inventory;

    private enum MoveDir
    {
        Left, Right
    }
    private MoveDir dir = MoveDir.Left;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = FindObjectOfType<Inventory>();
        attackPos = transform.GetChild(1);
    }
    private void FixedUpdate()
    {
        rb.velocity = moveVect;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BodyPart")
        {
            inventory.AddPart(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveVect = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);

        if(moveVect.x > 0 && dir == MoveDir.Left)
        {
            var newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            dir = MoveDir.Right;
        }
        else if(moveVect.x < 0 && dir == MoveDir.Right)
        {
            var newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            dir = MoveDir.Left;
        }

        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (gameObject.GetComponentInChildren<Attack>() == null)
        {
            var hitbox = Instantiate(attackHitbox, transform);
            hitbox.transform.localPosition = attackPos.localPosition;
        }
    }
}
