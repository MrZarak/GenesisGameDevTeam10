using System.Collections.Generic;
using Core;
using InputReader;
using UI;

namespace Player
{
    public class PlayerSystem
    {
        private readonly PlayerEntity _playerEntity;
        private readonly PlayerBrain _playerBrain;

        public PlayerSystem(
            UIContext uiContext, PlayerEntity playerEntity, List<IEntityInputSource> inputSources,
            GameLevelInitializer gameLevelInitializer
        )
        {
            _playerEntity = playerEntity;
            _playerBrain = new PlayerBrain(uiContext, _playerEntity, inputSources, gameLevelInitializer);
        }
    }
}