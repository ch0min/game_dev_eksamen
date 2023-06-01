using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneScript : MonoBehaviour
{

    public string sceneName;
    bool hasTriggered = false;
    

    private void OnTriggerEnter(Collider other) {
        if (!hasTriggered && other.CompareTag("Player")) {
            hasTriggered = true;
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            StartCoroutine(LoadSceneWithDelay(audioSource.clip.length));
        }
    }

    private IEnumerator LoadSceneWithDelay(float delay) {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }
}
