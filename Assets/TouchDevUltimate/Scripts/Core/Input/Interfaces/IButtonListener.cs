namespace TouchDevUltimate.Core.Input
{
    public interface IButtonListener
    {
        void OnButton(string name, InputState state);
    }
}