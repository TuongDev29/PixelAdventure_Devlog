public static class EnemyStateMachineFactory
{
    public static EnemyStateMachine Create(EEnemyCode enemyCode, Blackboard<EEnemyBlackBoard> blackboard)
    {
        return enemyCode switch
        {
            EEnemyCode.Mushroom => new MushroomStateMachine(blackboard),
            EEnemyCode.Chicken => new ChickenStateMachine(blackboard),
            EEnemyCode.Chameleon => new ChameleonStateMachine(blackboard),
            EEnemyCode.AngryPig => new AngryPigStateMachine(blackboard),
            EEnemyCode.Trunk => new TrunkStateMachine(blackboard),
            EEnemyCode.Rino => new RinoStateMachine(blackboard),
            _ => throw new($"Unknown enemy type: {enemyCode}"),
        };
    }
}