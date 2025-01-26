using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
    public class PauseButton : CorgiMonoBehaviour
    {
        public void ResumeGame()
        {
            StartCoroutine(ResumeGameCo());
        }

        public void RestartLevel()
        {
            StartCoroutine(RestartLevelCo());
        }
        public virtual void ReturnToMainMenu()
        {
            StartCoroutine(ReturnToMainMenuCo());
        }

        protected IEnumerator ResumeGameCo()
        {
            yield return null;
            CorgiEngineEvent.Trigger(CorgiEngineEventTypes.TogglePause);
        }

        protected IEnumerator RestartLevelCo()
        {
            yield return null;
            MMSceneLoadingManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        protected virtual IEnumerator ReturnToMainMenuCo()
        {
            yield return null;

            // Set the scene to load and trigger the loading process
            MMSceneLoadingManager.LoadScene("MainMenu", MMSceneLoadingManager.LoadingScreenSceneName);
        }

        private void CleanupReferences()
        {
            // Nullify references or clear listeners if needed
            // Example: Remove listeners or nullify static variables
        }
    }
}
