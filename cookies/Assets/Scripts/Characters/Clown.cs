using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clown : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public float maxDistance;
    public GameObject player;
    public GameObject hammer;
    public GameObject clown_key;
    public GameObject clown_door;
    public GameObject final_circus_thread_trigger;

    private Transform _target;
    private NavMeshAgent _agent;
    private float _timer;
    private float distance;
    private Animator _animator;
    private Vector3 newPos;
    private bool wandering = true;
    private bool _hasGivenKey;
    private OpenableInteractable _clown_openable;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _timer = wanderTimer;
        _animator = GetComponent<Animator>();
        _clown_openable = clown_door.GetComponent<OpenableInteractable>();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);


        _timer += Time.deltaTime;

        if (wandering)
        {
            if (_timer >= wanderTimer)
            {
                newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                _agent.SetDestination(newPos);
                _timer = 0;
            }
        }

        if (distance < maxDistance)
        {
            wandering = false;
            Vector3 towardsPlayer = transform.position - player.transform.position;
            Vector3 newPosition = transform.position - towardsPlayer;
            _agent.speed = 0.6f;
            _agent.SetDestination(newPosition);
        }
        else if (distance > maxDistance)
        {
            wandering = true;
            _agent.speed = 0.6f;
        }

        if (distance < 0.6f)
        {
            hammer.SetActive(true);
            _agent.isStopped = true;
            _animator.SetBool("swinging", true);
        }

        if (_animator.enabled == false)
        {
            if (!_hasGivenKey)
            {
                hammer.transform.parent = null;
                hammer.GetComponent<Rigidbody>().isKinematic = false;

                _clown_openable.reqType = Interactable.RequirementType.Single;
                _clown_openable.requiredTags = new string[1];
                _clown_openable.requiredTags[0] = "Clown Key";
                _clown_openable.isLocked = false;
                _clown_openable.isOpened = false;
                _clown_openable.closeToggle = false;

                final_circus_thread_trigger.SetActive(true);

                StartCoroutine(GivePlayerKeys(1.5f));
            }
            _hasGivenKey = true;
        }
    }

    public void ResetSwing()
    {
        _agent.isStopped = false;
        _animator.SetBool("swinging", false);
        hammer.SetActive(false);
        hammer.GetComponent<BoxCollider>().enabled = false;
        
    }

    public void EnableHammerHitbox()
    {
        hammer.GetComponent<BoxCollider>().enabled = true;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private IEnumerator GivePlayerKeys(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        clown_key.transform.parent = null;
        clown_key.GetComponent<Interactable>().InteractAction(); // give players the key if custodian is killed
        clown_key.tag = "Interactable";
    }
}
