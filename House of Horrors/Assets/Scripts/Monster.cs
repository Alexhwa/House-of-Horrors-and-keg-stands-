using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed;
    public float runAwaySpeed;
    public float minScareDist;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    private GameObject scaryObj;
    private GameObject scaredExclamPnt;

    public GameObject bodyPart;

    public Sprite[] bodyParts;

    public enum MoveDir
    {
        left = -1, right = 1
    }
    public enum MonsterState
    {
        Walking, Scared, Dead
    }
    public MoveDir moveDir;
    public MonsterState state = MonsterState.Walking;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        scaryObj = GameObject.FindGameObjectWithTag("Player");
        scaredExclamPnt = transform.GetChild(0).gameObject;
        scaredExclamPnt.SetActive(false);
    }
    private void Update()
    {
        if(Vector2.Distance(scaryObj.transform.position, transform.position) < minScareDist)
        {
            state = MonsterState.Scared;
            scaredExclamPnt.SetActive(true);
        }
        else
        {
            state = MonsterState.Walking;
            scaredExclamPnt.SetActive(false);
        }
        HandleFlip();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MonsterAI();
    }
    public void MonsterAI()
    {
        if (state == MonsterState.Walking)
        {
            rb.velocity = new Vector2(moveSpeed * (float)moveDir, 0);
        }
        else if(state == MonsterState.Scared)
        {
            rb.velocity = Vector3.Normalize(transform.position - scaryObj.transform.position) * runAwaySpeed;

        }
    }
    public void Die()
    {
        if (state != MonsterState.Dead)
        {
            state = MonsterState.Dead;
            var part = Instantiate(bodyPart, transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0), transform.rotation);
            part.GetComponent<SpriteRenderer>().sprite = bodyParts[Random.Range(0, bodyParts.Length - 1)];
            Vector3 rot = part.transform.eulerAngles;
            rot.z = Random.Range(0, 360);
            part.transform.eulerAngles = rot;
            Destroy(gameObject);
        }
    }

    private void HandleFlip()
    {
        if(rb.velocity.x < 0)
        {
            spriteRend.flipX = false;
        }
        else if (rb.velocity.x > 0)
        {
            spriteRend.flipX = true;
        }
    }
}
