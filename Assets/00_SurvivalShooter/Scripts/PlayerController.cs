using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask movableMask;
    [SerializeField] LayerMask enemyMask;

    [SerializeField] PlayerBullet bullet;

    EnemyBehaviour selectedEnemy;

    float attackRange = 9;
    float attackCoolDown = 0.6f;
    bool isInAttackRange;

    NavMeshAgent agent;
    PlayerMouseInput mInput;
    InputAction mMainClickAction;

    Vector3 mousePos;

    float timer;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mInput = new PlayerMouseInput();
        mInput.Player.Enable();

        mMainClickAction = mInput.Player.MainClick;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (isInAttackRange && timer >= attackCoolDown)
        {
            timer = 0;
            Shoot(selectedEnemy.transform.position);
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
    void Shoot(Vector3 pos)
    {
        LTDescr tween = LeanTween.move(bullet.gameObject, pos + Vector3.up, attackCoolDown/5);
        bullet.Shoot(tween, 25, transform.position + Vector3.up);
    }
}
