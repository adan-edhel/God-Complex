using UnityEngine;

public class Character : MonoBehaviour, IHealthFunctions
{
    /// <summary>
    /// Returns the constant max health value of an entity. Can only be set in initial implementation.
    /// </summary>
    protected const float MaxHealth = 100f;

    /// <summary>
    /// Returns the private health value of an entity. Defaults to max health at start. If you only wish to get the value, use the "Health" property instead.
    /// </summary>
    [SerializeField, Header("Base Values", order = 0), Range(1, MaxHealth)]
    protected float _health = MaxHealth;

    /// <summary>
    /// Damage reduction multiplier of an entity. Divides damage by assigned value. Default = 1.
    /// </summary>
    [SerializeField, Range(1, 10)]
    private int _damageReductionMultiplier = 1;

    /// <summary>
    /// Returns the maximum health value of an entity. Cannot be set.
    /// </summary>
    public float MaxHP { get { return MaxHealth; } private set { Debug.Log($"Trying to set the 'Max Health Points' property in {this} while it's not allowed. What are you even trying to do? Adjust the _maxHealth field value in the script instead."); } }

    /// <summary>
    /// Returns the health value of an entity. Cannot be set.
    /// </summary>
    public float HP { get { return _health; } private set { Debug.Log($"Trying to set the 'Health Points' property in {this} while it's not allowed. Use the _health field instead, or better yet; Ask Mort what to do."); } }

    /// <summary>
    /// Player input processor of the character
    /// </summary>
    public InputProcessor playerInput { get; set; }

    /// <summary>
    /// Refer the IHealthFunctions interface instead!
    /// </summary>
    /// <param name="damageValue"></param>
    public void ReceiveDamage(float damageValue)
    {
        // Return if health is equal or lower than 0.
        if (_health <= 0) return;

        // Deliver damage divided by reduction modifier.
        _health = _health - damageValue / _damageReductionMultiplier;

        // Call death function once health is 0 or lower.
        if (_health <= 0)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// Refer the IHealthFunctions interface instead!
    /// </summary>
    /// <param name="healthValue"></param>
    public void Heal(float healthValue) /// Implemented in case this is used for interaction with the public.
    {
        // Return if health is higher than max health amount.
        if (_health > MaxHP)
        {
            _health = MaxHP;
            return;
        }

        // Receive health
        _health = _health + healthValue;
    }

    /// <summary>
    /// Override in child classes when necessary to perform functions when death occurs.
    /// </summary>
    protected virtual void OnDeath()
    {
        // Reserved for child class functions.
        Destroy(this);
    }
}
