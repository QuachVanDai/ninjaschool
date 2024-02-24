
using UnityEngine.SceneManagement;

public static class Game
{
    
    public static void Continue()
    {
        SceneManager.LoadSceneAsync("Player");
        //gameManager.Instance.IsPlaygame = true;
    }
    public static void Quit()
    {
        ConfirmPanel.Ask("Are you sure you want to quit game?", QuitImediately);
    }
    public static void QuitImediately()
    {
        // EventManager.RaiseEvent("OnGameSave");
/*#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif*/
    }
}