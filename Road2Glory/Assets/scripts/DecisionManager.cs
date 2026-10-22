using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public PlayerData jugador;

    public void LanzarPenalti(int opcion)
    {
        if (jugador == null)
        {
            Debug.LogError("No hay jugador conectado al DecisionManager.");
            return;
        }

        float probabilidad = 0f;

        switch (opcion)
        {
            case 1:
                // PENALTI FÁCIL
                probabilidad = 80f;
                break;

            case 2:
                // PENALTI NORMAL
                probabilidad = 65f;
                break;

            case 3:
                // PENALTI DIFÍCIL
                probabilidad = 55f;
                break;

            default:
                Debug.LogError("Opción de penalti no válida.");
                return;
        }

        // La forma modifica la probabilidad
        probabilidad += (jugador.forma - 75f) * 0.2f;

        // La moral también modifica la probabilidad
        probabilidad += (jugador.moral - 75f) * 0.1f;

        // Nunca puede ser menor del 5% ni mayor del 95%
        probabilidad = Mathf.Clamp(probabilidad, 5f, 95f);

        // Número aleatorio entre 0 y 100
        float resultado = Random.Range(0f, 100f);

        Debug.Log("===== DECISIÓN DE PENALTI =====");
        Debug.Log("Jugador: " + jugador.nombre);
        Debug.Log("Dificultad: " + opcion);
        Debug.Log("Probabilidad de marcar: " + probabilidad + "%");
        Debug.Log("Resultado aleatorio: " + resultado);

        if (resultado <= probabilidad)
        {
            PenaltiMarcado(opcion);
        }
        else
        {
            PenaltiFallado();
        }
    }

    private void PenaltiMarcado(int opcion)
    {
        jugador.goles++;

        Debug.Log("⚽ ¡GOOOOOOL!");

        switch (opcion)
        {
            case 1:
                // FÁCIL
                jugador.moral += 0;
                jugador.aura += 0;

                Debug.Log("Penalti fácil: sin recompensa extra.");
                break;

            case 2:
                // NORMAL
                jugador.moral += 5;
                jugador.aura += 3;

                Debug.Log("Penalti normal: +5 moral, +3 aura.");
                break;

            case 3:
                // DIFÍCIL
                jugador.moral += 10;
                jugador.aura += 7;

                Debug.Log("Penalti difícil: +10 moral, +7 aura.");
                break;
        }

        // Evitamos que moral, forma o aura pasen de 100
        jugador.moral = Mathf.Clamp(jugador.moral, 0, 100);
        jugador.forma = Mathf.Clamp(jugador.forma, 0, 100);
        jugador.aura = Mathf.Clamp(jugador.aura, 0, 100);

        Debug.Log("Goles: " + jugador.goles);
        Debug.Log("Moral: " + jugador.moral);
        Debug.Log("Forma: " + jugador.forma);
        Debug.Log("Aura: " + jugador.aura);
    }

    private void PenaltiFallado()
    {
        Debug.Log("❌ PENALTI FALLADO");

        jugador.moral -= 10;
        jugador.forma -= 5;

        // Evitamos valores negativos
        jugador.moral = Mathf.Clamp(jugador.moral, 0, 100);
        jugador.forma = Mathf.Clamp(jugador.forma, 0, 100);

        Debug.Log("Goles: " + jugador.goles);
        Debug.Log("Moral: " + jugador.moral);
        Debug.Log("Forma: " + jugador.forma);
        Debug.Log("Aura: " + jugador.aura);
    }
}