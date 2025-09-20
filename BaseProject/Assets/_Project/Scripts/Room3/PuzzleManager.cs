using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private DoorController doorController; // Arraste a porta que este puzzle controla
    [SerializeField] private List<int> correctSequence; // Defina a sequ�ncia correta de n�meros (ex: 5, 6, 2)

    private List<int> currentSequence = new List<int>();

    private void Awake()
    {
        TubeController.puzzleManager = this;
    }

    public void TubeFilled(int value)
    {
        if (!currentSequence.Contains(value)) // Evita adicionar o mesmo tubo duas vezes
        {
            currentSequence.Add(value);
        }

        // Verifica se a sequ�ncia est� correta a cada bola inserida
        if (currentSequence.Count == correctSequence.Count)
        {
            // A fun��o SequenceEqual verifica se as duas listas s�o id�nticas na mesma ordem
            if (currentSequence.SequenceEqual(correctSequence))
            {
                Debug.Log("Sequ�ncia correta! Abrindo a porta.");
                if (doorController != null)
                {
                    doorController.OpenDoor();
                }
            }
            else // Se a contagem � a mesma mas a sequ�ncia est� errada, avisa o jogador.
            {
                Debug.Log("Sequ�ncia incorreta. Interaja com o bot�o de reset para tentar novamente.");
            }
        }
    }

    // Fun��o p�blica para resetar o estado do puzzle
    public void ResetPuzzle()
    {
        Debug.Log("Resetando o puzzle...");
        currentSequence.Clear();

        // Encontra todos os tubos na cena e chama a fun��o para reset�-los
        TubeController[] tubes = FindObjectsOfType<TubeController>();
        foreach (TubeController tube in tubes)
        {
            tube.ResetTube();
        }

        // Encontra todas as bolas na cena e chama a fun��o para reset�-las
        BallController[] balls = FindObjectsOfType<BallController>();
        foreach (BallController ball in balls)
        {
            ball.ResetPosition();
        }
    }
}

