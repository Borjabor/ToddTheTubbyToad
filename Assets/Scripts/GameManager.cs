using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void SingletonAwake()
    {
    }
    
    void OnEnable()
    {
        Load();
    }
    async public void Load()
    {
        await Task.Delay((int) (1000.0f * Time.deltaTime));
    }
    
    public void Save()
    {
        
    }
}