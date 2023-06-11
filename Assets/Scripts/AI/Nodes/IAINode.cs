public enum AINodeResult
{
    SUCCESS,
    FAILURE,
    IN_PROGRESS
}

public interface IAINode
{
    public void OnStart();

    public bool CanEnter();

    public AINodeResult TryRun();
}
