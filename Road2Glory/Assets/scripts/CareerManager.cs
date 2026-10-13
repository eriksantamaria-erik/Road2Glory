using UnityEngine;

public class CareerManager : MonoBehaviour
{
    public PlayerData jugador;

    public void CrearJugador(string nombre, string posicion, int edad)
    {
        jugador = new PlayerData();

        jugador.nombre = nombre;
        jugador.posicion = posicion;
        jugador.edad = edad;

        jugador.media = 60;


        jugador.clubId = "";

        jugador.salario = 0;
        jugador.moral = 75;
        jugador.forma = 75;
        jugador.aura = 75;

        jugador.partidos = 0;
        jugador.goles = 0;
        jugador.asistencias = 0;

        Debug.Log(" JUGADOR CREADO ");
        Debug.Log("Nombre: " + jugador.nombre);
        Debug.Log("Posición: " + jugador.posicion);
        Debug.Log("Edad: " + jugador.edad);
        Debug.Log("Media: " + jugador.media);

    }
}