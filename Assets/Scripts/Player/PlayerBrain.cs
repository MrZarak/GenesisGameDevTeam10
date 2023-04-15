using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using InputReader;
using Core.Services.Updater;

namespace Player
{
    public class PlayerBrain : IDisposable
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSource> _inputSources;

        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
            ProjectUpdater.Instance.FixedUpdateCalled += OnFixedUpdate;
        }
        public void Dispose() => ProjectUpdater.Instance.FixedUpdateCalled -= OnFixedUpdate;
        private void OnFixedUpdate()
        {
            _playerEntity.MoveHorizontally(GetHorizontalDirection());
            _playerEntity.MoveVertically(GetVerticalDirection());

            if(IsJump)
                _playerEntity.Jump();

            if(IsAttack)
                _playerEntity.StartAttack();  

            foreach (var inputSources in _inputSources)
                inputSources.ResetOneTimeActions();  
        }

        private float GetHorizontalDirection()
        {
            foreach(var inputSources in _inputSources)
            {
                if(inputSources.HorizontalDirection == 0)
                    continue;
                
                return inputSources.HorizontalDirection;
            }

            return 0;
        }

        private float GetVerticalDirection()
        {
            foreach(var inputSources in _inputSources)
            {
                if(inputSources.VerticalDirection == 0)
                    continue;
                
                return inputSources.VerticalDirection;
            }

            return 0;
        }

        private bool IsJump => _inputSources.Any(source => source.Jump);
        private bool IsAttack => _inputSources.Any(source => source.Attack);
    }
}

