using System.Collections.Generic;
using MUtility;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public int CurrentTarget { get; private set; }
    private Rigidbody2D rb;
    private List<Transform> path;

    public void Init(List<Transform> path)
    {
        this.path = path;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (CurrentTarget >= path.Count)
        {
            Destroy(gameObject);
            return;
        }

        rb.velocity = (path[CurrentTarget].position - transform.position).normalized * moveSpeed;
        if ((transform.position - path[CurrentTarget].position).sqrMagnitude < 0.002f) CurrentTarget++;
    }
}
