public class zanki

{
    private Playerlife player;

    public zanki()
    {
        player = new Playerlife(); 
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