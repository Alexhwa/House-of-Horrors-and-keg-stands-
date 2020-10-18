using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 moveVect;
    private Rigidbody2D rb;

    [Header("Attacking")]
    public KeyCode attackKey;
    public GameObject attackHitbox;
    private Transform attackPos;
    public float attackCooldown;

    private Inventory inventory;

    private enum PlayerState
    {
        Idle, Moving, Attacking
    }
    private PlayerState state;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BodyPart" && state != PlayerState.Attacking)
        {
            inventory.AddPart(collision.gameObject.GetComponent<SpriteRenderer>().sprite);
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }

    // Update is called once per frame
    void Update()
    {
        if (state != PlayerState.Attacking)
        {
            DoMovement();
            DoAttack();
        }
    }
    private void DoAttack()
    {
        if (Input.GetKeyDown(attackKey) && gameObject.GetComponentInChildren<Attack>() == null)
        {
            var hitbox = Instantiate(attackHitbox, transform);
            hitbox.transform.localPosition = attackPos.localPosition;
            state = PlayerState.Attacking;
            StartCoroutine(ResetState(attackCooldown, PlayerState.Idle));
        }
    }
    private void DoMovement()
    {
        moveVect = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);
        rb.velocity = moveVect;

        if (moveVect.x > 0 && dir == MoveDir.Left)
        {
            var newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            dir = MoveDir.Right;
        }
        else if (moveVect.x < 0 && dir == MoveDir.Right)
        {
            var newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            dir = MoveDir.Left;
        }
        if(moveVect != Vector2.zero)
        {
            state = PlayerState.Moving;
        }
    }

    IEnumerator ResetState(float delay, PlayerState newState)
    {
        yield return new WaitForSeconds(delay);
        state = newState;
    }
}

