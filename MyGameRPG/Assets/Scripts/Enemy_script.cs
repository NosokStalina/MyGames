using UnityEngine;
public class Enemy_script : MonoBehaviour
{
    [Header("Характеристики врага")]
    [Range(0, 20)] public float Speed_Enemy;
    [Min(0)] public uint Health_Enemy;
    [Range(0, 20)] public uint Damage_Enemy;
    [Range(0, 7.5f)] public float RangeTrigger;
    [Range(0, 7.5f)] public float maxCoolDown_Punch;
    public bool FaceLeft = false;
    private float CoolDown_Punch;
    [Header("Компоненты врага")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Мозг врага")]
    public Transform Target;
    public LayerMask solidTrigger;
    private void Start()
    {
        Target = GameObject.Find("MainChest").transform;
    }
    private void Update()
    {
        CheckingTimers();
        Collider2D triggerCollider = Physics2D.OverlapCircle(transform.position, RangeTrigger, solidTrigger);
        if(triggerCollider != null)
        {
            if (triggerCollider.CompareTag("Chest") && CoolDown_Punch <= 0)
            {
                MainChest mainChest = triggerCollider.GetComponent<MainChest>();
                mainChest.GetDamage(Damage_Enemy); CoolDown_Punch = maxCoolDown_Punch;
            }
        }
        gameObject.transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed_Enemy * Time.deltaTime);
        if (transform.position.x > Target.position.x && FaceLeft == false)
            FlipFace();
        else if (transform.position.x < Target.position.x && FaceLeft == true)
            FlipFace();

    }
    void FlipFace()
    {
        FaceLeft = !FaceLeft; 
        transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
    }
    void CheckingTimers()
    {
        if(CoolDown_Punch > 0)
            CoolDown_Punch -= 1 * Time.deltaTime;
    }
}