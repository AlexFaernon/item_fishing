using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : MonoBehaviour
{
 [Header("Tags")] [SerializeField] private string musicTag;
 private void Awake()
 { 
   GameObject obj = GameObject.FindWithTag(musicTag);
   if (obj != null) 
       Destroy(gameObject);
   else
   {
       gameObject.tag = musicTag;
       DontDestroyOnLoad(gameObject);
   }

   SceneManager.sceneLoaded += OnSceneLoaded;
 }
 
 private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => gameObject.GetComponent<AudioSource>().mute = scene.name == "SampleScene";

 private void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;
}
