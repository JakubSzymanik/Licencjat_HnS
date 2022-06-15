using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    [SerializeField] PlayerCharacterContoler player;
    [SerializeField] List<Enemy> enemies;
    [SerializeField] EnemiesCounter counter;
    [SerializeField] GameOverPanel goPanel;

    private void Start()
    {
        player.OnDeath.AddListener(OnPlayerDeath);
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnDeath.AddListener(OnEnemyDeath);
        }
        counter.SetAmount(enemies.Count);
        goPanel.OnQuitPressed.AddListener(Quit);
        goPanel.OnRestartPressed.AddListener(Restart);
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
        counter.SetAmount(enemies.Count);
        if(enemies.Count <= 0)
        {
            goPanel.Show(true);
        }
    }
    private void OnPlayerDeath()
    {
        goPanel.Show(false);
    }

    private void Quit()
    {
        Application.Quit();
    }
    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
