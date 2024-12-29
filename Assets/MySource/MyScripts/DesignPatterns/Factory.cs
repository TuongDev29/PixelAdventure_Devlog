// public static class Factory
// {
//     public static class Enemy
//     {
//         public static EnemyStateMachine Create(EEnemyType enemyType, EnemyController controller)
//         {
//             return enemyType switch
//             {
//                 EEnemyType.Mushroom => new MushroomStateMachine(controller),
//                 EEnemyType.Chicken => new ChickenStateMachine(controller),
//                 EEnemyType.Trunk => new TrunkStateMachine(controller),
//                 EEnemyType.Chameleon => new ChameleonStateMachine(controller),
//                 EEnemyType.AngryPig => new AngryPigStateMachine(controller),
//                 _ => throw new($"Unknown enemy type: {enemyType}"),
//             };
//         }
//     }
// }
