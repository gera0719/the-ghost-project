namespace GhostProject.core
{
    public enum GameState { MENU, RUNNING, PAUSED, GAME_OVER, FINISHED }
    public enum HazardType { ACID, LASER }


    public abstract class GameObject
    {
        protected float x;
        protected float y;
        protected float width;
        protected float height;
        protected bool active;

        public abstract void Update();
        public abstract void Render();

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public bool IsActive { get => active; set => active = value; }
    }

    public class Player : GameObject
    {
        private int health;
        private float speed;

        public void Move() { }
        public void Jump() { }
        public void Crouch() { }
        public void Interact() { }

        public override void Update() { }
        public override void Render() { }
    }
    public class Hazard : GameObject
    {
        private HazardType type;
        private int damage;

        public void ApplyEffect() { }

        public override void Update() { }
        public override void Render() { }
    }
    public class Terminal : GameObject
    {
        private string message;
        private bool activated;

        public void Activate() { }
        public void DisplayMessage() { }

        public override void Update() { }
        public override void Render() { }
    }
    public class SectorTransition : GameObject
    {
        private int targetSectorId;
        private bool unlocked;

        public void Unlock() { }
        public void TransferPlayer() { }

        public override void Update() { }
        public override void Render() { }
    }
}