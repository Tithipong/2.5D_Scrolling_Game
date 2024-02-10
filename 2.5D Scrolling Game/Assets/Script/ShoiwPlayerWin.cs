using UnityEngine;

public class ShowPlayerWin : MonoBehaviour
{
    public Canvas winCanvas;
    
    public void Start()
    {
        winCanvas.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
