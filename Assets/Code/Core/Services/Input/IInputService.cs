namespace Core.Services.Input
{
    public interface IInputService
    {
        public bool IsEnabled { get; set; }
        public void BindInputs();
        public void UnbindInputs();
        public void EnableInput();
        public void DisableInput();
    }
}
