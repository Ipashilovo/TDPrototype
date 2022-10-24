using System;
using System.Collections.Generic;
using System.Threading;
using Balance.Data.Level;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Core.GameplaySystems.Wave
{
    public class WaveModule : IWaveModule
    {
        private readonly LevelData _levelData;
        private readonly ITimeProvider _timeProvider;
        private readonly IEnemyModule _enemyModule;
        private DisposableList _disposableList = new DisposableList();
        private CancellationTokenSource _cancellationTokenSource;
        private ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();
        private ReactiveProperty<Time> _timeToWave = new ReactiveProperty<Time>();
        private int _waveNumber;
        
        public IReadOnlyReactiveProperty<Time> TimeToWave => _timeToWave;
        public IReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public WaveModule(LevelData levelData,  IEnemyModule enemyModule, ITimeProvider timeProvider)
        {
            _levelData = levelData;
            _enemyModule = enemyModule;
            _timeProvider = timeProvider;
            _waveNumber = 0;
            CreateWaveAfterStartDealy();
        }

        private async UniTask CreateWaveAfterStartDealy()
        {
            await UniTask.Delay((int) (_levelData.TimeToStartWave.Value * 1000),
                cancellationToken: _cancellationTokenSource.Token);
            TryStartNextWave();
        }

        private async UniTask WaitToNewWave(Time time)
        {
            while (time <= _timeProvider.WorldTime)
            {
                _timeToWave.Value = time - _timeProvider.WorldTime; 
                await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token);
            }
            TryStartNextWave();
        }

        private void TryStartNextWave()
        {
            if (_waveNumber >= _levelData.WaveDatas.Length)
            {
                SubscribeEnemyDead(() => _isComplete.Value = true);
                return;
            }

            var currentWave = _levelData.WaveDatas[_waveNumber];
            _waveNumber++;
            if (currentWave.HaveBoss == false)
            {
                StartWave(currentWave, () => WaitToNewWave(currentWave.TimeToNextWave));
            }
            else
            {
                StartWave(currentWave, () => SubscribeEnemyDead(() => WaitToNewWave(currentWave.TimeToNextWave)));
            }
        }

        private void SubscribeEnemyDead(Action callback)
        {
            DisposableList temporaryList = new DisposableList();
            _disposableList.Add(temporaryList);
            _enemyModule.Count.Subscribe(v =>
            {
                if (v <= Amount.Zero)
                {
                    callback?.Invoke();
                    temporaryList.Dispose();
                }
            }).AddTo(temporaryList);
            return;
        }

        private async UniTask StartWave(WaveData currentWave, Action callback = null)
        {
            for (int i = 0; i < currentWave.Steps.Length; i++)
            {
                var step = currentWave.Steps[i];
                await UniTask.Delay((int) (step.StartDelay.Value * 1000), cancellationToken: _cancellationTokenSource.Token);
                List<UniTask> tasks = new List<UniTask>(step.WavePathDatas.Length);
                foreach (var wavePathData in step.WavePathDatas)
                {
                    tasks.Add(CreateUnitInWave(wavePathData));
                }


                await UniTask.WhenAll(tasks);
            }
            callback?.Invoke();
        }

        private async UniTask CreateUnitInWave(WavePathData wavePathData)
        {
            foreach (var wave in wavePathData.UnitCounts)
            {
                _enemyModule.AddEnemy(wavePathData.PathId, wave.SpawnCount);
                await UniTask.Delay((int) (wave.Delay.Value * 1000), cancellationToken: _cancellationTokenSource.Token);
            }

        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _disposableList?.Dispose();
            _cancellationTokenSource?.Dispose();
            _isComplete?.Dispose();
        }
    }
    

    public interface IWaveModule : IDisposable
    {
        public IReadOnlyReactiveProperty<Time> TimeToWave { get; }
        public IReadOnlyReactiveProperty<bool> IsComplete { get; }
    }
}