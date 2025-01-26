using UnityEngine;

//  Track our Game's State
public enum GameState
{
    Game,
    Menu
}

/*public class GameManager : MonoBehaviour
{
    [Header("State of Game")]
    public GameState gameState;

    private void Start()
    {

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (gameState != GameState.Menu)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}*/