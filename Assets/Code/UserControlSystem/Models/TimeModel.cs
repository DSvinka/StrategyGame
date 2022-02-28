using System;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace UserControlSystem.Models
{
    public sealed class TimeModel: ITimeModel, ITickable
    {
        private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

        public IObservable<int> GameTime => _gameTime.Select(val => (int) val);

        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }
    }
}