using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Estadísticas del arma")]
    [SerializeField] private int projectileDamage = 1;
    [SerializeField] private int maxAmmo = 8;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float fireRate = 3f;

    [Header("Límites de mejoras")]
    [SerializeField] private float minimumReloadTime = 0.2f;

    // ========================
    // VALORES ACTUALES
    // ========================

    public int ProjectileDamage => projectileDamage;
    public int MaxAmmo => maxAmmo;
    public float ReloadTime => reloadTime;
    public float FireRate => fireRate;


    // ========================
    // MEJORAS
    // ========================

    public void IncreaseDamage(int amount)
    {
        projectileDamage += amount;
    }

    public void IncreaseMaxAmmo(int amount)
    {
        maxAmmo += amount;
    }

    public void ReduceReloadTime(float amount)
    {
        reloadTime -= amount;

        if (reloadTime < minimumReloadTime)
        {
            reloadTime = minimumReloadTime;
        }
    }

    public void IncreaseFireRate(float amount)
    {
        fireRate += amount;
    }
}