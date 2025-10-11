public class Player
{
    public int Lives { get; private set; }

    public Player(int initialLives)
    {
        Lives = initialLives;
    }

    // �c�@�����炷
    public void LoseLife()
    {
        if (Lives > 0)
        {
            Lives--;
        }
    }

    // �Q�[���I�[�o�[����
    public bool IsGameOver()
    {
        return Lives <= 0;
    }
}