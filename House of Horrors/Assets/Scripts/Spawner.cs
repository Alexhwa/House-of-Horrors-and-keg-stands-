using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float YOffset;
    public GameObject[] monsters;
    public float frequency;
    [Range(0, 1)]
    public float variance;

    private enum SpawnState
    {
        Idle, Spawning, Disabled
    }
    private SpawnState state = SpawnState.Idle;

    public bool spawnsLeft;

    // Start is called before the first frame update
    void Start()
    {
        SpawnMonster();
    }
    private void Update()
    {
        if(state == SpawnState.Idle)
        {
            SpawnMonster();
        }
    }
    private void SpawnMonster()
    {
        var spawnedMonster = monsters[Random.Range(0, monsters.Length)];
        var monsterInst = Instantiate(spawnedMonster, 
            transform.position + new Vector3(0, Random.Range(-YOffset, YOffset), 0), 
            transform.rotation);
        var monsterScript = monsterInst.GetComponent<Monster>();

        state = SpawnState.Spawning;

        if (spawnsLeft)
        {
            monsterScript.moveDir = Monster.MoveDir.left;
        }
        else
        {
            monsterScript.moveDir = Monster.MoveDir.right;
        }

        StartCoroutine(ResetSpawnState(Random.Range(-1f, 1f) * variance + frequency));
    }
    private IEnumerator ResetSpawnState(float delay)
    {
        yield return new WaitForSeconds(delay);
        state = SpawnState.Idle;
    }
}
