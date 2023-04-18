using System;
using System.Collections.Generic;
using Core.Services.Updater;
using InputReader;
using Item.Core;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity playerEntity;
        [SerializeField] private GameUIInputView gameUIInputView;
        [SerializeField] private ItemRegistry itemRegistry;

        private ExternalDevicesInputReader _externalDevicesInput;
        private PlayerSystem _playerSystem;
        private ProjectUpdater _projectUpdater;
        private SceneItemsSystem _sceneItemsSystem;

        private List<IDisposable> _disposables;

        private bool _onPause;

        private void Awake()
        {
            _disposables = new List<IDisposable>();
            InitProjectUpdater();
            _externalDevicesInput = new ExternalDevicesInputReader(_projectUpdater);

            InitPlayerSystem();
            InitSceneItemsSystem();

            _disposables.Add(_sceneItemsSystem);
            _disposables.Add(_externalDevicesInput);
        }

        private void InitProjectUpdater()
        {
            if (ProjectUpdater.Instance == null)
            {
                _projectUpdater = new GameObject().AddComponent<ProjectUpdater>();
                _projectUpdater.name = nameof(ProjectUpdater);
            }
            else
            {
                _projectUpdater = ProjectUpdater.Instance as ProjectUpdater;
            }
        }

        private void InitPlayerSystem()
        {
            _playerSystem = new PlayerSystem(playerEntity, new List<IEntityInputSource>
            {
                gameUIInputView,
                _externalDevicesInput
            });
        }

        private void InitSceneItemsSystem()
        {
            _sceneItemsSystem = new SceneItemsSystem(playerEntity);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //todo single responsibility issue, fix that
                _projectUpdater.IsPaused = !_projectUpdater.IsPaused;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                // todo remove in future, for testing purpose
                var itemById = itemRegistry.GetItemById(
                    Random.value > 0.5 ? ItemId.LesserArcane : ItemId.LesserMana
                );

                var container = new ItemContainer(itemById, 1);
                _sceneItemsSystem.DropItem(container, playerEntity.transform.position);
            }
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}