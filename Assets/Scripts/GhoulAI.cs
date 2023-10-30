using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class HumanoidAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private AgentLinkMover linkMover;

    [SerializeField]
    private GameObject target;

    public Animator animator;
    private bool isRunning = false;
    private bool isIdle = false;
    private bool isDead = false;

    private float rotationFactorPerFrame = 5.0f;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        linkMover = GetComponent<AgentLinkMover>();

        linkMover.OnLinkStart += HandleLinkStart;
        linkMover.OnLinkEnd += HandleLinkEnd;
    }

    void UpdateRotation()
    {

        Quaternion currentRotation = transform.rotation;

        // LookRotation needs a specified forward and up vector to create the "targetRotation"
        // If we subtract target position from our position, we create a new vector pointing towards target (which we use to specify the "forward" direction).
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
    }

    void HandleLinkStart()
    {
        animator.SetTrigger("jumpTrigger");
    }

    void HandleLinkEnd()
    {
    }


    void UpdateAnimations()
    {
        Vector3 velocity = agent.velocity;

        if (velocity.magnitude > 0f && !isRunning)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            isRunning = true;
            isIdle = false;
        }

        if (velocity.magnitude == 0f && !isIdle)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
            isIdle = true;
            isRunning = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        InvokeRepeating("UpdateDestination", 0f, 0.1f);

    }

    private void Update()
    {
        UpdateRotation();
        UpdateAnimations();
    }    
    
    private void UpdateDestination()
    {
        if (agent.enabled)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    private void OnEnable()
    {
        agent.enabled = true;
    }

    private void OnDisable()
    {
        agent.enabled = false;
    }
}
