using System.Collections;
using TMPro;
using UnityEngine;

public class Cañon : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPosition;

    [Header("UI")]
    [SerializeField] private GameObject reloadPanel;
    [SerializeField] private TMP_Text ammoText;

    [Header("Rotación")]
    [SerializeField, Range(30f, 720f)]
    private float rotationSpeed = 180f;

    private int currentAmmo;
    private float nextFireTime;
    private bool isReloading;

    private Camera cam;
    private PlayerStats playerStats;
    private WaveManager waveManager;

    private void Start()
    {
        cam = Camera.main;

        playerStats = FindFirstObjectByType<PlayerStats>();
        waveManager = FindFirstObjectByType<WaveManager>();

        currentAmmo = playerStats.MaxAmmo;

        reloadPanel.SetActive(false);

        UpdateAmmoUI();
    }

    private void Update()
    {
        if (GameManager.IsGameOver)
            return;

        if (waveManager == null || !waveManager.oleadaActiva)
        {
            reloadPanel.SetActive(false);
            return;
        }

        RotateToMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading && currentAmmo < playerStats.MaxAmmo)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private void RotateToMouse()
    {
        Vector2 mouseWorldPoint =
            cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction =
            mouseWorldPoint - (Vector2)transform.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation =
            Quaternion.Euler(0, 0, angle);

        transform.rotation =
            Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (waveManager == null || !waveManager.oleadaActiva)
            return;

        if (isReloading)
            return;

        if (Time.time < nextFireTime)
            return;

        if (currentAmmo <= 0)
        {
            reloadPanel.SetActive(true);
            return;
        }

        Projectile projectile =
            Instantiate(
                projectilePrefab,
                shootPosition.position,
                shootPosition.rotation);

        projectile.SetDamage(playerStats.ProjectileDamage);

        projectile.LaunchProjectile(shootPosition.right);

        currentAmmo--;

        UpdateAmmoUI();

        if (currentAmmo <= 0)
        {
            reloadPanel.SetActive(true);
        }

        nextFireTime =
            Time.time + (1f / playerStats.FireRate);
    }

    private IEnumerator Reload()
    {
        if (waveManager == null || !waveManager.oleadaActiva)
            yield break;

        isReloading = true;

        yield return new WaitForSeconds(playerStats.ReloadTime);

        if (waveManager == null || !waveManager.oleadaActiva)
        {
            isReloading = false;
            reloadPanel.SetActive(false);
            yield break;
        }

        currentAmmo = playerStats.MaxAmmo;

        UpdateAmmoUI();

        reloadPanel.SetActive(false);

        isReloading = false;
    }

    private void UpdateAmmoUI()
    {
        ammoText.text =
            currentAmmo + " / " + playerStats.MaxAmmo;
    }
}