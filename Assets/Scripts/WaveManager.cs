using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    [Header("Configuración")]
    public int oleadaActual = 1;

    [Header("Estado")]
    public int enemigosPorOleada = 5;
    public int enemigosGenerados;
    public int enemigosMuertos;

    [Header("UI Toolkit")]
    [SerializeField] private UIDocument uiDocument;

    [Header("Tienda")]
    [SerializeField] private GameObject shopButton;

    private VisualElement waveCompletePanel;
    private Label waveTitleLabel;
    private Button nextWaveButton;
    private Button restartWaveButton;

    public bool oleadaActiva = false;

    public int enemigosRestantes
    {
        get
        {
            return enemigosPorOleada - enemigosMuertos;
        }
    }

    private void Start()
    {
        ConfigurarUI();
        IniciarOleada();
    }

    private void Update()
    {
        if (oleadaActiva && enemigosRestantes <= 0)
        {
            FinalizarOleada();
        }
    }

    private void ConfigurarUI()
    {
        if (uiDocument == null)
        {
            Debug.LogError("WaveManager: No hay UIDocument asignado.");
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;

        waveCompletePanel = root.Q<VisualElement>("waveCompletePanel");
        waveTitleLabel = root.Q<Label>("waveTitleLabel");
        nextWaveButton = root.Q<Button>("nextWaveButton");
        restartWaveButton = root.Q<Button>("restartWaveButton");

        Debug.Log("Panel encontrado: " + (waveCompletePanel != null));
        Debug.Log("Label encontrado: " + (waveTitleLabel != null));
        Debug.Log("Botón siguiente encontrado: " + (nextWaveButton != null));
        Debug.Log("Botón reiniciar encontrado: " + (restartWaveButton != null));

        OcultarPanelOleada();

        if (restartWaveButton != null)
        {
            restartWaveButton.clicked += ReiniciarOleada;
        }

        if (nextWaveButton != null)
        {
            nextWaveButton.clicked += BotonSiguienteOleadaTemporal;
        }
    }

    public void IniciarOleada()
    {
        oleadaActiva = true;

        enemigosGenerados = 0;
        enemigosMuertos = 0;

        if (shopButton != null)
        {
            shopButton.SetActive(false);
        }

        OcultarPanelOleada();

        Debug.Log(
            "Comenzó la oleada " + oleadaActual +
            " con " + enemigosPorOleada + " enemigos.");
    }

    public void EnemigoGenerado()
    {
        enemigosGenerados++;

        Debug.Log("Enemigos generados: " + enemigosGenerados);
    }

    public void EnemigoMuerto()
    {
        enemigosMuertos++;

        Debug.Log("Enemigo eliminado registrado");
        Debug.Log("Muertos: " + enemigosMuertos);
        Debug.Log("Restantes: " + enemigosRestantes);
    }

    private void FinalizarOleada()
    {
        oleadaActiva = false;

        if (shopButton != null)
        {
            shopButton.SetActive(true);
        }

        Debug.Log("Oleada " + oleadaActual + " completada.");

        MostrarPanelOleada();
    }

    private void MostrarPanelOleada()
    {
        if (waveCompletePanel == null)
        {
            Debug.LogError("No se encontró waveCompletePanel.");
            return;
        }

        if (waveTitleLabel == null)
        {
            Debug.LogError("No se encontró waveTitleLabel.");
            return;
        }

        waveTitleLabel.text =
            "Oleada " + oleadaActual + " completada";

        waveCompletePanel.style.display = DisplayStyle.Flex;
        waveCompletePanel.visible = true;

        Debug.Log("Panel de oleada mostrado.");
    }

    private void OcultarPanelOleada()
    {
        if (waveCompletePanel == null)
            return;

        waveCompletePanel.style.display = DisplayStyle.None;
        waveCompletePanel.visible = false;
    }

    public void ReiniciarOleada()
    {
        Debug.Log("Reiniciando oleada " + oleadaActual);

        IniciarOleada();
    }

    private void BotonSiguienteOleadaTemporal()
    {
        Debug.Log("Botón de siguiente oleada todavía sin función.");
    }
}