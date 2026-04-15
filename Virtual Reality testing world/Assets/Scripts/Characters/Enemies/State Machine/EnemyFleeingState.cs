using Unity.VisualScripting;
using UnityEngine;

public class EnemyFleeingState : EnemyState
{
    public EnemyFleeingState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine) { }
    float maximumFleeTime = 3;
    Cooldown fleeCooldown;
    float targetHeight;
    Vector2 Direction;
    public override void EnterState() 
    {
        fish.GetComponent<Animator>().SetBool("Swimming", true);
        fleeCooldown = new Cooldown(maximumFleeTime);
        fleeCooldown.StartCooldown();
        fish.agent.enabled = true;
        fish.agent.speed = fish.moveSpeed * 2;
        Direction = Random.insideUnitCircle.normalized * 10;
        targetHeight = Random.Range(-2, 2);
        fish.agent.destination = new Vector3(fish.transform.position.x + Direction.x, 0, fish.transform.position.z + Direction.y);
        //Debug.Log(Direction);
    }
    public override void ExitState() 
    {
        fish.agent.speed = fish.moveSpeed;
        fish.GetComponent<Animator>().SetBool("Swimming", false);
    }
    public override void PhysicsUpdate() { }
    public override void Update() 
    {
        if (!fleeCooldown.isCoolingDown)
        {
            enemyStateMachine.ChangeState(fish.idleState);
        }
        fish.ApproachTargetHeight(new Vector3(Direction.x, fish.transform.position.y + targetHeight, Direction.y));
        
        /*Debug.Log(Mathf.Abs(Vector3.Distance(new Vector3(fish.transform.position.x, 0, fish.transform.position.z), fish.agent.destination)) - fish.agent.stoppingDistance + " -> " + "0.3\n" + fish.agent.destination);
        if (Mathf.Abs(Vector3.Distance(new Vector3(fish.transform.position.x, 0, fish.transform.position.z), fish.agent.destination)) - fish.agent.stoppingDistance <= 1f)
        {
            enemyStateMachine.ChangeState(fish.idleState);
            
        }*/
    }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerEnter(Collider other) { }
}
