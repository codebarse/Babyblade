using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	bool gameHasEnded = false;
	int numberOfPlayers = 3;

    public void EndGame()
    {
    	numberOfPlayers--;
    	if(numberOfPlayers <= 1)
        {
        	Debug.Log("Game Over");
        	Restart();
        }
    }

    void Restart()
    {

    }
}
