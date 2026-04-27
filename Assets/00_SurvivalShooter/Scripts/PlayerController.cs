using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask movableMask;
    [SerializeField] LayerMask enemyMask;

    EnemyBehaviour selectedEnemy;

    float attackRange = 9;
    float attackCoolDown = 0.6f;
    bool isInAttackRange;

    NavMeshAgent agent;
    PlayerMouseInput mInput;
    
    InputAction mMainClickAction;
    InputAction[] mSwitchWeaponActions;
    
    [SerializeField] GameObject[] weapons = new GameObject[3];
    IWeapon currentWeapon;
    

    Vector3 mousePos;

    float timer;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mInput = new PlayerMouseInput();
        mInput.Player.Enable();

        mMainClickAction = mInput.Player.MainClick;
        mSwitchWeaponActions = new InputAction[3]
        {
            mInput.Player.First,
            mInput.Player.Second,
            mInput.Player.Third,
        };
    }
    void Update()
    {
        currentWeapon = weapons[0].GetComponent<IWeapon>();
        timer += Time.deltaTime;
        if (isInAttackRange && timer >= attackCoolDown)
        {
            timer = 0;
            Shoot();
        }

        mousePos = Mouse.current.position.value;
        if (mMainClickAction.WasPressedThisFrame()) MoveTo();

        if (selectedEnemy != null)
        {
            if ((selectedEnemy.transform.position - transform.position).magnitude <= attackRange)
            {
                if (!isInAttackRange) timer = attackCoolDown;
                isInAttackRange = true;
                agent.SetDestination(transform.position);
            }
            else
            {
                isInAttackRange = false;
                agent.SetDestination(selectedEnemy.transform.position);
            }
        }
        else isInAttackRange = false;
    }

    void MoveTo()
    {
        Ray rCast = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(rCast, out hit, Mathf.Infinity, enemyMask))
        {
            selectedEnemy = hit.collider.gameObject.GetComponent<EnemyBehaviour>();
        }
        else
        {
            selectedEnemy = null;
            isInAttackRange = false;
        }
        if (Physics.Raycast(rCast,out hit, Mathf.Infinity, movableMask) && !isInAttackRange)
        {
            NavMeshHit navHit;
            NavMesh.SamplePosition(hit.point, out navHit, 5, NavMesh.AllAreas);
            agent.SetDestination(navHit.position);
        }
    }
    void Shoot()
    {
        currentWeapon.Shoot(transform, selectedEnemy);
    }

    private void OnDrawGizmos()
    {
        if (isInAttackRange) Gizmos.color = Color.red; else Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        if (agent != null) Gizmos.DrawSphere(agent.destination, 0.5f);
        Gizmos.color = Color.red;
        if (selectedEnemy != null && selectedEnemy.GetComponentInChildren<MeshFilter>() != null) Gizmos.DrawWireMesh(selectedEnemy.GetComponentInChildren<MeshFilter>().mesh, 0, selectedEnemy.transform.position + Vector3.up);
    }
}
