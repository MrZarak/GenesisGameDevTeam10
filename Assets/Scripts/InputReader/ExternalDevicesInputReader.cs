using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Player;
using Core.Services.Updater;

namespace InputReader
{
    public class ExternalDevicesInputReader: IEntityInputSource, IDisposable
    {
    
        public float HorizontalDirection => Input.GetAxisRaw("Horizontal");
        public float VerticalDirection => Input.GetAxisRaw("Vertical");
        public bool Jump { get; private set; }
        public bool Attack { get; private set; }

        public ExternalDevicesInputReader()
        {
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        }

        public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
   
        public void ResetOneTimeActions() 
        {
            Jump = false;
            Attack = false;
        }

        private void OnUpdate()
        {
            if(Input.GetButtonDown("Jump"))
                Jump = true;

            if(!IsPointerOverUI() && Input.GetButtonDown("Fire1"))
                Attack = true;
        }
       
         private bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
    }
}

