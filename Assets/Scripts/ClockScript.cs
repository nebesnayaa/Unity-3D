using UnityEngine;

public class ClockScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    private float gameTime;

    void Start()
    {
        clock = GetComponent<TMPro.TextMeshProUGUI>();
        if (clock == null)
        {
            Debug.Log("TextMeshProUGUI не знайдено!");
        }
        gameTime = 0.0f;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        int hours = Mathf.FloorToInt(gameTime / 3600);
        int minutes = Mathf.FloorToInt((gameTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        float fraction = gameTime * 9f % 9f;

        clock.text = string.Format("{0:00}:{1:00}:{2:00}.{3:0}", hours, minutes, seconds, fraction);
    }
}
