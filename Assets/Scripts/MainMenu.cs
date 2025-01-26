using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;    // Reference to the Start Game button
    public Button creditsButton;  // Reference to the Credits button
    public Button quitButton;     // Reference to the Quit Game button
    public Button closeCreditsButton; // Button to close the credits screen

    [Header("Menu Screens")]
    public GameObject creditsScreen; // Reference to the credits screen

    /// <summary>
    /// Called when the Main Menu scene starts.
    /// Assigns OnClick events to the buttons.
    /// </summary>
    private void Start()
    {
        // Assign OnClick listeners to the buttons
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        if (creditsButton != null)
            creditsButton.onClick.AddListener(OpenCredits);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        if (closeCreditsButton != null)
            closeCreditsButton.onClick.AddListener(CloseCredits);

        Debug.Log("Buttons configured with OnClick events.");
    }

    /// <summary>
    /// Called when the Start Game button is pressed.
    /// Loads the first level (SampleScene).
    /// </summary>
    public void StartGame()
    {
        // Trigger Corgi Engine LevelStart event (if required)
        MoreMountains.CorgiEngine.CorgiEngineEvent.Trigger(MoreMountains.CorgiEngine.CorgiEngineEventTypes.LevelStart);

        // Load the game scene
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Game Started. Loading SampleScene.");
    }

    /// <summary>
    /// Called when the Credits button is pressed.
    /// Displays the credits screen.
    /// </summary>
    public void OpenCredits()
    {
        if (creditsScreen != null)
        {
            creditsScreen.SetActive(true);
            Debug.Log("Credits Screen Opened.");
        }
    }

    /// <summary>
    /// Called when the Close Credits button is pressed.
    /// Hides the credits screen.
    /// </summary>
    public void CloseCredits()
    {
        if (creditsScreen != null)
        {
            creditsScreen.SetActive(false);
            Debug.Log("Credits Screen Closed.");
        }
    }

    /// <summary>
    /// Called when the Quit button is pressed.
    /// Exits the application.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Game Quit.");
        Application.Quit();
    }
}
