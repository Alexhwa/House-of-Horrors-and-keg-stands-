using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed;
    public float runAwaySpeed;
    public float minScareDist;

    private Rigidbody2D rb;
    private GameObject scaryObj;
    private GameObject scaredExclamPnt;

    public enum MoveDir
    {
        left = -1, right = 1
    }
    public enum MonsterState
    {
        Walking, Scared
    }
    public MoveDir moveDir;
    public MonsterState state = MonsterState.Walking;

    // Start is called before the first frame update
    void Start()
    {
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
}
