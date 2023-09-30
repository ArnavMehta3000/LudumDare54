using UnityEngine;

namespace LD54
{
    public class GameManager : MonoBehaviour
    {
        public WaveManager waveManager;
        public Turret turret;

        private void Start()
        {
            waveManager.BeginWave(0);
        }
    } 
}
