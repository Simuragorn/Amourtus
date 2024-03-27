
public class InsiderMinion : Insider
{
    public InsiderMinionConfiguration InsiderMinionConfiguration => Configuration as InsiderMinionConfiguration;
    protected override void Awake()
    {
        base.Awake();
        isTeleportable = false;
    }
}