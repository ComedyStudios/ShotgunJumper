using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StartScreenButtonManager : MonoBehaviour
    {
        public string startScene;
        public string settingScene;
        
        public void QuitGame()
        {
            Application.Quit();
        }
        public void OpenSettings()
        {
            SceneManager.LoadScene(settingScene);
        }
        public void StartGame()
        {
            SceneManager.LoadScene(startScene);
        }
    }
}