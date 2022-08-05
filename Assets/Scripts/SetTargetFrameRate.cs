using UnityEngine;
 
 public class SetTargetFrameRate : MonoBehaviour 
 {
     public int targetFrameRate = 60;
 
     private void Start()
     {
         QualitySettings.vSyncCount = 0;
         Application.targetFrameRate = targetFrameRate;
     }
    //  private void Update() {
    //     float c;
    //     c = (int)(1f/Time.unscaledDeltaTime);
    //     Debug.Log(c);
    //  }
 }