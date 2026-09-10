using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject shopPanel;

    [Header("Información de mejoras")]
    [SerializeField] private TMP_Text damageInfo;
    [SerializeField] private TMP_Text ammoInfo;
    [SerializeField] private TMP_Text reloadInfo;
    [SerializeField] private TMP_Text fireRateInfo;

    [Header("Precios")]
    [SerializeField] private int damagePrice = 20;
    [SerializeField] private int ammoPrice = 20;
    [SerializeField] private int reloadPrice = 20;
    [SerializeField] private int fireRatePrice = 20;

    private void Start()
    {
        shopPanel.SetActive(false);

        UpdateShopUI();
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);

        UpdateShopUI();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void UpgradeDamage()
    {
        if (GameManager.Instance.SpendPoints(damagePrice))
        {
            playerStats.IncreaseDamage(1);

            UpdateShopUI();
        }
    }

    public void UpgradeAmmo()
    {
        if (GameManager.Instance.SpendPoints(ammoPrice))
        {
            playerStats.IncreaseMaxAmmo(2);

            UpdateShopUI();
        }
    }

    public void UpgradeReload()
    {
        if (GameManager.Instance.SpendPoints(reloadPrice))
        {
            playerStats.ReduceReloadTime(0.3f);

            UpdateShopUI();
        }
    }

    public void UpgradeFireRate()
    {
        if (GameManager.Instance.SpendPoints(fireRatePrice))
        {
            playerStats.IncreaseFireRate(1f);

            UpdateShopUI();
        }
    }

    private void UpdateShopUI()
    {
        damageInfo.text =
            "Daño actual: " + playerStats.ProjectileDamage +
            "\nPrecio: " + damagePrice;

        ammoInfo.text =
            "Munición: " + playerStats.MaxAmmo +
            "\nPrecio: " + ammoPrice;

        reloadInfo.text =
            "Tiempo de recarga: " +
            playerStats.ReloadTime.ToString("F1") + "s" +
            "\nPrecio: " + reloadPrice;

        fireRateInfo.text =
            "Cadencia: " +
            playerStats.FireRate.ToString("F1") +
            "\nPrecio: " + fireRatePrice;
    }
}