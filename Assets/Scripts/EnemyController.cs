using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IFlashable
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float blindTime;

    [SerializeField] private Transform targetTransform;

    [SerializeField] private GameObject jumpscareScreen;

    private NavMeshAgent agent;

    private bool isBlind = false;
    private bool canChase = false;

    public bool CanChase => canChase;

    public event Action IsMoving;

    private IEnumerator Blind()
    {
        isBlind = true;
        agent.speed = 0f;

        yield return new WaitForSeconds(blindTime);

        isBlind = false;
        agent.speed = walkSpeed;
    }

    private IEnumerator Jumpscare()
    {
        jumpscareScreen.SetActive(true);

        yield return new WaitForSeconds(1);

        LoadingManager.Manager.StartLoading("ForestNight");
    }

    public void Flash()
    {
        if (!isBlind)
        {
            StartCoroutine(Blind());
        }
    }

    private void OnObjectiveCompleted(string name)
    {
        if (name != "exp4") return;

        canChase = true;
        IsMoving?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Jumpscare());
        }
    }

    private void OnEnable()
    {
        ObjectiveManager.OnObjectiveDeleted += OnObjectiveCompleted;
    }

    private void OnDisable()
    {
        ObjectiveManager.OnObjectiveDeleted -= OnObjectiveCompleted;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
    }

    private void FixedUpdate()
    {
        if (canChase && !isBlind)
        {
            float distance = Vector3.Distance(transform.position, targetTransform.position);

            if (distance > 15)
            {
                agent.speed = runSpeed;
            }
            else
            {
                agent.speed = walkSpeed;
            }

            agent.SetDestination(targetTransform.position);
        }
    }
}
