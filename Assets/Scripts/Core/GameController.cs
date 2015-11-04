using UnityEngine;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        public static Game Game { get; private set; }
        private void Awake()
        {
            Game = new Game(this);
        }
    }
}
