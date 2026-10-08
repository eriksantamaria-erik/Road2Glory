using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Liga
{
    public string id;
    public string nombre;
    public string pais;
    public int division;
    public int nivel_medio;
}

[Serializable]
public class Club
{
    public string id;
    public string nombre;
    public string pais;
    public string liga_id;
    public int division;
    public int media;
    public int reputacion;
    public float presupuesto_millones;
    public int cantera;
    public int minutos_para_jovenes;
}

[Serializable]
public class BaseDatos
{
    public int version;
    public string temporada_base;
    public string notas;
    public int total_ligas;
    public int total_clubes;

    public List<Liga> ligas;
    public List<Club> clubes;
}

public class DatabaseManager : MonoBehaviour
{
    public TextAsset archivoJSON;

    public BaseDatos datos;

    void Awake()
    {
        CargarBaseDeDatos();
    }

    void CargarBaseDeDatos()
    {
        if (archivoJSON == null)
        {
            Debug.LogError("❌ No se ha asignado el archivo JSON.");
            return;
        }

        datos = JsonUtility.FromJson<BaseDatos>(archivoJSON.text);


        Debug.Log("BASE DE DATOS CARGADA");
        Debug.Log("Temporada: " + datos.temporada_base);
        Debug.Log("Ligas: " + datos.total_ligas);
        Debug.Log("Clubes: " + datos.total_clubes);

    }
}