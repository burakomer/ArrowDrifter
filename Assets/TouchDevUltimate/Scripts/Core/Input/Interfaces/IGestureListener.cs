namespace TouchDevUltimate.Core.Input
{
    public interface IGestureListener
    {
        void OnGesture(string name, GestureType type);
    }
}