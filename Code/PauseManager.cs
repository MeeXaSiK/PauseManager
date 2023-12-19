using System;
using System.Collections.Generic;
using NTC.Services.Pause.Interfaces;

namespace NTC.Services.Pause
{
    public class PauseManager : IPauseHandler
    {
        private readonly HashSet<IPauseHandler> _pauseHandlers = new(32);

        public bool IsPaused { get; private set; }
        
        public void RegisterHandler(IPauseHandler pauseHandler)
        {
#if DEBUG
            if (pauseHandler == null)
                throw new NullReferenceException(nameof(pauseHandler));
#endif
            _pauseHandlers.Add(pauseHandler);
        }

        public void UnregisterHandler(IPauseHandler pauseHandler)
        {
#if DEBUG
            if (pauseHandler == null)
                throw new NullReferenceException(nameof(pauseHandler));
#endif
            _pauseHandlers.Remove(pauseHandler);
        }
        
        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;

            foreach (var pauseHandler in _pauseHandlers)
            {
                pauseHandler.SetPaused(isPaused);
            }
        }
    }
}