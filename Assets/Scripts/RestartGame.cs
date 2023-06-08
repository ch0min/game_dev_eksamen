using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
<<<<<<< Updated upstream
    public void RestartTheGame() {
=======
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartTheGame()
    {
>>>>>>> Stashed changes
        SceneManager.LoadScene("Main Menu");
    }
}
