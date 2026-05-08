using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    public float timeLimit = 120f; 
    public TextMeshProUGUI timerText;

    [Header("Collectibles")]
    public int totalCubes = 0; 
    private int collected = 0;

    public static GameManager Instance;

    void Awake()
    {
        // Ensure singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLimit>0f)
            timeLimit-=Time.deltaTime;
        else
            timeLimit = 0f;    
        UpdateTimerUI();
    }

    public void CollectCube()
    {
        collected++;
        if(collected==totalCubes)
        {
            Debug.Log("Game complete");
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeLimit / 60);
        int seconds = Mathf.FloorToInt(timeLimit % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
