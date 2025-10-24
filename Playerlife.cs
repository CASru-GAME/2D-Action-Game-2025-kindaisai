public class Playerlife
{
    public int Lives { get; private set; }

    public Playerlife(int initialLives)
    {
        Lives = initialLives;
    }

    // 
    public void LoseLife()
    {
        if (Lives > 0)
        {
            Lives--;
        }
    }

    // 
    public bool IsGameOver()
    {
        return Lives <= 0;
    }
}