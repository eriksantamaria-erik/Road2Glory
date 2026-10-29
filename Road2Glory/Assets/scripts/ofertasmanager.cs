using System.Collections.Generic;
using UnityEngine;

public class OfferManager : MonoBehaviour
{
    public CareerManager careerManager;
    public DatabaseManager databaseManager;

    public List<Club> ofertas = new List<Club>();

    public void GenerarOfertasIniciales()
    {
        ofertas.Clear();

        if (careerManager == null)
        {
            Debug.LogError("No hay CareerManager conectado.");
            return;
        }

        if (databaseManager == null)
        {
            Debug.LogError("No hay DatabaseManager conectado.");
            return;
        }

        if (careerManager.jugador == null)
        {
            Debug.LogError("No existe ningún jugador.");
            return;
        }

        int mediaJugador = careerManager.jugador.media;

        Debug.Log("Generando ofertas para jugador de media: " + mediaJugador);

        List<Club> clubesCompatibles = new List<Club>();

        foreach (Club club in databaseManager.datos.clubes)
        {
            // Buscamos clubes cercanos a la media del jugador
            if (club.media >= mediaJugador - 5 &&
                club.media <= mediaJugador + 5)
            {
                clubesCompatibles.Add(club);
            }
        }

        Debug.Log("Clubes compatibles encontrados: " + clubesCompatibles.Count);

        // Elegimos hasta 3 ofertas
        int cantidadOfertas = Mathf.Min(3, clubesCompatibles.Count);

        for (int i = 0; i < cantidadOfertas; i++)
        {
            int indice = Random.Range(0, clubesCompatibles.Count);

            Club clubElegido = clubesCompatibles[indice];

            ofertas.Add(clubElegido);

            clubesCompatibles.RemoveAt(indice);

            // Buscar el nombre de la liga
            Liga liga = databaseManager.datos.ligas.Find(
                l => l.id == clubElegido.liga_id
            );

            string nombreLiga = liga != null
                ? liga.nombre
                : "Liga desconocida";

            Debug.Log("===== OFERTA " + (i + 1) + " =====");
            Debug.Log("Equipo: " + clubElegido.nombre);
            Debug.Log("Liga: " + nombreLiga);
            Debug.Log("Presupuesto: " + clubElegido.presupuesto_millones + " M€");
        }
    }

    public void ElegirClub(int indiceOferta)
    {
        if (indiceOferta < 0 || indiceOferta >= ofertas.Count)
        {
            Debug.LogError("Oferta no válida.");
            return;
        }

        Club clubElegido = ofertas[indiceOferta];

        careerManager.jugador.clubId = clubElegido.id;

        Liga liga = databaseManager.datos.ligas.Find(
            l => l.id == clubElegido.liga_id
        );

        string nombreLiga = liga != null
            ? liga.nombre
            : "Liga desconocida";

        Debug.Log("===== CLUB ELEGIDO =====");
        Debug.Log("Equipo: " + clubElegido.nombre);
        Debug.Log("Liga: " + nombreLiga);
        Debug.Log("Presupuesto: " + clubElegido.presupuesto_millones + " M€");
        Debug.Log("Club ID: " + clubElegido.id);
        Debug.Log("Jugador ahora pertenece a: " + clubElegido.nombre);
    }
}