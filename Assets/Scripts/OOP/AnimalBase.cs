using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum EMoveType
{
    None,
    Walk,
    Fly,
    Swim
}

public abstract class AnimalBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected virtual void OnMove()
    {
        Debug.Log("AnimalBase On Move");
        Health = 105;
    }

    [ContextMenu("Test Death")]
    public void TestDeath()
    {
        Health = -5;
    }

    public float Health
    {
        get { return health; }
        protected set
        {
            Debug.Log($"Health [{value}]");
            value = Mathf.Clamp(value, 0, 100);
            Debug.Log($"Clamped Health [{value}]");
            health = value;
            onHealthChanged?.Invoke(value);
            if(Mathf.Approximately(health, 0))
            {
                Debug.Log($"OnDeath");
                OnDeath?.Invoke();
            }
        }
    }
    private float health;
    public UnityAction<float> onHealthChanged;
    public UnityAction OnDeath;

    [SerializeField]
    protected AttackType AttackType;
    
    [SerializeField]
    protected AttackType CheckAttackType;

    [ContextMenu("Test Attack Type")]
    public void CheckAttack()
    {
        Debug.Log($"Checking Attack Type {AttackType} against {CheckAttackType}: " +
            $"Result: {AttackType & CheckAttackType}");
        
        if ((AttackType & CheckAttackType) == CheckAttackType)
        {
            Debug.Log($"Attack Type {AttackType} has Check");
        }
        else
        {
            Debug.Log("Attack type failed check");
        }
    }

    [ContextMenu("Combine Attack Types")]
    public void CombineAttack()
    {
        AttackType |= CheckAttackType;
    }

}


