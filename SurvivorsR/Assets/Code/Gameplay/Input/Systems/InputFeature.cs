using Code.Gameplay.Input.Service;

namespace Code.Gameplay.Input.Systems
{
    public class InputFeature : Feature
    {
        public InputFeature(GameContext gameContext, IInputService inputService)
        {
            Add(new InitializeInputSystem());
            Add(new EmitInputSystem(gameContext, inputService));
        }
    }
}