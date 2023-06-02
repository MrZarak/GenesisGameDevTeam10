using System;
using System.Collections.Generic;
using Core.Services.Updater;
using InputReader;
using NPC.Controller;
using UI;

namespace Player
{
    public class PlayerSystem : IDisposable
    {
        private readonly ProjectUpdater _projectUpdater;
        private readonly PlayerEntity _playerEntity;
        public PlayerBrain PlayerBrain { get; }

        private List<IDisposable> _disposables;

        public PlayerSystem(UIContext uiContext, PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _disposables = new List<IDisposable>();
            _playerEntity = playerEntity;
            PlayerBrain = new PlayerBrain(uiContext, _playerEntity, inputSources);
        }


        public void Dispose()
        {
            PlayerBrain?.Dispose();
        }
    }
}