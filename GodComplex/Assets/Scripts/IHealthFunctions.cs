public interface IHealthFunctions
{
    /// <summary>
    /// Delivers the specified amount of damage to the entity after dividing with pre-assigned multiplier.
    /// </summary>
    /// <param name="damageValue"></param>
    void ReceiveDamage(float damageValue);

    /// <summary>
    /// Delivers the specified amount of health to the entity.
    /// </summary>
    /// <param name="healthValue"></param>
    void Heal(float healthValue);
}
