using System.Collections.Generic;
using InputReader;
using UI;

namespace Player
{
    public class PlayerSystem
    {
        private readonly PlayerEntity _playerEntity;
        private readonly PlayerBrain _playerBrain;
        public PlayerSystem(UIContext uiContext, PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _playerBrain = new PlayerBrain(uiContext, _playerEntity, inputSources);
        }
    }
}
  

