using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Core;
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

        public PlayerSystem(UIContext uiContext,
            PlayerEntity playerEntity,
            List<IEntityInputSource> inputSources,
            GameLevelInitializer gameLevelInitializer
        )
        {
            _disposables = new List<IDisposable>();
            _playerEntity = playerEntity;
            PlayerBrain = new PlayerBrain(uiContext, _playerEntity, inputSources, gameLevelInitializer);
        }


        public void Dispose()
        {
            PlayerBrain?.Dispose();
        }
    }
}