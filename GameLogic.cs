public class zanki

{
    private Player player;

    public zanki()
    {
        player = new Player(); 
    }

  
    public void OnHitEnemy()
    {
        player.LoseLife();
        CheckGameOver();
    }

    
    public void OnFallIntoPit()
    {
        player.LoseLife();
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (player.IsGameOver())
        {
            // �Q�[���I�[�o�[�����i�������j
            
        }
    }
}