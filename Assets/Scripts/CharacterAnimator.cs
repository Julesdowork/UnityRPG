using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    public AnimationClip[] defaultAttackAnimSet;
    public AnimationClip replaceableAttackAnim;

    const float locomotionAnimationSmoothTime = .1f;

    protected Animator anim;
    private NavMeshAgent agent;
    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;
    protected AnimationClip[] currentAttackAnimSet;

	// Use this for initialization
	protected virtual void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        anim.SetBool("inCombat", combat.InCombat);
	}

    protected virtual void OnAttack()
    {
        anim.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
