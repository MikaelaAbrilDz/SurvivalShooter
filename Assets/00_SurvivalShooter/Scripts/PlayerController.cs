using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask movableMask;
    [SerializeField] LayerMask enemyMask;

    [SerializeField] GameData gameData;

    EnemyBehaviour selectedEnemy;

    bool isInAttackRange;

    [HideInInspector] public NavMeshAgent agent;
    PlayerMouseInput mInput;
    
    InputAction mMainClickAction;
    InputAction[] mSwitchWeaponActions;
    InputAction m_saveAction;
    InputAction m_deleteAction;
    
    [SerializeField] GameObject[] weapons = new GameObject[3];
    IWeapon currentWeapon;
   
    Vector3 mousePos;

    float timer;
    void Awake()
    {
        currentWeapon = weapons[0].GetComponent<IWeapon>();
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
        m_saveAction = mInput.Player.SaveQuit;
        m_deleteAction = mInput.Player.Reset;

        if (SaveSystem.Load(gameData, gameObject))
        {
            agent.Warp(gameData.playerPosition);
            for (int i = 0; i < gameData.enemyPositions.Length; i++)
            {
                FindAnyObjectByType<EnemySpawner>().RestoreEnemy(gameData.enemyPositions[i], gameData.enemyHealths[i]);
            }
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        print(timer + " vs " + currentWeapon.GetCooldown());
        if (isInAttackRange && timer >= currentWeapon.GetCooldown())
        {
            timer = 0;
            Shoot();
        }

        mousePos = Mouse.current.position.value;
        if (mMainClickAction.WasPressedThisFrame()) MoveTo();

        if (selectedEnemy != null)
        {
            if ((selectedEnemy.transform.position - transform.position).magnitude <= currentWeapon.GetRange())
            {
                if (!isInAttackRange) timer = currentWeapon.GetCooldown();
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

        for (int i = 0; i < mSwitchWeaponActions.Length; i++)
        {
            if (mSwitchWeaponActions[i].WasPressedThisFrame())
            {
                foreach (GameObject weapon in weapons)
                {
                    weapon.SetActive(false);
                }
                weapons[i].SetActive(true);
                currentWeapon = weapons[i].GetComponent<IWeapon>();
            }
        }

        if (m_saveAction.WasPressedThisFrame()) SaveAndQuit();
        if (m_deleteAction.WasPressedThisFrame()) ResetGame();
    }
    void SaveAndQuit()
    {
        gameData.playerPosition = transform.position;

        EnemyBehaviour[] enemies = FindObjectsByType<EnemyBehaviour>();
        gameData.enemyPositions = new Vector3[enemies.Length];
        gameData.enemyHealths = new int[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            gameData.enemyPositions[i] = enemies[i].transform.position;
            gameData.enemyHealths[i] = enemies[i].life;
        }

        SaveSystem.Save(gameData);
        Application.Quit();
    }
    void ResetGame()
    {
        SaveSystem.DeleteSave();
        gameData.Reset();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
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
        Gizmos.DrawWireSphere(transform.position, currentWeapon.GetRange());
        Gizmos.color = Color.blue;
        if (agent != null) Gizmos.DrawSphere(agent.destination, 0.5f);
        Gizmos.color = Color.red;
        if (selectedEnemy != null && selectedEnemy.GetComponentInChildren<MeshFilter>() != null) Gizmos.DrawWireMesh(selectedEnemy.GetComponentInChildren<MeshFilter>().mesh, 0, selectedEnemy.transform.position + Vector3.up);
    }
}
