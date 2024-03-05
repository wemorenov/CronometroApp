using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Cronometro
{
    /// <summary>
    /// Delegado para el tiempo transcurido
    /// </summary>
    public delegate void TimeElaspsedHandler(TimeSpan elapsed);

    public class TimerServices
    {
        /// <summary>
        /// Evento del tiempo transcurrido
        /// </summary>
        public event TimeElaspsedHandler? TimeElaspsed;

        private DispatcherTimer? _timer = null;
        private DateTime _startTime;
        private TimeSpan _elapsed;
        private bool _isExecuting;

        /// <summary>
        /// Servicio Cronómetro
        /// </summary>
        public TimerServices()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick!;
        }

        /// <summary>
        /// Inicia el cronómetro
        /// </summary>
        public void Start()
        {
            if (!_isExecuting)
            {
                _startTime = DateTime.Now;
                _timer!.Start();
                _isExecuting = true;
            }
        }

        /// <summary>
        /// Detiene el cronómetro
        /// </summary>
        public void Stop()
        {
            _timer!.Stop();
            _isExecuting = false;
            TimeElaspsed?.Invoke(TimeSpan.Zero);
        }

        /// <summary>
        /// Pausa el cronómetro
        /// </summary>
        public void Pause()
        {
            if (_isExecuting)
            {
                _timer!.Stop();
                _isExecuting = false;
            }
            else
            {
                _startTime = _startTime.Add(_elapsed); 
                _timer!.Start();
                _isExecuting = true;

            }
        }

        public bool IsExecuting { 
            get {
                    return _isExecuting; 
                }
        }

        /// <summary>
        /// Manejador el tick del timer
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            _elapsed = DateTime.Now - _startTime;
            TimeElaspsed?.Invoke(_elapsed);
        }
    }
}
