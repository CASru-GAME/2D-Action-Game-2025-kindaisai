public class Player
{
    public int Lives { get; private set; }

    public Player(int initialLives)
    {
        Lives = initialLives;
    }

    // 残機を減らす
    public void LoseLife()
    {
        if (Lives > 0)
        {
            Lives--;
        }
    }

    // ゲームオーバー判定
    public bool IsGameOver()
    {
        return Lives <= 0;
    }
}