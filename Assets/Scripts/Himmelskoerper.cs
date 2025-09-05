using UnityEngine;

public class Himmelskoerper : MonoBehaviour
{
    #region Instanzvariablen
    // Physikalische Werte
    protected double masse;
    [HideInInspector] public Vector3 bewegungsvektor;

    // Allgemein
    public Vector3 InitialVelocity { get; set; }

    #endregion

    #region Accessoren
    public double Masse
    {
        get => masse;
    }
    #endregion

    #region MonoBehaviour-Methoden
    void Start()
    {
        // Euler Integration Start
        //bewegungsvektor = initialVelocity;
    }

    void Update()
    {

    }
    #endregion

    #region Physics
    //Euler Integration - Für Massereiche Körper deprecated. Stattdessen KeplerOrbit und SOI Mechanics nutzen
    public void UpdateVelocity(Vector3 _beschleunigung, float _deltaTime) => bewegungsvektor += _beschleunigung * _deltaTime;
    public void UpdatePosition(float deltaTime) => transform.position += bewegungsvektor * deltaTime;

    #endregion
}