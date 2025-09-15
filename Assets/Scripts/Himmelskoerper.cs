using UnityEngine;

public class Himmelskoerper : MonoBehaviour
{
    #region Instanzvariablen
    // Physikalische Werte
    protected double masse;
    protected double durchmesserInKm;
    protected double gewichtskraft;

    // Verwaltung
    GameManager gm = GameManager.Instance;
    public string Name;


    // Euler-Ansatz (Vector)
    public Vector3 InitialVelocity { get; set; }
    [HideInInspector] public Vector3 bewegungsvektor;

    #endregion

    #region Accessoren
    public double Masse
    {
        get => masse;
    }
    public double DurchmesserInKm
    {
        get => durchmesserInKm;
        set => durchmesserInKm = value;
    }

    #endregion

    #region MonoBehaviour-Methoden
    private void Awake()
    {
        gm.InitInfosCall += InitInfos;
    }
    void Start()
    {
        // Euler Integration Start
        //bewegungsvektor = initialVelocity;
    }

    void Update()
    {

    }
    private void OnDestroy()
    {
        gm.InitInfosCall -= InitInfos;
    }
    #endregion

    #region Physics
    #region Euler-Integration
    //Euler Integration - Für Massereiche Körper deprecated. Stattdessen KeplerOrbit und SOI Mechanics nutzen
    public void UpdateVelocity(Vector3 _beschleunigung, float _deltaTime) => bewegungsvektor += _beschleunigung * _deltaTime;
    public void UpdatePosition(float deltaTime) => transform.position += bewegungsvektor * deltaTime;
    #endregion


    #endregion

    private void InitInfos()
    {

    }
}