using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy_script : MonoBehaviour
{
    [Header("Точки перемещения")]
    public List<Vector2> PointsMooving = new List<Vector2>();
    [Header("Статистика врага")]
    [Min(0)] public uint Health_enemy = 1;
    [Min(1)] public uint Damage_enemy = 1;
    [Min(1)] public float Speed_enemy = 1;
    [Min(0.1f)] public float radius_Trigger = 0.1f;
    [Header("Режим врага")]
    public bool PassiveMode = true;
    public bool CheckLastTrigger = false;
    public bool IsMooving = false;
    [Header("Мозг врага")]
    public int CurrentPoint = 0;
    public bool GoStraight = true;
    public Vector2 Triggered;
    public LayerMask TriggerSolid;
    public float CoolDown_CheckLastPoint = 0;
    //Скрытые елементы
    bool ChangedPoint = false;
    private void Start()
    {
        SearchPoints(); GoToStartPoint();
    }
    private void Update()
    {
        EnemyCheck_dead();
        if (PassiveMode == false && Triggered != new Vector2(0, 0))
        {
            CheckLastTrigger = true;
        }
        else if (PassiveMode == true && CheckLastTrigger == false)
        {
            if (ChangedPoint == false)
                StartCoroutine(ChangePoint());
            if ((transform.position.x != PointsMooving[CurrentPoint].x) && transform.position.y != PointsMooving[CurrentPoint].y)
            {
                transform.position = Vector2.MoveTowards(transform.position, PointsMooving[CurrentPoint], Speed_enemy * Time.deltaTime);
                IsMooving = true;
            }
            else
                IsMooving = false;
        }
        else
            MooveTrggier();
        if (CheckLastTrigger == true)
        {
            MooveTrggier();
            if (CoolDown_CheckLastPoint > 0)
                CoolDown_CheckLastPoint -= 1 * Time.deltaTime;
            else
                NulledPointTrigger();
        }
        CheckRadius();
    }
    private void MooveTrggier()
    {
        if(Triggered != new Vector2(0, 0))
            transform.position = Vector2.MoveTowards(transform.position, Triggered, Speed_enemy * Time.deltaTime);
    }
    void NulledPointTrigger()
    {
        CheckLastTrigger = false;
        Triggered = new Vector2(0, 0);
        PassiveMode = true;
    }
    private void CheckRadius()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius_Trigger, TriggerSolid);
        if(collider != null)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Transform posTrigger = collider.transform;
                Triggered = new Vector2(posTrigger.position.x, posTrigger.position.y);
                PassiveMode = false;
                CoolDown_CheckLastPoint = 10f;
            }
        }
    }
    private void GoToStartPoint()
    {
        transform.position = PointsMooving[CurrentPoint];
    }
    private void SearchPoints()
    {
        GameObject path_Road = GameObject.Find("PathRoad");
        for (int i = 0; i < path_Road.transform.childCount; i++)
        {
            Transform child = path_Road.transform.GetChild(i);
            PointsMooving.Add(new Vector2(child.position.x, child.position.y));
        }
    }
    IEnumerator ChangePoint()
    {
        ChangedPoint = true;
        if (CurrentPoint == 0)
            GoStraight = true;
        else if (CurrentPoint == PointsMooving.Count - 1)
            GoStraight = false;
        if (GoStraight == true)
            CurrentPoint++;
        else
            CurrentPoint--;
        yield return new WaitForSeconds(Random.Range(4, 15));
        ChangedPoint = false;
    }
    private void EnemyCheck_dead()
    {
        if(Health_enemy <= 0)
            Destroy(gameObject);
    }
    public void Get_Damage(uint Damage_)
    {
        if (Health_enemy <= Damage_)
            Health_enemy = 0;
        else
            Health_enemy -= Damage_;
    }
}